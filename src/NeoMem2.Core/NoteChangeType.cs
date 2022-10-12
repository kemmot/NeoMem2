// <copyright file="NoteChangeType.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core
{
    /// <summary>
    /// The supported note change types.
    /// </summary>
    public enum NoteChangeType
    {
        /// <summary>
        /// A note was inserted.
        /// </summary>
        Inserted = 1,

        /// <summary>
        /// A note was updated.
        /// </summary>
        Updated = 2,

        /// <summary>
        /// A note was deleted.
        /// </summary>
        Deleted = 3
    }
}