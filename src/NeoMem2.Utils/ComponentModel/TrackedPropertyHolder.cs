// <copyright file="TrackedPropertyHolder.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Utils.ComponentModel
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Handles multiple <see cref="INotifyPropertyChanged"/> objects.
    /// </summary>
    /// <remarks>
    /// Holds references to multiple objects that support change notification and
    /// exposes all of their change events through a single event on this object.
    /// </remarks>
    public class TrackedPropertyHolder : NotifyPropertyChangedBase
    {
        /// <summary>
        /// Registers an object to be subscribed to for change notification.
        /// </summary>
        /// <param name="publisher">The object to subscribe to.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if a required argument has a null reference.
        /// </exception>
        public void Register(INotifyPropertyChanged publisher)
        {
            if (publisher == null)
            {
                throw new ArgumentNullException("publisher");
            }

            publisher.PropertyChanged += this.PropertyPropertyChanged;
        }

        /// <summary>
        /// Handles the <see cref="INotifyPropertyChanged.PropertyChanged"/> events of registered publishers.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void PropertyPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.OnPropertyChanged(e);
        }
    }
}
