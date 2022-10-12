// <copyright file="ItemCompleteEventArgs.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Utils
{
    using System;

    /// <summary>
    /// An event arguments class regarding completion of a task related to an item.
    /// </summary>
    /// <typeparam name="T">The type of item that the arguments relate to.</typeparam>
    public class ItemCompleteEventArgs<T> : ItemEventArgs<T>
    {
        /// <summary>
        /// Backing field for the <see cref="Exception"/> property.
        /// </summary>
        private readonly Exception exception;

        /// <summary>
        /// Backing field for the <see cref="Success"/> property.
        /// </summary>
        private readonly bool success;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemCompleteEventArgs{T}" /> class.
        /// </summary>
        /// <param name="item">The item that this completion event is regarding.</param>
        public ItemCompleteEventArgs(T item)
            : this(item, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemCompleteEventArgs{T}" /> class.
        /// </summary>
        /// <param name="item">The item that this completion event is regarding.</param>
        /// <param name="exception">The exception that caused the unsuccessful completion.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if a required argument has a null reference.
        /// </exception>
        public ItemCompleteEventArgs(T item, Exception exception)
            : this(item, false)
        {
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }

            this.exception = exception;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemCompleteEventArgs{T}" /> class.
        /// </summary>
        /// <param name="item">The item that this completion event is regarding.</param>
        /// <param name="success">Whether the completion was successful.</param>
        public ItemCompleteEventArgs(T item, bool success)
            : base(item)
        {
            this.success = success;
        }

        /// <summary>
        /// Gets the exception that caused the unsuccessful completion.
        /// </summary>
        public Exception Exception
        {
            get { return this.exception; }
        }

        /// <summary>
        /// Gets a value indicating whether the completion was successful.
        /// </summary>
        public bool Success 
        {
            get
            {
                return this.success;
            } 
        }
    }
}
