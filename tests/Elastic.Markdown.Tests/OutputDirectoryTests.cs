using System.IO.Abstractions.TestingHelpers;
using Elastic.Markdown.IO;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Elastic.Markdown.Tests;

public class OutputDirectoryTests
{
	[Fact]
	public async Task CreatesDefaultOutputDirectory()
	{
		var logger = NullLoggerFactory.Instance;
		var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
		{
			{ "docs/source/index.md", new MockFileData("test") }
		}, new MockFileSystemOptions
		{
			CurrentDirectory = Paths.Root.FullName
		});
		var context = new BuildContext { ReadFileSystem = fileSystem, WriteFileSystem = fileSystem };
		var set = new DocumentationSet(null, null, context);
		var generator = new DocumentationGenerator(set, context, logger);

		await generator.GenerateAll(default);

		fileSystem.Directory.Exists(".artifacts").Should().BeTrue();

	}
}
