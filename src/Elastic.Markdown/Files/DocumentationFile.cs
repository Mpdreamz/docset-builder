namespace Elastic.Markdown.Files;

public abstract class DocumentationFile
{
	protected DocumentationFile(FileInfo sourceFile, DirectoryInfo sourcePath, DirectoryInfo outputPath)
	{
		SourceFile = sourceFile;
		RelativePath = Path.GetRelativePath(sourcePath.FullName, sourceFile.FullName);
		OutputFile  = new FileInfo(Path.Combine(outputPath.FullName, RelativePath.Replace(".md", ".html")));
	}

	public FileInfo SourceFile { get; }
	public FileInfo OutputFile { get; }
	public string RelativePath { get; }

}

public class ImageFile(FileInfo sourceFile, DirectoryInfo sourcePath, DirectoryInfo outputPath)
	: DocumentationFile(sourceFile, sourcePath, outputPath);

public class StaticFile(FileInfo sourceFile, DirectoryInfo sourcePath, DirectoryInfo outputPath)
	: DocumentationFile(sourceFile, sourcePath, outputPath);
