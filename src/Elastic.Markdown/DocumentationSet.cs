using Elastic.Markdown.Files;
using Elastic.Markdown.Parsers;

namespace Elastic.Markdown;

public class DocumentationSet
{
	public string Name { get; }
	public DirectoryInfo SourcePath { get; }
	private DirectoryInfo OutputPath { get; }

	public DocumentationSet(DirectoryInfo sourcePath, DirectoryInfo outputPath, MarkdownParser parser)
	{
		Name = sourcePath.FullName;
		SourcePath = sourcePath;
		OutputPath = outputPath;

		Files = Directory.EnumerateFiles(SourcePath.FullName, "*.*", SearchOption.AllDirectories)
			.Select(f => new FileInfo(f))
			.Select<FileInfo, DocumentationFile>(file => file.Extension switch
			{
				".png" => new ImageFile(file, SourcePath),
				".md" => new MarkdownFile(file, SourcePath, parser),
				_ => new StaticFile(file, SourcePath)
			})
			.ToList();

		FlatMappedFiles = Files.ToDictionary(file => file.RelativePath, file => file);

		var markdownFiles = Files.OfType<MarkdownFile>()
			.Where(file => !file.RelativePath.StartsWith("_"))
			.GroupBy(file =>
			{
				var path = file.ParentFolders.Count >= 1 ? file.ParentFolders[0] : file.FileName;
				return path;
			})
			.ToDictionary(k => k.Key, v => v.ToArray());

		Tree = new DocumentationFolder(markdownFiles, 0, "");
	}

	public DocumentationFolder Tree { get; }

	public List<DocumentationFile> Files { get; }

	public Dictionary<string, DocumentationFile> FlatMappedFiles { get; }

	public void ClearOutputDirectory()
	{
		if (OutputPath.Exists)
			OutputPath.Delete(true);
		OutputPath.Create();
	}
}
