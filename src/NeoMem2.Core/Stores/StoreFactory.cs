// <copyright file="StoreFactory.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

namespace NeoMem2.Core.Stores
{
    public class StoreFactory
    {
        public static readonly StoreFactory Instance = new StoreFactory();

        private readonly Dictionary<string, INeoMemStoreFactory> m_StoreFactories = new Dictionary<string, INeoMemStoreFactory>();

        public IEnumerable<string> GetStoreTypes()
        {
            return m_StoreFactories.Keys;
        }

        public void Register(INeoMemStoreFactory storeFactory)
        {
            m_StoreFactories[storeFactory.GetType().FullName] = storeFactory;
        }

        public void Register(string name, INeoMemStoreFactory storeFactory)
        {
            m_StoreFactories[name] = storeFactory;
        }

        public string GetDefaultConnectionString(string name)
        {
            return GetStoreFactory(name).GetDefaultConnectionString();
        }

        public INeoMemStore GetStore(string name, string connectionString)
        {
            return GetStoreFactory(name).GetStore(connectionString);
        }

        private INeoMemStoreFactory GetStoreFactory(string name)
        {
            INeoMemStoreFactory factory;
            if (!m_StoreFactories.TryGetValue(name, out factory))
            {
                throw new NotSupportedException("Store not supported: " + name);
            }

            return factory;
        }
    }
}
