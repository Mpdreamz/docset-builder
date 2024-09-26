namespace Elastic.Markdown;

public static class Paths
{
	private static DirectoryInfo RootDirectoryInfo()
	{
		var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
		while (directory != null && !directory.GetFiles("*.sln").Any())
			directory = directory.Parent;
		return directory ?? new DirectoryInfo(Directory.GetCurrentDirectory());
	}

	public static DirectoryInfo Root = RootDirectoryInfo();
}
