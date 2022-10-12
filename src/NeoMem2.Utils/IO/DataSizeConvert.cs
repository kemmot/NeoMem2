// <copyright file="DataSizeConvert.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Utils.IO
{
    /// <summary>
    /// Converts values of data size from one unit to another.
    /// </summary>
    public static class DataSizeConvert
    {
        /// <summary>
        /// The number of bytes in a kilobyte.
        /// </summary>
        public const long BytesInKilobyte = 1024;

        /// <summary>
        /// The number of kilobytes in a megabyte.
        /// </summary>
        public const long KilobytesInMegabyte = 1024;

        /// <summary>
        /// The number of bytes in a megabyte.
        /// </summary>
        public const long BytesInMegabyte = BytesInKilobyte * KilobytesInMegabyte;

        /// <summary>
        /// The number of megabytes in a gigabyte.
        /// </summary>
        public const long MegabytesInGigabyte = 1024;

        /// <summary>
        /// The number of bytes in a gigabyte.
        /// </summary>
        public const long BytesInGigabyte = BytesInMegabyte * MegabytesInGigabyte;

        /// <summary>
        /// The number of gigabytes in a terabyte.
        /// </summary>
        public const long GigabytesInTerabyte = 1024;

        /// <summary>
        /// The number of bytes in a terabyte.
        /// </summary>
        public const long BytesInTerabyte = BytesInGigabyte * GigabytesInTerabyte;
        
        #region BytesFrom methods

        /// <summary>
        /// Converts a number of kilobytes into bytes.
        /// </summary>
        /// <param name="kilobytes">The number of kilobytes to convert.</param>
        /// <returns>The number of bytes.</returns>
        public static long BytesFromKilobytes(double kilobytes)
        {
            return (long)(kilobytes * BytesInKilobyte);
        }

        /// <summary>
        /// Converts a number of megabytes into bytes.
        /// </summary>
        /// <param name="megabytes">The number of megabytes to convert.</param>
        /// <returns>The number of bytes.</returns>
        public static long BytesFromMegabytes(double megabytes)
        {
            return (long)(megabytes * BytesInMegabyte);
        }

        /// <summary>
        /// Converts a number of gigabytes into bytes.
        /// </summary>
        /// <param name="gigabytes">The number of gigabytes to convert.</param>
        /// <returns>The number of bytes.</returns>
        public static long BytesFromGigabytes(double gigabytes)
        {
            return (long)(gigabytes * BytesInGigabyte);
        }

        /// <summary>
        /// Converts a number of terabytes into bytes.
        /// </summary>
        /// <param name="terabytes">The number of terabytes to convert.</param>
        /// <returns>The number of bytes.</returns>
        public static long BytesFromTerabytes(double terabytes)
        {
            return (long)(terabytes * BytesInTerabyte);
        }

        #endregion

        #region FromBytes methods

        /// <summary>
        /// Converts a number of bytes into kilobytes.
        /// </summary>
        /// <param name="bytes">The number of bytes to convert.</param>
        /// <returns>The result.</returns>
        public static double KilobytesFromBytes(long bytes)
        {
            return bytes / (double)BytesInKilobyte;
        }

        /// <summary>
        /// Converts a number of bytes into megabytes.
        /// </summary>
        /// <param name="bytes">The number of bytes to convert.</param>
        /// <returns>The result.</returns>
        public static double MegabytesFromBytes(long bytes)
        {
            return bytes / (double)BytesInMegabyte;
        }

        /// <summary>
        /// Converts a number of bytes into gigabytes.
        /// </summary>
        /// <param name="bytes">The number of bytes to convert.</param>
        /// <returns>The result.</returns>
        public static double GigabytesFromBytes(long bytes)
        {
            return bytes / (double)BytesInGigabyte;
        }

        /// <summary>
        /// Converts a number of bytes into terabytes.
        /// </summary>
        /// <param name="bytes">The number of bytes to convert.</param>
        /// <returns>The result.</returns>
        public static double TerabytesFromBytes(long bytes)
        {
            return bytes / (double)BytesInTerabyte;
        }

        #endregion
    }
}
