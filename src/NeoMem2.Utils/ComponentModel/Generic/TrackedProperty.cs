// <copyright file="TrackedProperty.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Utils.ComponentModel.Generic
{
    using System;
    using System.ComponentModel;

    using NLog;

    /// <summary>
    /// Represents a generically typed property that can be change tracked.
    /// </summary>
    /// <typeparam name="T">The type of the property.</typeparam>
    public class TrackedProperty<T> : TrackedProperty
    {
        /// <summary>
        /// The maximum number of characters of the property value to log when it changes.
        /// </summary>
        private const int MaxCharactersToLog = 100;

        /// <summary>
        /// The backing field for the <see cref="Name"/> property.
        /// </summary>
        private readonly string name;

        /// <summary>
        /// The backing field for the <see cref="Value"/> property.
        /// </summary>
        private T value;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackedProperty{T}" /> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        public TrackedProperty(string name)
            : this(name, default(T))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackedProperty{T}" /> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="defaultValue">The starting value of the property.</param>
        public TrackedProperty(string name, T defaultValue)
        {
            this.name = name;
            this.value = defaultValue;
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// Gets or sets the value of the property.
        /// </summary>
        public T Value
        {
            get
            {
                return this.value;
            }

            set
            {
                if (this.value == null || !this.value.Equals(value))
                {
                    var oldValue = this.value;
                    this.value = value;

                    if (Logger.IsDebugEnabled)
                    {
                        int maxLength = Logger.IsEnabled(LogLevel.Trace) ? -1 : MaxCharactersToLog;
                        Logger.Debug(
                            "{0} property changed from '{1}' to '{2}'",
                            this.name,
                            TruncateObjectForLogging(oldValue, maxLength),
                            TruncateObjectForLogging(this.value, maxLength));
                    }

                    this.OnPropertyChanged(new PropertyChangedEventArgs(this.Name));
                }
            }
        }

        /// <summary>
        /// An operator for implicitly casting an object of type <see cref="TrackedProperty{T}"/> into a value of type T.
        /// </summary>
        /// <param name="value">The value to cast.</param>
        /// <returns>The result of the cast.</returns>
        public static implicit operator T(TrackedProperty<T> value)
        {
            return value.Value;
        }

        /// <summary>
        /// Ensures that the property value is no longer than the specified length for logging purposes.
        /// </summary>
        /// <param name="value">The value to truncate.</param>
        /// <param name="maxLength">The maximum length to truncate at.</param>
        /// <returns>The value no longer than the specified length.</returns>
        private static string TruncateObjectForLogging(object value, int maxLength)
        {
            string text;
            if (value == null)
            {
                text = string.Empty;
            }
            else
            {
                text = value.ToString();
                int newLineIndex = text.IndexOf(Environment.NewLine);

                if (newLineIndex >= 0)
                {
                    text = text.Substring(0, newLineIndex);
                }

                if (maxLength >= 0 && text.Length > maxLength)
                {
                    text = text.Substring(0, maxLength);
                }
            }

            return text;
        }
    }
}
