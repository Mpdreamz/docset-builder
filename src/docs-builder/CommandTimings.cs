using System.Diagnostics;
using ConsoleAppFramework;

namespace Documentation.Builder;

internal class CommandTimings(ConsoleAppFilter next) : ConsoleAppFilter(next)
{
	public override async Task InvokeAsync(ConsoleAppContext context, Cancel ctx)
	{
		var isHelpOrVersion = context.Arguments.Any(a => a is "--help" or "-h" or "--version");
		if (!isHelpOrVersion)
			Console.WriteLine($":: {context.CommandName} :: Starting");
		var sw = Stopwatch.StartNew();
		try
		{
			await Next.InvokeAsync(context, ctx);
		}
		finally
		{
			sw.Stop();
			if (!isHelpOrVersion)
				Console.WriteLine($":: {context.CommandName} :: Finished in '{sw.Elapsed}");
		}
	}
}

internal class CatchExceptionFilter(ConsoleAppFilter next) : ConsoleAppFilter(next)
{
	public override async Task InvokeAsync(ConsoleAppContext context, CancellationToken cancellationToken)
	{
		try
		{
			await Next.InvokeAsync(context, cancellationToken);
		}
		catch (Exception ex)
		{
			if (ex is OperationCanceledException)
			{
				ConsoleApp.Log("Cancellation requested, exiting.");
				return;
			}

			ConsoleApp.LogError(ex.ToString()); // .ToString() shows stacktrace, .Message can avoid showing stacktrace to user.
		}
	}
}
