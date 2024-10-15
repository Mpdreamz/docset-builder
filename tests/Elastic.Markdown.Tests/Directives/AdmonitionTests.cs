using Elastic.Markdown.Myst.Directives;
using FluentAssertions;

namespace Elastic.Markdown.Tests.Directives;

public abstract class AdmonitionTests(string directive) : DirectiveTest<AdmonitionBlock>(
// language=markdown
$$"""
```{{{directive}}}
This is an attention block
```
A regular paragraph.
"""
)
{
	[Fact]
	public void ParsesAdmonitionBlock() => Block.Should().NotBeNull();

	[Fact]
	public void SetsCorrectAdmonitionType() => Block!.Admonition.Should().Be(directive);
}

public class AttentionTests() : AdmonitionTests("attention")
{
	[Fact]
	public void SetsTitle() => Block!.Title.Should().Be("Attention");
}
public class CautionTests() : AdmonitionTests("caution")
{
	[Fact]
	public void SetsTitle() => Block!.Title.Should().Be("Caution");
}
public class DangerTests() : AdmonitionTests("danger")
{
	[Fact]
	public void SetsTitle() => Block!.Title.Should().Be("Danger");
}
public class ErrorTests() : AdmonitionTests("error")
{
	[Fact]
	public void SetsTitle() => Block!.Title.Should().Be("Error");
}
public class HintTests() : AdmonitionTests("hint")
{
	[Fact]
	public void SetsTitle() => Block!.Title.Should().Be("Hint");
}
public class ImportantTests() : AdmonitionTests("important")
{
	[Fact]
	public void SetsTitle() => Block!.Title.Should().Be("Important");
}
public class NoteTests() : AdmonitionTests("note")
{
	[Fact]
	public void SetsTitle() => Block!.Title.Should().Be("Note");
}
public class SeeAlsoTests() : AdmonitionTests("seealso")
{
	[Fact]
	public void SetsTitle() => Block!.Title.Should().Be("See Also");
}
public class TipTests() : AdmonitionTests("tip")
{
	[Fact]
	public void SetsTitle() => Block!.Title.Should().Be("Tip");
}

public class NoteTitleTests() : DirectiveTest<AdmonitionBlock>(
// language=markdown
"""
```{note} This is my custom note
This is an attention block
```
A regular paragraph.
"""
)
{
	[Fact]
	public void SetsCorrectAdmonitionType() => Block!.Admonition.Should().Be("note");

	[Fact]
	public void SetsCustomTitle() => Block!.Title.Should().Be("Note This is my custom note");
}

public class AdmonitionTitleTests() : DirectiveTest<AdmonitionBlock>(
// language=markdown
"""
```{admonition} This is my custom note
This is an attention block
```
A regular paragraph.
"""
)
{
	[Fact]
	public void SetsCorrectAdmonitionType() => Block!.Admonition.Should().Be("note");

	[Fact]
	public void SetsCustomTitle() => Block!.Title.Should().Be("This is my custom note");
}


