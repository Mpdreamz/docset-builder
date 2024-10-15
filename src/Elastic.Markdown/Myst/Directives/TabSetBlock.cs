namespace Elastic.Markdown.Myst.Directives;

public class SideBarBlock(DirectiveBlockParser blockParser, string directive, Dictionary<string, string> properties)
	: DirectiveBlock(blockParser, properties)
{
	public string Directive => directive;

	public override void FinalizeAndValidate()
	{
	}
}

public class TabSetBlock(DirectiveBlockParser blockParser, Dictionary<string, string> properties)
	: DirectiveBlock(blockParser, properties)
{
	public int Index { get; set; }
	public override void FinalizeAndValidate() => Index = FindIndex();

	private int _index = -1;
	public int FindIndex()
	{
		if (_index > -1) return _index;
		var siblings = Parent!.OfType<TabSetBlock>().ToList();
		_index = siblings.IndexOf(this);
		return _index;
	}
}
public class TabItemBlock(DirectiveBlockParser blockParser, Dictionary<string, string> properties)
	: DirectiveBlock(blockParser, properties)
{
	public string Title { get; set; } = default!;
	public int Index { get; set; }
	public int TabSetIndex { get; set; }

	public override void FinalizeAndValidate()
	{
		Title = Arguments ?? "Unnamed Tab";
		Index = Parent!.IndexOf(this);
		TabSetIndex = Parent is TabSetBlock tb ? tb.FindIndex() : -1;
	}

}
public class CardBlock(DirectiveBlockParser blockParser, string directive, Dictionary<string, string> properties)
	: DirectiveBlock(blockParser, properties)
{
	public string Directive => directive;

	public override void FinalizeAndValidate()
	{
	}
}
public class GridBlock(DirectiveBlockParser blockParser, string directive, Dictionary<string, string> properties)
	: DirectiveBlock(blockParser, properties)
{
	public string Directive => directive;

	public override void FinalizeAndValidate()
	{
	}
}
public class GridItemCardBlock(DirectiveBlockParser blockParser, string directive, Dictionary<string, string> properties)
	: DirectiveBlock(blockParser, properties)
{
	public string Directive => directive;

	public override void FinalizeAndValidate()
	{
	}
}

public class UnknownDirectiveBlock(DirectiveBlockParser blockParser, string directive, Dictionary<string, string> properties)
	: DirectiveBlock(blockParser, properties)
{
	public string Directive => directive;

	public override void FinalizeAndValidate()
	{
	}
}
