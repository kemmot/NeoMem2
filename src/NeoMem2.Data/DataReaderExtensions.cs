// <copyright file="DataReaderExtensions.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data
{
    using System;
    using System.Data;

    /// <summary>
    /// Provides extension methods for the <see cref="SqlDataReader"/> class.
    /// </summary>
    public static class DataReaderExtensions
    {
        /// <summary>
        /// Attempts to cast a reference value from a reader value.
        /// </summary>
        /// <typeparam name="T">The type to cast the value to.</typeparam>
        /// <param name="reader">The reader to acquire the value from.</param>
        /// <param name="columnName">The name of the column to acquire the value from.</param>
        /// <returns>The cast value.</returns>
        /// <exception cref="Exception">Thrown if it fails to acquire or cast the value.</exception>
        public static T GetReferenceValue<T>(this IDataReader reader, string columnName)
        {
            T value;
            try
            {
                int ordinal = GetOrdinal(reader, columnName);

                object valueObject;
                try
                {
                    valueObject = reader.GetValue(ordinal);
                }
                catch (Exception ex)
                {
                    string message = string.Format("Failed to get value for column ordinal {0}", ordinal);
                    throw new Exception(message, ex);
                }

                try
                {
                    if (valueObject == DBNull.Value)
                    {
                        value = default(T);
                    }
                    else
                    {
                        value = (T)valueObject;
                    }
                }
                catch (Exception ex)
                {
                    string message = string.Format(
                        "Failed to cast ordinal {0} to {1}: '{2}'",
                        ordinal,
                        typeof(T).Name,
                        valueObject == null ? "[NULL]" : valueObject.ToString());
                    throw new Exception(message, ex);
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("Failed to get value for column '{0}'", columnName);
                throw new Exception(message, ex);
            }

            return value;
        }

        /// <summary>
        /// Gets the ordinal value of a column by name from a reader result.
        /// </summary>
        /// <param name="reader">The reader to query for the ordinal.</param>
        /// <param name="columnName">The name of the column to query for.</param>
        /// <returns>The ordinal value of the column.</returns>
        /// <exception cref="Exception">Thrown if the ordinal could not be found.</exception>
        /// <remarks>Provides additional exception information when it fails.</remarks>
        private static int GetOrdinal(IDataReader reader, string columnName)
        {
            int ordinal;
            try
            {
                ordinal = reader.GetOrdinal(columnName);
            }
            catch (Exception ex)
            {
                string message = string.Format("Failed to get ordinal for column: '{0}'", columnName);
                throw new Exception(message, ex);
            }

            return ordinal;
        }
    }
}
