// <copyright file="AdoContext.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data
{
    using NeoMem2.Utils;
    using System.Data;

    public class AdoContext : DisposableBase
    {
        public IDbConnection Connection { get; set; }

        public IDbTransaction Transaction { get; set; }

        public void Close(bool successful)
        {
            if (Transaction != null)
            {
                if (successful) Transaction.Commit();
                else Transaction.Rollback();
                Transaction = null;
            }

            if (Connection != null)
            {
                Connection.Close();
                Connection.Dispose();
                Connection = null;
            }
        }

        protected override void DisposeManagedResources()
        {
            Close(false);
            base.DisposeManagedResources();
        }
    }
}