using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AsciiDocNet
{
    /// <summary>
    /// Delimited block substitutions
    /// </summary>
    public enum DelimitedBlockSubstitutions
	{
        /// <summary>
        /// none
        /// </summary>
        None,
        /// <summary>
        /// attributes
        /// </summary>
        Attributes,
        /// <summary>
        /// callouts
        /// </summary>
        Callouts,
        /// <summary>
        /// macros
        /// </summary>
        Macros,
        /// <summary>
        /// quotes
        /// </summary>
        Quotes,
        /// <summary>
        /// replacements
        /// </summary>
        Replacements,
        /// <summary>
        /// special characters
        /// </summary>
        [Display(Name = "specialchars")]
        SpecialCharacters,
        /// <summary>
        /// special words
        /// </summary>
        SpecialWords
    }
}
