using ConsoleAppFramework;
using Documentation.Builder;
using Documentation.Builder.Http;
using Elastic.Markdown;

var app = ConsoleApp.Create();
app.UseFilter<CommandTimings>();
app.UseFilter<CatchExceptionFilter>();

app.Add("", Commands.Generate);
app.Add("generate", Commands.Generate);
app.Add("serve", Commands.Serve);

await app.RunAsync(args);


internal static class Commands
{
	/// <summary>
	///
	/// </summary>
	/// <param name="path"></param>
	/// <param name="ctx"></param>
	public static async Task Serve(string? path = null, Cancel ctx = default)
	{
		var host = new DocumentationWebHost(path);
		await host.RunAsync(ctx);
	}


	/// <summary>
	/// Converts a source markdown folder or file to an output folder
	/// </summary>
	/// <param name="path"> -p, Defaults to the`{pwd}/docs` folder</param>
	/// <param name="output"> -o, Defaults to `.artifacts/html` </param>
	/// <param name="ctx"></param>
	public static async Task Generate(string? path = null, string? output = null, Cancel ctx = default)
	{
		var generator = DocumentationGenerator.Create(path, output);
		await generator.GenerateAll(ctx);
	}
}
