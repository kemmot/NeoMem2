// <copyright file="NLogExtensions.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Utils
{
    using System;

    using NLog;

    /// <summary>
    /// Provides static functionality for use with NLog.
    /// </summary>
    public static class NLogExtensions
    {
        /// <summary>
        /// Pushes formatted text onto the current thread nested diagnostic context stack.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">The objects to format.</param>
        /// <returns>An object that allows this context to be popped from the stack in a finally block.</returns>
        /// <example>
        /// The following shows this method being used in a using block:
        /// <code>
        /// for (int i = 0; i &lt; 10; i++)
        /// {
        ///     using (IDisposable a = NLogExtensions.Push("Context: ", i))
        ///     {
        ///         // do some logging here
        ///     }
        /// }
        /// </code>
        /// </example>
        public static IDisposable Push(string format, params object[] args)
        {
            return NestedDiagnosticsContext.Push(string.Format(format, args));
        }
    }
}
