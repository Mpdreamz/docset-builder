using Documentation.Builder.Commands;
using Elastic.Markdown.Files;
using Microsoft.Extensions.DependencyInjection;

namespace Documentation.Builder.Http;

/// <summary>Singleton behaviour enforced by registration on <see cref="IServiceCollection"/></summary>
public class ReloadableGeneratorState(string? path, string? output)
{
	private string? Path { get; } = path;
	private string? Output { get; } = output;

	private DocumentationGenerator _generator = new(path, output);
	public DocumentationGenerator Generator => _generator;

	public async Task ReloadAsync(CancellationToken ctx)
	{
		var generator = new DocumentationGenerator(Path, Output);
		await generator.ResolveDirectoryTree(ctx);
		Interlocked.Exchange(ref _generator, generator);
	}

	public async Task ReloadNavigationAsync(MarkdownFile current, CancellationToken ctx) =>
		await Generator.ReloadNavigationAsync(current, ctx);
}
