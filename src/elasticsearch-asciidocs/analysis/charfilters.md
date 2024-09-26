## Character filters reference

_Character filters_ are used to preprocess the stream of characters before it
is passed to the [tokenizer](#analysis-tokenizers).

A character filter receives the original text as a stream of characters and
can transform the stream by adding, removing, or changing characters. For
instance, a character filter could be used to convert Hindu-Arabic numerals
(٠‎١٢٣٤٥٦٧٨‎٩‎) into their Arabic-Latin equivalents (0123456789), or to strip HTML
elements like `<b>` from the stream.

Elasticsearch has a number of built in character filters which can be used to build
[custom analyzers](#analysis-custom-analyzer).

* **[HTML Strip Character Filter](#analysis-htmlstrip-charfilter)**

The `html_strip` character filter strips out HTML elements like `<b>` and
decodes HTML entities like `&amp;`.

* **[Mapping Character Filter](#analysis-mapping-charfilter)**

The `mapping` character filter replaces any occurrences of the specified
strings with the specified replacements.

* **[Pattern Replace Character Filter](#analysis-pattern-replace-charfilter)**

The `pattern_replace` character filter replaces any characters matching a
regular expression with the specified replacement.
