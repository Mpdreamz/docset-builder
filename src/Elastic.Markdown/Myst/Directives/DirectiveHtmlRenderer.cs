// Copyright (c) Alexandre Mutel. All rights reserved.
// This file is licensed under the BSD-Clause 2 license.
// See the license.txt file in the project root for more information.

using Elastic.Markdown.Slices.Directives;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using RazorSlices;

namespace Elastic.Markdown.Myst.Directives;

/// <summary>
/// An HTML renderer for a <see cref="DirectiveBlock"/>.
/// </summary>
/// <seealso cref="HtmlObjectRenderer{CustomContainer}" />
public class DirectiveHtmlRenderer : HtmlObjectRenderer<DirectiveBlock>
{
	protected override void Write(HtmlRenderer renderer, DirectiveBlock directiveBlock)
	{
		if (directiveBlock is TocTreeBlock)
			return;

		renderer.EnsureLine();

		switch (directiveBlock)
		{
			case AdmonitionBlock admonitionBlock:
				WriteAdmonition(renderer, admonitionBlock);
				return;
			case VersionBlock versionBlock:
				WriteVersion(renderer, versionBlock);
				return;
			case CodeBlock codeBlock:
				WriteCode(renderer, codeBlock);
				return;
			case SideBarBlock sideBar:
				WriteSideBar(renderer, sideBar);
				return;
			case TabSetBlock tabSet:
				WriteTabSet(renderer, tabSet);
				return;
			case TabItemBlock tabItem:
				WriteTabItem(renderer, tabItem);
				return;
			case CardBlock card:
				WriteCard(renderer, card);
				return;
			case GridItemCardBlock gridItemCard:
				WriteGridItemCard(renderer, gridItemCard);
				return;
			default:
				// if (!string.IsNullOrEmpty(directiveBlock.Info) && !directiveBlock.Info.StartsWith('{'))
				// 	WriteCode(renderer, directiveBlock);
				// else if (!string.IsNullOrEmpty(directiveBlock.Info))
				// 	WriteAdmonition(renderer, directiveBlock);
				// else
				WriteChildren(renderer, directiveBlock);
				break;
		}
	}

	private void WriteChildren(HtmlRenderer renderer, DirectiveBlock directiveBlock) =>
		renderer.WriteChildren(directiveBlock);

	private void WriteCard(HtmlRenderer renderer, CardBlock directiveBlock)
	{
		var title = directiveBlock.Arguments;
		var link = directiveBlock.Properties.GetValueOrDefault("link");
		var slice = Card.Create(new CardViewModel { Title = title, Link = link });
		RenderRazorSlice(slice, renderer, directiveBlock, implicitParagraph: false);
	}

	private void WriteGrid(HtmlRenderer renderer, GridBlock directiveBlock)
	{
		//todo we always assume 4 integers
		var columns = directiveBlock.Arguments?.Split(' ')
			.Select(t => int.TryParse(t, out var c) ? c : 0).ToArray() ?? [];
		// 1 1 2 3
		int xs = 1, sm = 1, md = 2, lg = 3;
		if (columns.Length >= 4)
		{
			xs = columns[0];
			sm = columns[1];
			md = columns[2];
			lg = columns[3];
		}

		var slice = Grid.Create(new GridViewModel
		{
			BreakPointLg = lg, BreakPointMd = md, BreakPointSm = sm, BreakPointXs = xs
		});
		RenderRazorSlice(slice, renderer, directiveBlock);
	}

	private void WriteGridItemCard(HtmlRenderer renderer, GridItemCardBlock directiveBlock)
	{
		var title = directiveBlock.Arguments;
		var link = directiveBlock.Properties.GetValueOrDefault("link");
		var slice = GridItemCard.Create(new GridItemCardViewModel { Title = title, Link = link });
		RenderRazorSlice(slice, renderer, directiveBlock);
	}


	private void WriteVersion(HtmlRenderer renderer, VersionBlock block)
	{
		var slice = Slices.Directives.Version.Create(new VersionViewModel
		{
			Directive = block.Directive, Title = block.Title, VersionClass = block.Class
		});
		RenderRazorSlice(slice, renderer, block);
	}

	private void WriteAdmonition(HtmlRenderer renderer, AdmonitionBlock block)
	{
		var slice = Admonition.Create(new AdmonitionViewModel
		{
			Directive = block.Admonition,
			CrossReferenceName = block.CrossReferenceName,
			Classes = block.Classes,
			Title = block.Title
		});
		RenderRazorSlice(slice, renderer, block);
	}

	private void WriteCode(HtmlRenderer renderer, CodeBlock block)
	{
		var slice = Code.Create(new CodeViewModel
		{
			CrossReferenceName = block.CrossReferenceName, Language = block.Language, Caption = block.Caption
		});
		RenderRazorSlice(slice, renderer, block);
	}


	private void WriteSideBar(HtmlRenderer renderer, SideBarBlock directiveBlock)
	{
		var slice = SideBar.Create(new SideBarViewModel());
		RenderRazorSlice(slice, renderer, directiveBlock);
	}

	private int _seenTabSets;

	private void WriteTabSet(HtmlRenderer renderer, TabSetBlock block)
	{
		var slice = TabSet.Create(new TabSetViewModel());
		RenderRazorSlice(slice, renderer, block);
		_seenTabSets++;
	}

	private void WriteTabItem(HtmlRenderer renderer, TabItemBlock block)
	{
		var slice = TabItem.Create(new TabItemViewModel { Index = block.Index, Title = block.Title, TabSetIndex = block.TabSetIndex });
		RenderRazorSlice(slice, renderer, block);
	}

	private static void RenderRazorSlice<T>(
		RazorSlice<T> slice, HtmlRenderer renderer, DirectiveBlock obj, bool implicitParagraph = true)
	{
		var html = slice.RenderAsync().GetAwaiter().GetResult();
		var blocks = html.Split("[CONTENT]", 2, StringSplitOptions.RemoveEmptyEntries);
		renderer.Write(blocks[0]);
		renderer.WriteChildren(obj);
		renderer.Write(blocks[1]);
	}
}
