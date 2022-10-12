// <copyright file="TrackedProperty.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Utils.ComponentModel
{
    using NLog;

    /// <summary>
    /// Represents a property that can be change tracked.
    /// </summary>
    public class TrackedProperty : NotifyPropertyChangedBase
    {
        /// <summary>
        /// The logger to use.
        /// </summary>
        protected static readonly Logger Logger = LogManager.GetLogger(typeof(TrackedProperty).FullName);
    }
}
