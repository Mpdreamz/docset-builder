using Elastic.Markdown.DocSet;
using Elastic.Markdown.Myst.Directives;
using Elastic.Markdown.Templating;
using Markdig;
using Markdig.Extensions.Yaml;
using Markdig.Helpers;
using Markdig.Syntax;
using Slugify;

namespace Elastic.Markdown.Files;

public class MarkdownFile : DocumentationFile
{
	private readonly SlugHelper _slugHelper = new();
	private string? _tocTitle;

	public MarkdownFile(FileInfo sourceFile, DirectoryInfo sourcePath, DirectoryInfo outputPath)
		: base(sourceFile, sourcePath, outputPath)
	{
		ParentFolders = RelativePath.Split(Path.DirectorySeparatorChar).SkipLast(1).ToArray();
		FileName = sourceFile.Name;
	}

	public required MarkdownConverter MarkdownConverter { get; init; }
	private YamlFrontMatterConverter YamlFrontMatterConverter { get; } = new();
	public string? Title { get; private set; }
	public string? TocTitle
	{
		get => !string.IsNullOrEmpty(_tocTitle) ? _tocTitle : Title;
		set => _tocTitle = value;
	}

	public List<PageTocItem> TableOfContents { get; } = new();
	public IReadOnlyList<string> ParentFolders { get; }
	public string FileName { get; }
	public string Url => $"/{RelativePath.Replace(".md", ".html")}";

	public async Task ParseAsync(CancellationToken ctx)
	{
		var document = await MarkdownConverter.ParseAsync(SourceFile, ctx);
		if (document.FirstOrDefault() is YamlFrontMatterBlock yaml)
		{
			var raw = string.Join(Environment.NewLine, yaml.Lines.Lines);
			var frontMatter = YamlFrontMatterConverter.Deserialize(raw);
			Title = frontMatter.Title;
		}

		if (SourceFile.Name == "index.md")
		{
			TocTree = document
				.Where(block => block is TocTreeBlock)
				.Cast<TocTreeBlock>()
				.FirstOrDefault()?.Links ?? new OrderedList<TocTreeLink>();
		}

		var contents = document
			.Where(block => block is HeadingBlock { Level: 2 })
			.Cast<HeadingBlock>()
			.Select(h => h.Inline?.FirstChild?.ToString())
			.Where(title => !string.IsNullOrWhiteSpace(title))
			.Select(title => new PageTocItem { Heading = title!, Slug = _slugHelper.GenerateSlug(title) })
			.ToList();
		TableOfContents.Clear();
		TableOfContents.AddRange(contents);
	}

	public OrderedList<TocTreeLink>? TocTree { get; private set; }

	public async Task<string> CreateHtmlAsync(CancellationToken ctx)
	{
		var document = await MarkdownConverter.ParseAsync(SourceFile, ctx);
		return document.ToHtml(MarkdownConverter.Pipeline);
	}
}
