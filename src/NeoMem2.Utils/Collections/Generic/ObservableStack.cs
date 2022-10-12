// <copyright file="ObservableStack.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Utils.Collections.Generic
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A stack implementation that publishes change events.
    /// </summary>
    /// <typeparam name="T">The type of object stored in the stack.</typeparam>
    public class ObservableStack<T>
    {
        /// <summary>
        /// The collection used for internal storage.
        /// </summary>
        private readonly Stack<T> internalStack = new Stack<T>();

        /// <summary>
        /// Raised when the stack changes.
        /// </summary>
        public event EventHandler StateChanged;

        /// <summary>
        /// Gets the count of items in the stack.
        /// </summary>
        public int Count
        {
            get { return this.internalStack.Count; }
        }

        /// <summary>
        /// Pops an item from the head of the stack.
        /// </summary>
        /// <returns>The popped item.</returns>
        public T Pop()
        {
            T item = this.internalStack.Pop();
            this.OnStateChanged(new EventArgs());
            return item;
        }

        /// <summary>
        /// Pops all items from the head of the stack.
        /// </summary>
        /// <returns>All popped items.</returns>
        public IEnumerable<T> PopAll()
        {
            while (this.internalStack.Count > 0)
            {
                yield return this.internalStack.Pop();
            }
        }

        /// <summary>
        /// Pushes multiple items onto the head of the stack.
        /// </summary>
        /// <param name="items">The items to push onto the stack.</param>
        public void Push(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                this.internalStack.Push(item);
            }

            this.OnStateChanged(new EventArgs());
        }

        /// <summary>
        /// Pushes an item onto the head of the stack.
        /// </summary>
        /// <param name="item">The item to push onto the stack.</param>
        public void Push(T item)
        {
            this.internalStack.Push(item);
            this.OnStateChanged(new EventArgs());
        }

        /// <summary>
        /// Raises the <see cref="StateChanged"/> event.
        /// </summary>
        /// <param name="e">The event arguments to raise the event with.</param>
        protected virtual void OnStateChanged(EventArgs e)
        {
            if (this.StateChanged != null)
            {
                this.StateChanged(this, e);
            }
        }
    }
}
