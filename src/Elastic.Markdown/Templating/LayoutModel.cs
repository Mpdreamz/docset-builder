using Elastic.Markdown.DocSet;
using Elastic.Markdown.Files;
using Markdig.Syntax;

namespace Elastic.Markdown.Templating;

public class LayoutModel
{
	public string Title { get; set; } = "Elastic Documentation";
	public required IReadOnlyCollection<PageTocItem> PageTocItems { get; init; }
	public required DocumentationGroup Tree { get; init; }
	public required MarkdownFile CurrentDocument { get; init; }
	public required string Navigation { get; set; }
}

public class PageTocItem
{
	public required string Heading { get; init; }
	public required string Slug { get; init; }
}

public class NavigationModel
{
	public required DocumentationGroup Tree { get; init; }
	public required MarkdownFile CurrentDocument { get; init; }
}

public class TreeItemModel
{
	public required int Level { get; init; }
	public required MarkdownFile CurrentDocument { get; init; }
	public required DocumentationGroup SubTree { get; init; }
}
