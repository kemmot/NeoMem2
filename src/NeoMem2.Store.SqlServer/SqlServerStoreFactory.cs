// <copyright file="SqlServerStoreFactory.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer
{
    using NeoMem2.Core.Stores;

    /// <summary>
    /// An implementation of <see cref="INeoMemStoreFactory"/> that uses <see cref="SqlServerStore"/>.
    /// </summary>
    public class SqlServerStoreFactory : INeoMemStoreFactory
    {
        /// <summary>
        /// Gets a default connection string for the store.
        /// </summary>
        /// <returns>The connection string.</returns>
        public string GetDefaultConnectionString()
        {
            return SqlServerStore.GetDefaultConnectionString();
        }

        /// <summary>
        /// Gets an initialized store.
        /// </summary>
        /// <param name="connectionString">The connection string to use to initialize the store.</param>
        /// <returns>The store.</returns>
        public INeoMemStore GetStore(string connectionString)
        {
            return new SqlServerStore(connectionString);
        }
    }
}
