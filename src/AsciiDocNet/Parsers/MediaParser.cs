using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;

namespace AsciiDocNet
{
    public class MediaParser : ProcessBufferParserBase
    {
        public override bool IsMatch(IDocumentReader reader, Container container, AttributeList attributes) =>
            PatternMatcher.Media.IsMatch(reader.Line);

        public override void InternalParse(Container container, IDocumentReader reader, Func<string, bool> predicate, ref List<string> buffer,
            ref AttributeList attributes)
        {
            var match = PatternMatcher.Media.Match(reader.Line);
            if (!match.Success)
            {
                throw new ArgumentException("not a media");
            }

            var path = match.Groups["path"].Value;
            Media media;

            switch (match.Groups["media"].Value.ToLowerInvariant())
            {
                case "image":
                    media = new Image(path);
                    break;
                case "video":
                    media = new Video(path);
                    break;
                case "audio":
                    media = new Audio(path);
                    break;
                default:
                    throw new ArgumentException("unrecognized media type");
            }

            media.Attributes.Add(attributes);
            var attributesValue = match.Groups["attributes"].Value;
            string width = null;
            string height = null;

            if (!string.IsNullOrEmpty(attributesValue))
            {
                var attributeValues = SplitOnCharacterOutsideQuotes(attributesValue);

                for (int index = 0; index < attributeValues.Length; index++)
                {
                    var attributeValue = attributeValues[index];
                    int dimension;

                    if (index == 0)
                    {
                        media.AlternateText = attributeValue;
                    }
                    else if (index == 1 && int.TryParse(attributeValue, out dimension))
                    {
                        width = dimension.ToString(CultureInfo.InvariantCulture);
                    }
                    else if (index == 2 && int.TryParse(attributeValue, out dimension))
                    {
                        height = dimension.ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        var attributeMatch = PatternMatcher.AttributeNameValue.Match(attributeValue);

                        if (attributeMatch.Success)
                        {
	                        var tokens = attributeValue.Split(['='], 2, StringSplitOptions.RemoveEmptyEntries);
	                        var name = tokens[0].ToLowerInvariant();
	                        if (tokens.Length == 1)
	                        {

	                        }

	                        if (tokens.Length == 2)
	                        {
								var value = tokens[1].Trim('\"');
								switch (name)
								{
									case "link":
										media.Link = value;
										break;
									case "title":
										media.Title = value;
										break;
									case "float":
										media.Float = value;
										break;
									case "align":
										media.Align = value;
										break;
									case "role":
										media.Role = value;
										break;
									case "width":
										width = value;
										break;
									case "size":
										width = value;
										height = value;
										break;
									case "opts":
										foreach(var opt in value.Split(','))
											switch (opt.ToLowerInvariant().Trim())
											{
												case "inline":
													media.Inline = true;
													break;
												default:
													throw new NotImplementedException(
														$"TODO: add opts '{opt}' with value: '{value}' parsing {attributeValue}"
													);
											}
										media.Role = value;
										break;
									default:
										throw new NotImplementedException(
											$"TODO: add attribute '{name}' with value: '{value}' parsing {attributeValue}");
								}

	                        }
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(width) && !string.IsNullOrEmpty(height))
            {
                media.SetWidthAndHeight(width, height);
            }

            container.Add(media);
            attributes = null;

            reader.ReadLine();
        }

        private string[] SplitOnCharacterOutsideQuotes(string input, char character = ',')
        {
            var output = new List<string>();
            if (string.IsNullOrEmpty(input))
            {
                return output.ToArray();
            }

            var start = 0;
            var inDoubleQuotes = false;
            var inSingleQuotes = false;

            for (var index = 0; index < input.Length; index++)
            {
                var currentChar = input[index];
                if (currentChar == '"' && !inSingleQuotes)
                {
                    inDoubleQuotes = !inDoubleQuotes;
                }
                else if (currentChar == '\'' && !inDoubleQuotes)
                {
                    inSingleQuotes = !inSingleQuotes;
                }

                var atLastChar = index == input.Length - 1;

                if (atLastChar)
                {
                    output.Add(input.Substring(start));
                }
                else if (currentChar == character && !inDoubleQuotes && !inSingleQuotes)
                {
                    output.Add(input.Substring(start, index - start));
                    start = index + 1;
                }
            }

            return output.ToArray();
        }
    }
}
