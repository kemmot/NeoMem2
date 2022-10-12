// <copyright file="EnumWrapper.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Utils
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Provides a wrapper class for displaying descriptions for enumerations rather than
    /// just the enumeration string.
    /// </summary>
    /// <typeparam name="T">The type of enumeration to wrap.</typeparam>
    /// <remarks>
    /// If an enumeration value has the <see cref="DescriptionAttribute"/> applied to it
    /// then it's Description property will be used as the result of this classes
    /// ToString method.  If the attribute doesn't exist on the enumeration value then
    /// the usual enumeration values string will be used.
    /// </remarks>
    public class EnumWrapper<T> : IComparable<T>, IComparable<EnumWrapper<T>>, IEquatable<T>, IEquatable<EnumWrapper<T>>
        where T : struct, IComparable
    {
        /// <summary>
        /// The backing field for the <see cref="Description"/> property.
        /// </summary>
        private readonly string description;

        /// <summary>
        /// The backing field for the <see cref="EnumValue"/> property.
        /// </summary>
        private readonly T enumValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumWrapper{T}"/> class.
        /// </summary>
        /// <param name="enumValue">The enumeration value to wrap.</param>
        public EnumWrapper(T enumValue)
        {
            this.enumValue = enumValue;

            // calculate the description for the enum value
            object[] attributes = this.enumValue.GetType().GetField(this.enumValue.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            this.description = attributes.Length > 0
                ? ((DescriptionAttribute)attributes[0]).Description
                : enumValue.ToString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumWrapper{T}"/> class.
        /// </summary>
        /// <param name="enumValue">The enumeration value to wrap.</param>
        /// <param name="description">The description of the enumeration value.</param>
        public EnumWrapper(T enumValue, string description)
        {
            this.enumValue = enumValue;
            this.description = description;
        }

        /// <summary>
        /// Gets the description used for the specified enumeration value.
        /// </summary>
        /// <value>The description.</value>
        public string Description
        {
            get { return this.description; }
        }

        /// <summary>
        /// Gets the enumeration value being wrapped.
        /// </summary>
        /// <value>The enumeration value.</value>
        public T EnumValue
        {
            get { return this.enumValue; }
        }

        /// <summary>
        /// Compares this object to another.
        /// </summary>
        /// <param name="other">The object to compare to.</param>
        /// <returns>A value indicating the comparison result.</returns>
        public int CompareTo(T other)
        {
            return this.EnumValue.CompareTo(other);
        }

        /// <summary>
        /// Compares this <see cref="EnumWrapper{T}"/> to another.
        /// </summary>
        /// <param name="other">The object to compare to.</param>
        /// <returns>A value indicating the comparison result.</returns>
        public int CompareTo(EnumWrapper<T> other)
        {
            return this.EnumValue.CompareTo(other.EnumValue);
        }

        /// <summary>
        /// Checks if this object is equivalent to another.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>A value indicating the equivalency result.</returns>
        public override bool Equals(object obj)
        {
            if (obj is T)
            {
                return this.CompareTo((T)obj) == 0;
            }

            if (obj is EnumWrapper<T>)
            {
                return this.CompareTo((EnumWrapper<T>)obj) == 0;
            }

            return false;
        }

        /// <summary>
        /// Checks if the wrapped value <see cref="EnumValue"/> is equivalent to another.
        /// </summary>
        /// <param name="other">The object to compare to.</param>
        /// <returns>A value indicating the equivalency result.</returns>
        bool IEquatable<T>.Equals(T other)
        {
            return this.CompareTo(other) == 0;
        }

        /// <summary>
        /// Checks if this <see cref="EnumWrapper{T}"/> is equivalent to another.
        /// </summary>
        /// <param name="other">The object to compare to.</param>
        /// <returns>A value indicating the equivalency result.</returns>
        bool IEquatable<EnumWrapper<T>>.Equals(EnumWrapper<T> other)
        {
            return this.CompareTo(other) == 0;
        }

        /// <summary>
        /// Gets a hash code for the current object.
        /// </summary>
        /// <returns>The hash code value.</returns>
        public override int GetHashCode()
        {
            return this.enumValue.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override string ToString()
        {
            return this.description;
        }
    }
}
