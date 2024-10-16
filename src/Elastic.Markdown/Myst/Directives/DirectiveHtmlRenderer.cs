// Copyright (c) Alexandre Mutel. All rights reserved.
// This file is licensed under the BSD-Clause 2 license.
// See the license.txt file in the project root for more information.

using Elastic.Markdown.Slices.Directives;
using Markdig.Extensions.Figures;
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
			case FigureBlock imageBlock:
				WriteFigure(renderer, imageBlock);
				return;
			case ImageBlock imageBlock:
				WriteImage(renderer, imageBlock);
				return;
			case DropdownBlock dropdownBlock:
				WriteDropdown(renderer, dropdownBlock);
				return;
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
			case GridBlock grid:
				WriteGrid(renderer, grid);
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

	private void WriteImage(HtmlRenderer renderer, ImageBlock block)
	{
		var slice = Image.Create(new ImageViewModel
		{
			Classes = block.Classes,
			CrossReferenceName = block.CrossReferenceName,
			Align = block.Align,
			Alt = block.Alt,
			Height = block.Height,
			Scale = block.Scale,
			Target = block.Target,
			Width = block.Width,
			ImageUrl = block.ImageUrl,
		});
		RenderRazorSlice(slice, renderer, block);
	}

	private void WriteFigure(HtmlRenderer renderer, ImageBlock block)
	{
		var slice = Slices.Directives.Figure.Create(new ImageViewModel
		{
			Classes = block.Classes,
			CrossReferenceName = block.CrossReferenceName,
			Align = block.Align,
			Alt = block.Alt,
			Height = block.Height,
			Scale = block.Scale,
			Target = block.Target,
			Width = block.Width,
			ImageUrl = block.ImageUrl,
		});
		RenderRazorSlice(slice, renderer, block);
	}

	private void WriteChildren(HtmlRenderer renderer, DirectiveBlock directiveBlock) =>
		renderer.WriteChildren(directiveBlock);

	private void WriteCard(HtmlRenderer renderer, CardBlock block)
	{
		var slice = Card.Create(new CardViewModel { Title = block.Title, Link = block.Link });
		RenderRazorSlice(slice, renderer, block, implicitParagraph: false);
	}

	private void WriteGrid(HtmlRenderer renderer, GridBlock block)
	{
		var slice = Grid.Create(new GridViewModel
		{
			BreakPoint = block.BreakPoint
		});
		RenderRazorSlice(slice, renderer, block);
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
			Title = block.Title,
			Open = block.DropdownOpen.GetValueOrDefault() ? "open" : null
		});
		RenderRazorSlice(slice, renderer, block);
	}

	private void WriteDropdown(HtmlRenderer renderer, DropdownBlock block)
	{
		var slice = Dropdown.Create(new AdmonitionViewModel
		{
			Directive = block.Admonition,
			CrossReferenceName = block.CrossReferenceName,
			Classes = block.Classes,
			Title = block.Title,
			Open = block.DropdownOpen.GetValueOrDefault() ? "open" : null
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

	private void WriteTabSet(HtmlRenderer renderer, TabSetBlock block)
	{
		var slice = TabSet.Create(new TabSetViewModel());
		RenderRazorSlice(slice, renderer, block);
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
