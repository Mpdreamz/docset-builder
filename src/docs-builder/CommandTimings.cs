using System.Diagnostics;
using ConsoleAppFramework;

namespace Documentation.Builder;

internal class CommandTimings(ConsoleAppFilter next) : ConsoleAppFilter(next)
{
	public override async Task InvokeAsync(ConsoleAppContext context, Cancel ctx)
	{
		Console.WriteLine($":: {context.CommandName} :: Starting");
		var sw = Stopwatch.StartNew();
		try
		{
			await Next.InvokeAsync(context, ctx);
		}
		finally
		{
			sw.Stop();
			Console.WriteLine($":: {context.CommandName} :: Finished in '{sw.Elapsed}");
		}
	}
}
