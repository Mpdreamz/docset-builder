// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Elastic.Markdown.Myst;

[YamlStaticContext]
public partial class YamlFrontMatterStaticContext;

[YamlSerializable]
public class YamlFrontMatter
{
	public string? Title { get; set; }
	[YamlMember(Alias = "sub")]
	public Dictionary<string, string>? Properties { get; set; }
}

public class FrontMatterParser
{
	public YamlFrontMatter Deserialize(string yaml)
	{
		var input = new StringReader(yaml);

		var deserializer = new StaticDeserializerBuilder(new YamlFrontMatterStaticContext())
			.IgnoreUnmatchedProperties()
			.WithNamingConvention(CamelCaseNamingConvention.Instance)
			.Build();

		var frontMatter = deserializer.Deserialize<YamlFrontMatter>(input);
		return frontMatter;

	}
}
