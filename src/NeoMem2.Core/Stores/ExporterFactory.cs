// <copyright file="ExporterFactory.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Core.Stores
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A factory for providing <see cref="IExporter"/> implementations.
    /// </summary>
    public class ExporterFactory
    {
        /// <summary>
        /// A statically available factory instance.
        /// </summary>
        public static readonly ExporterFactory Instance = new ExporterFactory();

        /// <summary>
        /// The registered exporters.
        /// </summary>
        private readonly Dictionary<string, IExporter> exporters = new Dictionary<string, IExporter>();

        /// <summary>
        /// Registers an exporter.
        /// </summary>
        /// <param name="exporter">The exporter to register.</param>
        public void Register(IExporter exporter)
        {
            this.exporters[exporter.GetType().FullName] = exporter;
        }

        /// <summary>
        /// Registers an exporter.
        /// </summary>
        /// <param name="name">The custom name to register the exporter with.</param>
        /// <param name="exporter">The exporter to register.</param>
        public void Register(string name, IExporter exporter)
        {
            this.exporters[name] = exporter;
        }

        /// <summary>
        /// Gets the types of exporter registered.
        /// </summary>
        /// <returns>The names of the registered exporters.</returns>
        public IEnumerable<string> GetExporterTypes()
        {
            return this.exporters.Keys;
        }

        /// <summary>
        /// Gets an exporter by name.
        /// </summary>
        /// <param name="name">The name of the exporter to retrieve.</param>
        /// <returns>The requested exporter.</returns>
        /// <exception cref="NotSupportedException">Thrown if the requested exporter is not registered.</exception>
        public IExporter GetExporter(string name)
        {
            IExporter exporter;
            if (!this.exporters.TryGetValue(name, out exporter))
            {
                throw new NotSupportedException("Exporter not supported: " + name);
            }

            return exporter;
        }
    }
}
