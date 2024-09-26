using System.Text;
using AsciiDocNet;
using Attribute = AsciiDocNet.Attribute;

public class IncludesToMarkdownVisitor : AsciiDocVisitor, IDocumentVisitor
{
	private readonly StringWriter _writer;

	public IncludesToMarkdownVisitor(StringWriter writer, bool debug = false) : base(writer, debug) => _writer = writer;

// ReSharper disable RedundantOverriddenMember
	public override void VisitAdmonition(Admonition admonition) => base.VisitAdmonition(admonition);

	public override void VisitAnchor(Anchor anchor) => base.VisitAnchor(anchor);

	public override void VisitAttribute(Attribute attribute) => base.VisitAttribute(attribute);

	public override void VisitAttributeEntry(AttributeEntry attributeEntry) => base.VisitAttributeEntry(attributeEntry);

	public override void VisitAttributeList(AttributeList attributes) => base.VisitAttributeList(attributes);

	public override void VisitAttributeReference(AttributeReference reference) =>
		base.VisitAttributeReference(reference);

	public override void VisitAudio(Audio audio) => base.VisitAudio(audio);

	public override void VisitAuthorInfo(AuthorInfo author) => base.VisitAuthorInfo(author);

	public override void VisitAuthorInfos(IList<AuthorInfo> authors) => base.VisitAuthorInfos(authors);

	public override void VisitStrong(Strong strong) => base.VisitStrong(strong);

	public override void VisitCheckListItem(CheckListItem listItem) => base.VisitCheckListItem(listItem);

	public override void VisitDocument(Document document) => base.VisitDocument(document);

	public override void VisitMark(Mark mark) => base.VisitMark(mark);

	public override void VisitContainer(Container container) => base.VisitContainer(container);

	public override void VisitInlineContainer(InlineContainer inlineContainer) =>
		base.VisitInlineContainer(inlineContainer);

	public override void VisitImage(Image image) => base.VisitImage(image);

	public override void VisitInclude(Include include)
	{
		if (include == null) return;
		var attributes = new StringBuilder();
		if (include.LevelOffset.HasValue)
		{
			attributes.Append($"leveloffset={include.LevelOffset},");
		}

		if (!string.IsNullOrEmpty(include.Lines))
		{
			attributes.Append($"lines=\"{include.Lines}\",");
		}

		if (!string.IsNullOrEmpty(include.Tags))
		{
			attributes.Append($"tags=\"{include.Tags}\",");
		}

		if (include.Indent.HasValue)
		{
			attributes.Append($"indent={include.Indent},");
		}

		_writer.WriteLine("[!INCLUDE []({0})<{1}>]", include.Path,
			attributes.ToString(0, Math.Max(0, attributes.Length - 1)));
		base.VisitInclude(include);
	}

	public override void VisitEmphasis(Emphasis emphasis) => base.VisitEmphasis(emphasis);

	public override void VisitLabeledListItem(LabeledListItem listItem) => base.VisitLabeledListItem(listItem);

	public override void VisitLabeledList(LabeledList list) => base.VisitLabeledList(list);

	public override void VisitLink(Link link) => base.VisitLink(link);

	public override void VisitListing(Listing listing) => base.VisitListing(listing);

	public override void VisitCallout(Callout callout) => base.VisitCallout(callout);

	public override void VisitUnorderedListItem(UnorderedListItem listItem) => base.VisitUnorderedListItem(listItem);

	public override void VisitUnorderedList(UnorderedList list) => base.VisitUnorderedList(list);

	public override void VisitTextLiteral(TextLiteral text) => base.VisitTextLiteral(text);

	public override void VisitLiteral(Literal literal) => base.VisitLiteral(literal);

	public override void VisitMedia(Media media) => base.VisitMedia(media);

	public override void VisitMonospace(Monospace monospace) => base.VisitMonospace(monospace);

	public override void VisitNamedAttribute(NamedAttribute attribute) => base.VisitNamedAttribute(attribute);

	public override void VisitOpen(Open open) => base.VisitOpen(open);

	public override void VisitOrderedListItem(OrderedListItem listItem) => base.VisitOrderedListItem(listItem);

	public override void VisitOrderedList(OrderedList list) => base.VisitOrderedList(list);

	public override void VisitParagraph(Paragraph paragraph) => base.VisitParagraph(paragraph);

	public override void VisitQuotationMark(QuotationMark quotation) => base.VisitQuotationMark(quotation);

	public override void VisitQuote(Quote quote) => base.VisitQuote(quote);

	public override void VisitSectionTitle(SectionTitle sectionTitle)
	{
		//if (sectionTitle == null) return;
		VisitAttributeList(sectionTitle.Attributes);
		_writer.Write("{0} ", new string('#', sectionTitle.Level + 1));
		VisitInlineContainer(sectionTitle);
		_writer.WriteLine();
		_writer.WriteLine();
	}

	public override void VisitSource(Source source) => base.VisitSource(source);

	public override void VisitTitle(Title title) => base.VisitTitle(title);

	public override void VisitUnsetAttributeEntry(UnsetAttributeEntry attributeEntry) =>
		base.VisitUnsetAttributeEntry(attributeEntry);

	public override void VisitVideo(Video video) => base.VisitVideo(video);

	public override void VisitExample(Example example) => base.VisitExample(example);

	public override void VisitComment(Comment comment) => base.VisitComment(comment);

	public override void VisitFenced(Fenced fenced) => base.VisitFenced(fenced);

	public override void VisitPassthrough(Passthrough passthrough) => base.VisitPassthrough(passthrough);

	public override void VisitSidebar(Sidebar sidebar) => base.VisitSidebar(sidebar);

	public override void VisitTable(Table table) => base.VisitTable(table);

	public override void VisitDocumentTitle(DocumentTitle title) => base.VisitDocumentTitle(title);

	public override void VisitInternalAnchor(InternalAnchor anchor) => base.VisitInternalAnchor(anchor);

	public override void VisitInlineAnchor(InlineAnchor anchor) => base.VisitInlineAnchor(anchor);

	public override void VisitStem(Stem stem) => base.VisitStem(stem);

	public override void VisitVerse(Verse verse) => base.VisitVerse(verse);

	public override void VisitSubscript(Subscript subscript) => base.VisitSubscript(subscript);

	public override void VisitSuperscript(Superscript superscript) => base.VisitSuperscript(superscript);
// ReSharper restore RedundantOverriddenMember
}
