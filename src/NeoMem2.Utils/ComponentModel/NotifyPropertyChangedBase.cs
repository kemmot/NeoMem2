// <copyright file="NotifyPropertyChangedBase.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Utils.ComponentModel
{
    using System.ComponentModel;

    /// <summary>
    /// A base class providing a common implementation of the <see cref="INotifyPropertyChanged"/> interface.
    /// </summary>
    public class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, e);
            }
        }
    }
}
