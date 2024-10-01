using ConsoleAppFramework;
using Documentation.Builder;
using Documentation.Builder.Commands;
using Documentation.Builder.Http;

var app = ConsoleApp.Create();
app.UseFilter<CommandTimings>();

app.Add("generate", async Task (string? path = null, string? output = null, CancellationToken ctx = default) =>
{
	var generator = new DocumentationGenerator(path, output);
	await generator.Build(ctx);
});

app.Add("serve", async Task (string? path = null, CancellationToken ctx = default) =>
{
	var host = new DocumentationWebHost(path, args);
	await host.RunAsync(ctx);
});

app.Run(args);
