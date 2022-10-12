// <copyright file="INeoMemStoreFactory.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Stores
{
    public interface INeoMemStoreFactory
    {
        INeoMemStore GetStore(string connectionString);
        string GetDefaultConnectionString();
    }
}