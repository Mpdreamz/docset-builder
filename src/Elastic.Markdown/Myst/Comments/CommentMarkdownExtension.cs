using Elastic.Markdown.Myst.Comments;
using Markdig;
using Markdig.Parsers;
using Markdig.Renderers;

namespace Elastic.Markdown.Myst;

public static class CommentBuilderExtensions
{
	public static MarkdownPipelineBuilder UseComments(this MarkdownPipelineBuilder pipeline)
	{
		pipeline.Extensions.AddIfNotAlready<CommentMarkdownExtension>();
		return pipeline;
	}

	/// <summary>
	/// Modifies the built in generic attributes parser to only apply to block elements.
	/// </summary>
	/// <param name="pipeline"></param>
	/// <returns></returns>
    public static MarkdownPipelineBuilder UseBlockGenericAttributes(this MarkdownPipelineBuilder pipeline)
    {
        pipeline.Extensions.AddIfNotAlready<BlockGenericAttributesExtension>();
        return pipeline;
    }
}

public class CommentMarkdownExtension : IMarkdownExtension
{
	public void Setup(MarkdownPipelineBuilder pipeline)
	{
		if (!pipeline.BlockParsers.Contains<CommentBlockParser>())
			pipeline.BlockParsers.InsertBefore<ThematicBreakParser>(new CommentBlockParser());
	}

	public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
	{
		if (!renderer.ObjectRenderers.Contains<CommentRenderer>())
			renderer.ObjectRenderers.InsertBefore<SectionedHeadingRenderer>(new CommentRenderer());
	}
}
