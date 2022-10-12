// <copyright file="TextFormat.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core
{
    using System.ComponentModel;

    /// <summary>
    /// The supported text formats for a note.
    /// </summary>
    public enum TextFormat
    {
        /// <summary>
        /// The text is in mark down format.
        /// </summary>
        [Description("Markdown")]
        Markdown = 1,

        /// <summary>
        /// The text is in rich text format.
        /// </summary>
        [Description("Rich Text")]
        Rtf = 3,

        /// <summary>
        /// The text is in plain text format.
        /// </summary>
        [Description("Text Document")]
        Txt = 4,

        /// <summary>
        /// The text is CPP source code.
        /// </summary>
        [Description("CPP")]
        SourceCodeCPP = 5,

        /// <summary>
        /// The text is C# source code.
        /// </summary>
        [Description("C#")]
        SourceCodeCSharp = 6,

        /// <summary>
        /// The text is CSS source code.
        /// </summary>
        [Description("CSS")]
        SourceCodeCSS = 7,

        /// <summary>
        /// The text is DOSBatch source code.
        /// </summary>
        [Description("DOSBatch")]
        SourceCodeDOSBatch = 8,

        /// <summary>
        /// The text is HTML source code.
        /// </summary>
        [Description("HTML")]
        SourceCodeHTML = 9,

        /// <summary>
        /// The text is Java source code.
        /// </summary>
        [Description("Java")]
        SourceCodeJava = 10,

        /// <summary>
        /// The text is JavaScript source code.
        /// </summary>
        [Description("JavaScript")]
        SourceCodeJavaScript = 11,

        /// <summary>
        /// The text is PHP source code.
        /// </summary>
        [Description("PHP")]
        SourceCodePHP = 12,

        /// <summary>
        /// The text is Perl source code.
        /// </summary>
        [Description("Perl")]
        SourceCodePerl = 13,

        /// <summary>
        /// The text is Python source code.
        /// </summary>
        [Description("Python")]
        SourceCodePython = 14,

        /// <summary>
        /// The text is Transact SQL source code.
        /// </summary>
        [Description("T-SQL")]
        SourceCodeTSQL = 15,

        /// <summary>
        /// The text is XML source code.
        /// </summary>
        [Description("XML")]
        SourceCodeXML = 16
    }
}