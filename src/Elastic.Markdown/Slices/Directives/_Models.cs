namespace Elastic.Markdown.Slices.Directives;

public class AdmonitionModel
{
	public required string Title { get; init; }
	public required string Directive { get; init; }
	public required string? Id { get; init; }
	public required string? Classes { get; init; }
}

public class CodeModel
{
	public required string? Caption { get; init; }
	public required string Language { get; init; }
	public required string? Id { get; init; }
}

public class VersionModel
{
	public required string Directive { get; init; }
	public required string VersionClass { get; init; }
	public required string Title { get; init; }
}

public class SideBarModel;
public class TabSetModel;

public class TabItemModel
{
	public required int Index { get; init; }
	public required int TabSetIndex { get; init; }
	public required string Title { get; init; }
}

public class CardModel
{
	public required string? Title { get; init; }
	public required string? Link { get; init; }
}

public class GridModel
{
	public required int BreakPointXs { get; init; }
	public required int BreakPointSm { get; init; }
	public required int BreakPointMd { get; init; }
	public required int BreakPointLg { get; init; }

}

public class GridItemCardModel
{
	public required string? Title { get; init; }
	public required string? Link { get; init; }
};
