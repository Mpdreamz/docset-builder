namespace Elastic.Markdown.Myst.Directives;

public class ImageBlock(DirectiveBlockParser blockParser, Dictionary<string, string> properties)
	: DirectiveBlock(blockParser, properties)
{

	/// <summary>
	/// Alternate text: a short description of the image, displayed by applications that cannot display images,
	/// or spoken by applications for visually impaired users.
	/// </summary>
	public string? Alt { get; set; }

	/// <summary>
	/// The desired height of the image. Used to reserve space or scale the image vertically. When the “scale” option
	/// is also specified, they are combined. For example, a height of 200px and a scale of 50 is equivalent to
	/// a height of 100px with no scale.
	/// </summary>
	public string? Height { get; set; }

	/// <summary>
	/// The width of the image. Used to reserve space or scale the image horizontally. As with “height” above,
	/// when the “scale” option is also specified, they are combined.
	/// </summary>
	public string? Width { get; set; }

	/// <summary>
	/// The uniform scaling factor of the image. The default is “100 %”, i.e. no scaling.
	/// </summary>
	public string? Scale { get; set; }

	/// <summary>
	/// The values “top”, “middle”, and “bottom” control an image’s vertical alignment
	/// The values “left”, “center”, and “right” control an image’s horizontal alignment, allowing the image to float
	/// and have the text flow around it.
	/// </summary>
	public string? Align { get; set; }

	/// <summary>
	/// Makes the image into a hyperlink reference (“clickable”).
	/// </summary>
	public string? Target { get; set; }

	/// <summary>
	/// A space-separated list of CSS classes to add to the image.
	/// </summary>
	public string? Classes { get; protected set; }

	/// <summary>
	/// A reference target for the admonition (see cross-referencing).
	/// </summary>
	public string? CrossReferenceName  { get; private set; }

	public string? ImageUrl { get; private set; }


	public override void FinalizeAndValidate()
	{
		ImageUrl = Arguments; //todo validate
		Classes = Properties.GetValueOrDefault("class");
		CrossReferenceName = Properties.GetValueOrDefault("name");
		Alt = Properties.GetValueOrDefault("alt");
		Height = Properties.GetValueOrDefault("height");
		Width = Properties.GetValueOrDefault("width");
		Scale = Properties.GetValueOrDefault("scale");
		Align = Properties.GetValueOrDefault("align");
		Target = Properties.GetValueOrDefault("target");
	}
}


public class FigureBlock(DirectiveBlockParser blockParser, Dictionary<string, string> properties)
	: ImageBlock(blockParser, properties);
