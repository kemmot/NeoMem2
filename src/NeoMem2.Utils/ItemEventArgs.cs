// <copyright file="ItemEventArgs.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Utils
{
    using System;

    /// <summary>
    /// An event arguments class relating to a single object.
    /// </summary>
    /// <typeparam name="T">The type of item that the arguments relate to.</typeparam>
    public class ItemEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Backing field for the <see cref="Item"/> property.
        /// </summary>
        private readonly T item;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemEventArgs{T}" /> class.
        /// </summary>
        /// <param name="item">The item that the event relates to.</param>
        public ItemEventArgs(T item)
        {
            this.item = item;
        }

        /// <summary>
        /// Gets the item that the event relates to.
        /// </summary>
        public T Item
        {
            get { return this.item; }
        }
    }
}
