using Elastic.Markdown.DocSet;
using Elastic.Markdown.Files;

namespace Elastic.Markdown;

public class DocumentationSet
{
	public string Name { get; }
	public DirectoryInfo SourcePath { get; }
	public DirectoryInfo OutputPath { get; }

	public DocumentationSet(DirectoryInfo sourcePath, DirectoryInfo outputPath, MarkdownConverter markdownConverter)
	{
		Name = sourcePath.FullName;
		SourcePath = sourcePath;
		OutputPath = outputPath;

		Files = Directory.EnumerateFiles(SourcePath.FullName, "*.*", SearchOption.AllDirectories)
			.Select(f => new FileInfo(f))
			.Select<FileInfo, DocumentationFile>(file => file.Extension switch
			{
				".png" => new ImageFile(file, SourcePath, OutputPath),
				".md" => new MarkdownFile(file, SourcePath, OutputPath)
				{
					MarkdownConverter = markdownConverter
				},
				_ => new StaticFile(file, SourcePath, OutputPath)
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

		Tree = new DocumentationGroup(markdownFiles, 0, "");
	}

	public DocumentationGroup Tree { get; }

	public List<DocumentationFile> Files { get; }
	public Dictionary<string, DocumentationFile> FlatMappedFiles { get; }

	public void ClearOutputDirectory()
	{
		if (OutputPath.Exists)
			OutputPath.Delete(true);
		OutputPath.Create();
	}
}
