// <copyright file="ChangeTrackedBase.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Utils.ComponentModel
{
    using System.ComponentModel;

    /// <summary>
    /// A base class providing common functionality for tracking changes in an object.
    /// </summary>
    public class ChangeTrackedBase : NotifyPropertyChangedBase
    {
        /// <summary>
        /// The backing field for the <see cref="PropertyHolder"/> property.
        /// </summary>
        private readonly TrackedPropertyHolder propertyHolder = new TrackedPropertyHolder();

        /// <summary>
        /// The property name to report in change events.
        /// </summary>
        private readonly string propertyNameToReport;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeTrackedBase" /> class.
        /// </summary>
        /// <param name="propertyNameToReport">The property name to report in change events.</param>
        protected ChangeTrackedBase(string propertyNameToReport = "")
        {
            this.propertyNameToReport = propertyNameToReport;
            this.PropertyHolder.PropertyChanged += this.PropertyHolderPropertyChanged;
            this.IsDirty = true;
        }

        /// <summary>
        /// Gets a value indicating whether this object has changes.
        /// </summary>
        public bool IsDirty { get; private set; }

        /// <summary>
        /// Gets the object that tracks the properties of this object.
        /// </summary>
        protected TrackedPropertyHolder PropertyHolder
        {
            get { return this.propertyHolder; }
        }

        /// <summary>
        /// Sets this object as not having any changes.
        /// </summary>
        public void ClearIsDirty()
        {
            this.IsDirty = false;
        }

        /// <summary>
        /// Handles the <see cref="TrackedPropertyHolder.PropertyChanged"/> event of the <see cref="PropertyHolder"/> property.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void PropertyHolderPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.IsDirty = true;

            string propertyName = string.IsNullOrEmpty(this.propertyNameToReport) ? e.PropertyName : this.propertyNameToReport;
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}
