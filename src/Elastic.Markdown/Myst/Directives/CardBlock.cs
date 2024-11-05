namespace Elastic.Markdown.Myst.Directives;

public class CardBlock(DirectiveBlockParser parser, Dictionary<string, string> properties)
	: DirectiveBlock(parser, properties)
{
	public string? Link { get; set; }

	public string? Title { get; set; }

	public string? Header { get; set; }

	public string? Footer { get; set; }

	public override void FinalizeAndValidate()
	{
		Title = Arguments;
		Link = Properties.GetValueOrDefault("link");
		//TODO Render
		Header = Properties.GetValueOrDefault("header");
		Footer = Properties.GetValueOrDefault("footer");
	}
}
