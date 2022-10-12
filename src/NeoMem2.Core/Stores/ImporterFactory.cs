// <copyright file="ImporterFactory.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

namespace NeoMem2.Core.Stores
{
    public class ImporterFactory
    {
        public static readonly ImporterFactory Instance = new ImporterFactory();

        private readonly Dictionary<string, IImporter> m_Importers = new Dictionary<string, IImporter>();

        public void Register(IImporter importer)
        {
            m_Importers[importer.GetType().FullName] = importer;
        }

        public void Register(string name, IImporter importer)
        {
            m_Importers[name] = importer;
        }

        public IEnumerable<string> GetImporterTypes()
        {
            return m_Importers.Keys;
        }

        public IImporter GetImporter(string name)
        {
            IImporter importer;
            if (!m_Importers.TryGetValue(name, out importer))
            {
                throw new NotSupportedException("Importer not supported: " + name);
            }

            return importer;
        }
    }
}
