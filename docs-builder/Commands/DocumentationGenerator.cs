using Elastic.Markdown;
using Elastic.Markdown.Parsers;
using Elastic.Markdown.Files;
using Elastic.Markdown.Slices;

namespace Documentation.Builder.Commands;

public class DocumentationGenerator
{
	private DirectoryInfo SourcePath { get; } = new (Path.Combine(Paths.Root.FullName, "docs/source"));
	private DirectoryInfo OutputPath { get; } = new (Path.Combine(Paths.Root.FullName, ".artifacts/docs/html"));
	private HtmlWriter HtmlWriter { get; }
	private MarkdownParser MarkdownParser { get; } = new();

	public DocumentationSet DocumentationSet { get; }

	public DocumentationGenerator(string? path, string? output)
	{
		SourcePath = path != null ? new DirectoryInfo(path) : SourcePath;
		OutputPath = output != null ? new DirectoryInfo(output) : OutputPath;
		DocumentationSet = new DocumentationSet(SourcePath, OutputPath, MarkdownParser);
		HtmlWriter = new HtmlWriter(DocumentationSet);
	}

	public async Task ResolveDirectoryTree(CancellationToken ctx) =>
		await DocumentationSet.Tree.Resolve(ctx);

	public async Task ReloadNavigationAsync(MarkdownFile current, CancellationToken ctx) =>
		await HtmlWriter.ReloadNavigation(current, ctx);

	public async Task Build(CancellationToken ctx)
	{
		DocumentationSet.ClearOutputDirectory();

		Console.WriteLine("Resolving tree");
		await ResolveDirectoryTree(ctx);
		Console.WriteLine("Resolved tree");

		var handledItems = 0;
		await Parallel.ForEachAsync(DocumentationSet.Files, ctx, async (file, token) =>
		{
			var item = Interlocked.Increment(ref handledItems);
			var outputFile = file.OutputFile(OutputPath);
			if (file is MarkdownFile markdown)
			{
				await markdown.ParseAsync(token);
				await HtmlWriter.WriteAsync(outputFile, markdown, token);
			}
			else
			{
				if (outputFile.Directory is { Exists: false })
					outputFile.Directory.Create();
				File.Copy(file.SourceFile.FullName, outputFile.FullName, true);
			}
			if (item % 1_000 == 0)
				Console.WriteLine($"Handled {handledItems} files");
		});
	}

	public async Task<string?> RenderLayout(MarkdownFile markdown, CancellationToken ctx)
	{
		await DocumentationSet.Tree.Resolve(ctx);
		return await HtmlWriter.RenderLayout(markdown, ctx);
	}
}
