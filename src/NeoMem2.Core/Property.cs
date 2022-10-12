// <copyright file="Property.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core
{
    using System;
    using System.ComponentModel;
    using NeoMem2.Utils;
    using NeoMem2.Utils.ComponentModel;

    /// <summary>
    /// Represents a single note property.
    /// </summary>
    public class Property : NotifyPropertyChangedBase, ICloneable<Property>
    {
        /// <summary>
        /// The name of the <see cref="Value"/> property.
        /// </summary>
        public const string PropertyNameValue = "Value";
        
        /// <summary>
        /// The backing field for the <see cref="IsDeleted"/> property.
        /// </summary>
        private bool isDeleted;

        /// <summary>
        /// The backing field for the <see cref="Value"/> property.
        /// </summary>
        private string value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Property" /> class.
        /// </summary>
        /// <param name="id">The unique identifier of this property.</param>
        /// <param name="name">The name of this property.</param>
        /// <param name="value">The value of this property.</param>
        /// <param name="isSystemProperty">Whether this property is a system property.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if a required argument has an invalid value.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if a required argument has a null reference.
        /// </exception>
        public Property(long id, string name, object value, bool isSystemProperty = false)
            : this(name, value, isSystemProperty)
        {
            this.Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Property" /> class.
        /// </summary>
        /// <param name="name">The name of this property.</param>
        /// <param name="value">The value of this property.</param>
        /// <param name="isSystemProperty">Whether this property is a system property.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if a required argument has an invalid value.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if a required argument has a null reference.
        /// </exception>
        public Property(string name, object value, bool isSystemProperty = false)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (name.Length == 0)
            {
                throw new ArgumentException("Argument cannot be zero length", "name");
            }

            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            this.Name = name;
            this.Value = value.ToString();
            this.ValueString = value.ToString();
            this.ClrDataType = value.GetType().FullName;
            this.IsSystemProperty = isSystemProperty;
        }

        /// <summary>
        /// Gets or sets the unique identifier of this property.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the CLR data type of that this properties value is expected to represent.
        /// </summary>
        public string ClrDataType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this property has been deleted.
        /// </summary>
        public bool IsDeleted 
        {
            get
            {
                return this.isDeleted;
            }

            set
            {
                if (value != this.isDeleted)
                {
                    if (value && this.IsSystemProperty)
                    {
                        string message = string.Format(
                            "Cannot delete system property: {0}",
                            this.Name);
                        throw new Exception(message);
                    }

                    this.isDeleted = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("IsDeleted"));
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this property has changed.
        /// </summary>
        public bool IsDirty { get; private set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether this property is a system property.
        /// </summary>
        public bool IsSystemProperty { get; set; }

        /// <summary>
        /// Gets or sets the name of this property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the note that this property is attached to.
        /// </summary>
        public Note Note { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the note that this property belongs to.
        /// </summary>
        public long NoteId { get; set; }

        /// <summary>
        /// Gets or sets the value of this property.
        /// </summary>
        public string Value
        {
            get
            {
                return this.value;
            }

            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    this.IsDirty = true;
                    this.OnPropertyChanged(new PropertyChangedEventArgs(PropertyNameValue));
                }
            }
        }

        /// <summary>
        /// Gets or sets a string representation of this properties value.
        /// </summary>
        public string ValueString { get; set; }

        /// <summary>
        /// Clears the <see cref="IsDirty"/> flag.
        /// </summary>
        public void ClearIsDirty()
        {
            this.IsDirty = false;
        }

        /// <summary>
        /// Detaches this from its original storage so that it appears like a new note to be imported into another store.
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
            return string.Format("{0} = {1}", this.Name, this.Value);
        }

        /// <summary>
        /// Returns a clone of this property.
        /// </summary>
        /// <param name="deepCopy">True to perform a deep clone.</param>
        /// <returns>The clone.</returns>
        public Property Clone(bool deepCopy)
        {
            return new Property(this.Id, this.Name, this.Value, this.IsSystemProperty)
                {
                    ClrDataType = this.ClrDataType,
                    IsDeleted = this.IsDeleted
                };
        }

        /// <summary>
        /// Gets any script text that is contained in the value of this property.
        /// </summary>
        /// <returns>The script text.</returns>
        public string GetScript()
        {
            const string StartSentinel = "<#= ";
            const string EndSentinel = " #>";
            string script = string.Empty;

            int start = this.Value.IndexOf(StartSentinel, StringComparison.InvariantCulture);
            if (start >= 0)
            {
                start += StartSentinel.Length;
                int end = this.Value.IndexOf(EndSentinel, start, StringComparison.InvariantCulture);
                if (end >= 0)
                {
                    script = this.Value.Substring(start, end - start);
                }
            }

            return script;
        }
    }
}
