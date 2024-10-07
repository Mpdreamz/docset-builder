using System.IO.Abstractions;

namespace Elastic.Markdown.IO;

public abstract class DocumentationFile(FileInfo sourceFile, IDirectoryInfo sourcePath)
{
	public FileInfo SourceFile { get; } = sourceFile;
	public string RelativePath { get; } = Path.GetRelativePath(sourcePath.FullName, sourceFile.FullName);

	public FileInfo OutputFile(IDirectoryInfo outputPath) =>
		new(Path.Combine(outputPath.FullName, RelativePath.Replace(".md", ".html")));
}

public class ImageFile(FileInfo sourceFile, IDirectoryInfo sourcePath)
	: DocumentationFile(sourceFile, sourcePath);

public class StaticFile(FileInfo sourceFile, IDirectoryInfo sourcePath)
	: DocumentationFile(sourceFile, sourcePath);
