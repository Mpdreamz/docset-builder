using ConsoleAppFramework;
using ProcNet;
using Zx;
//using static Zx.Env;

var app = ConsoleApp.Create();
app.Add("", async (Cancel _) =>
{
	await "dotnet tool restore";
	await "dotnet build -c Release --verbosity minimal";
});
app.Add("validate-licenses", (Cancel _) =>
{
	string[] args =
	[
		"-u", "-t", "-i", "docs-builder.sln", "--use-project-assets-json",
		"--forbidden-license-types", "build/forbidden-license-types.json",
		"--packages-filter", "#System..*#"
	];
	Proc.Exec("dotnet", ["dotnet-project-licenses", ..args]);
});

await app.RunAsync(args);
