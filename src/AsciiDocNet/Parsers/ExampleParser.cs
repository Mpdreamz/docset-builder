namespace AsciiDocNet
{
    public class ExampleParser : BlockParserBase<Example>
    {
	    public override bool IsMatch(IDocumentReader reader, Container container, AttributeList attributes)
	    {
		    var match = PatternMatcher.Example.IsMatch(reader.Line);
		    if (match)
		    {

		    }

		    return match;
	    }
    }
}
