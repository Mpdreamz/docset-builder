namespace Elastic.Markdown.Myst.Directives;

public class AdmonitionBlock(DirectiveBlockParser blockParser, string admonition, Dictionary<string, string> properties)
	: DirectiveBlock(blockParser, properties)
{
	public string Admonition => admonition == "admonition" ? Classes ?? "note" : admonition;
	public string? Classes { get; private set; }
	public string? CrossReferenceName  { get; private set; }

	public string Title
	{
		get
		{
			var t = Admonition == "seealso" ? "see also" : Admonition;
			var title = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(t);
			if (admonition == "admonition" && !string.IsNullOrEmpty(Arguments))
				title = Arguments;
			else if (!string.IsNullOrEmpty(Arguments))
				title += $" {Arguments}";
			return title;
		}
	}

	public override void FinalizeAndValidate()
	{
		Classes = Properties.GetValueOrDefault("class");
		CrossReferenceName = Properties.GetValueOrDefault("name");
	}
}
