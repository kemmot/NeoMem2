using NeoMem2.Utils;

namespace NeoMem2.Core.Stores
{
    using System;
    using System.ComponentModel;

    public abstract class ExporterBase : IExporter
    {
        public event ProgressChangedEventHandler ProgressChanged;

        /// <summary>
        /// Gets or sets the connection string to export to.
        /// </summary>
        public virtual string ConnectionString { get; set; }

        public abstract void Export(NeoMemFile file);

        protected void OnProgressChanged(int countComplete, int total, string status)
        {
            OnProgressChanged(new ProgressChangedEventArgs(Math2.CalculatePercentage(countComplete, total), status));
        }

        protected void OnProgressChanged(ProgressChangedEventArgs e)
        {
            ProgressChanged?.Invoke(this, e);
        }
    }
}
