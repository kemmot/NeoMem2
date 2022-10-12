// <copyright file="StringBuilderExtensions.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Utils.Text
{
    using System.Text;

    /// <summary>
    /// Provides extension methods for the <see cref="StringBuilder"/> class.
    /// </summary>
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Appends a formatted line to the string builder.
        /// </summary>
        /// <param name="sb">The string builder to append to.</param>
        /// <param name="format">The format of text to append.</param>
        /// <param name="args">The formatting arguments.</param>
        public static void AppendLine(this StringBuilder sb, string format, params object[] args)
        {
            sb.AppendLine(string.Format(format, args));
        }
    }
}
