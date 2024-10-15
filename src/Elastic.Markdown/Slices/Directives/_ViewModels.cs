using System.Text;
using Elastic.Markdown.Myst.Directives;

namespace Elastic.Markdown.Slices.Directives;

public class AdmonitionViewModel
{
	public required string Title { get; init; }
	public required string Directive { get; init; }
	public required string? CrossReferenceName { get; init; }
	public required string? Classes { get; init; }
	public required string? Open { get; init; }
}

public class CodeViewModel
{
	public required string? Caption { get; init; }
	public required string Language { get; init; }
	public required string? CrossReferenceName { get; init; }
}

public class VersionViewModel
{
	public required string Directive { get; init; }
	public required string VersionClass { get; init; }
	public required string Title { get; init; }
}

public class SideBarViewModel;
public class TabSetViewModel;

public class TabItemViewModel
{
	public required int Index { get; init; }
	public required int TabSetIndex { get; init; }
	public required string Title { get; init; }
}

public class CardViewModel
{
	public required string? Title { get; init; }
	public required string? Link { get; init; }
}

public class GridViewModel
{
	public required GridResponsive BreakPoint { get; init; }
}

public class GridItemCardViewModel
{
	public required string? Title { get; init; }
	public required string? Link { get; init; }
}

public class ImageViewModel
{
	public required string? CrossReferenceName { get; init; }
	public required string? Classes { get; init; }
	public required string? Align { get; init; }
	public required string? Alt { get; init; }
	public required string? Height { get; init; }
	public required string? Scale { get; init; }
	public required string? Target { get; init; }
	public required string? Width { get; init; }
	public required string? ImageUrl { get; init; }

	public string Style
	{
		get
		{
			var sb = new StringBuilder();
			if (Height != null) sb.Append($"height: {Height};");
			if (Width != null) sb.Append($"width: {Width};");
			return sb.ToString();
		}
	}
}
