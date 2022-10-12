// <copyright file="Tag.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core
{
    using System;

    using NeoMem2.Utils.ComponentModel;
    using NeoMem2.Utils.ComponentModel.Generic;

    /// <summary>
    /// Represents a tag that can be assigned to a <see cref="Note"/>.
    /// </summary>
    public class Tag : ChangeTrackedBase
    {
        /// <summary>
        /// The name of the <see cref="Id"/> property.
        /// </summary>
        public const string PropertyNameId = "Id";

        /// <summary>
        /// The name of the <see cref="IsDeleted"/> property.
        /// </summary>
        public const string PropertyNameIsDeleted = "IsDeleted";

        /// <summary>
        /// The name of the <see cref="Name"/> property.
        /// </summary>
        public const string PropertyNameName = "Name";

        /// <summary>
        /// The backing field for the <see cref="Id"/> property.
        /// </summary>
        private readonly TrackedProperty<int> id = new TrackedProperty<int>(PropertyNameId, 0);

        /// <summary>
        /// The backing field for the <see cref="IsDeleted"/> property.
        /// </summary>
        private readonly TrackedProperty<bool> isDeleted = new TrackedProperty<bool>(PropertyNameIsDeleted);

        /// <summary>
        /// The backing field for the <see cref="Name"/> property.
        /// </summary>
        private readonly TrackedProperty<string> name = new TrackedProperty<string>(PropertyNameName, string.Empty);

        /// <summary>
        /// Initializes a new instance of the <see cref="Tag" /> class.
        /// </summary>
        /// <param name="id">The unique identifier for this tag.</param>
        /// <param name="name">The name of this tag.</param>
        public Tag(int id, string name)
            : this(name)
        {
            this.Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tag" /> class.
        /// </summary>
        /// <param name="name">The name of this tag.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if a required argument has an invalid value.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if a required argument has a null reference.
        /// </exception>
        public Tag(string name)
            : base("Tag")
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (name.Length == 0)
            {
                throw new ArgumentException("Argument cannot be zero length", "name");
            }

            this.Name = name;

            this.PropertyHolder.Register(this.id);
            this.PropertyHolder.Register(this.isDeleted);
            this.PropertyHolder.Register(this.name);
        }

        /// <summary>
        /// Gets or sets the unique identifier of this tag.
        /// </summary>
        public int Id
        {
            get { return this.id; } 
            set { this.id.Value = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this tag has been deleted.
        /// </summary>
        public bool IsDeleted
        {
            get { return this.isDeleted; } 
            set { this.isDeleted.Value = value; }
        }

        /// <summary>
        /// Gets or sets the name of this tag.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name.Value = value; }
        }

        /// <summary>
        /// Detaches from its original storage so that it appears like a new note to be imported into another store.
        /// </summary>
        public void Detach()
        {
            this.Id = 0;
            ClearIsDirty();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
