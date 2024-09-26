// See https://aka.ms/new-console-template for more information

using System.Text;
using AsciiDocNet;
using ProcNet;

var sourceFolder = args[0];
var outputFolder = args[1];


var files = Directory.EnumerateFiles(sourceFolder, "*.*", SearchOption.AllDirectories);

/*
await Parallel.ForEachAsync(files, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, async (file, ctx) =>
{
	await Convert(sourceFolder, file, outputFolder, ctx);
});
*/
foreach (var file in files)
	await Convert(sourceFolder, file, default);

async Task Convert(string s, string file, CancellationToken cancellationToken)
{
	//if (!file.EndsWith("index-modules.asciidoc")) return;

	var relative = Path.GetRelativePath(s, file);

	var outputPath = new FileInfo(Path.Combine(outputFolder, relative));
	Directory.CreateDirectory(outputPath.Directory!.FullName);

	if (!file.EndsWith(".asciidoc") && !file.EndsWith(".adoc"))
	{
		File.Copy(file, outputPath.FullName, true);
		return;
	}

	var contents = File.ReadAllText(file);
	try
	{
		var document = Document.Parse(contents);
		var sb = new StringBuilder();
		var visitor = new IncludesToMarkdownVisitor(new StringWriter(sb), debug: false);
		visitor.VisitDocument(document);
		var converted = sb.ToString().TrimEnd('\r', '\n');
		await File.WriteAllTextAsync(outputPath.FullName, converted, cancellationToken);
		var mdFile = outputPath.FullName.Replace(".asciidoc", ".md").Replace(".adoc", ".md");
		var md = Proc.Start("/Users/mpdreamz/Projects/downdoc/downdoc-macos", outputPath.FullName, "-o", mdFile);
		Console.WriteLine(md.ExitCode);
		outputPath.Delete();

		//// ./../../../downdoc/downdoc-macos ../test.adoc -o test.md

	}
	catch (Exception e)
	{
		Console.WriteLine(file);
		Console.ForegroundColor = ConsoleColor.DarkRed;
		Console.WriteLine(e);
		Console.ResetColor();
		//throw;
	}
}


// ReSharper disable once RedundantExtendsListEntry
