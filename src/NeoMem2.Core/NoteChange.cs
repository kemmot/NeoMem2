// <copyright file="NoteChange.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core
{
    using System;

    /// <summary>
    /// Represents a single note change for auditing.
    /// </summary>
    public class NoteChange
    {
        /// <summary>
        /// Gets or sets the type of change.
        /// </summary>
        public NoteChangeType ChangeType { get; set; }

        /// <summary>
        /// Gets or sets the note that changed.
        /// </summary>
        public Note Note { get; set; }

        /// <summary>
        /// Gets or sets the name of the note field that changed.
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the date that the change occurred.
        /// </summary>
        public DateTime ChangeDate { get; set; }

        /// <summary>
        /// Gets or sets the new value of the field after the change.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets a string representation of the new value of the field after the change.
        /// </summary>
        public string ValueString { get; set; }
    }
}
