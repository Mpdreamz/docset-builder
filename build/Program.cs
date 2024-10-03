using ConsoleAppFramework;
using Zx;
using static Zx.Env;

var app = ConsoleApp.Create();
app.Add("", async (Cancel _) =>
{
	await "dotnet build -c Release --verbosity minimal";
});

await app.RunAsync(args);
