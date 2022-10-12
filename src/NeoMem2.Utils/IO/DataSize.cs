// <copyright file="DataSize.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Utils.IO
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a data size that can be compared with other data sizes regardless of units.
    /// </summary>
    public class DataSize : IFormattable, IComparable<DataSize>
    {
        /// <summary>
        /// The default format to use.
        /// </summary>
        private const string DefaultOutputFormat = DataSizeFormat.BestShort;
        
        /// <summary>
        /// Backing field for the <see cref="Bytes"/> property.
        /// </summary>
        private readonly long bytes;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DataSize" /> class.
        /// </summary>
        public DataSize()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSize" /> class.
        /// </summary>
        /// <param name="dataSize">The DataSize instance to initialize this object from.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if a required argument has a null reference.
        /// </exception>
        public DataSize(DataSize dataSize)
        {
            if (dataSize == null)
            {
                throw new ArgumentNullException("dataSize");
            }

            this.bytes = dataSize.Bytes;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSize" /> class.
        /// </summary>
        /// <param name="bytes">The number of bytes to initialize this object with.</param>
        public DataSize(long bytes)
        {
            this.bytes = bytes;
        }
        
        /// <summary>
        /// Gets the number of bytes that this object represents.
        /// </summary>
        public long Bytes
        {
            get { return this.bytes; }
        }
        
        /// <summary>
        /// Supports implicit casting of <see cref="DataSize"/> instances to
        /// <see cref="long"/> values.
        /// </summary>
        /// <param name="value">The value to be cast.</param>
        /// <returns>The cast value.</returns>
        /// <remarks>
        /// The number of bytes is used as the conversion unit.
        /// </remarks>
        public static implicit operator long(DataSize value)
        {
            return value == null ? 0 : value.Bytes;
        }

        /// <summary>
        /// Supports implicit casting of <see cref="long"/> instances to
        /// <see cref="DataSize"/> values.
        /// </summary>
        /// <param name="value">The value to be cast.</param>
        /// <returns>The cast value.</returns>
        /// <remarks>
        /// The number of bytes is used as the conversion unit.
        /// </remarks>
        public static implicit operator DataSize(long value)
        {
            return new DataSize(value);
        }

        /// <summary>
        /// Supports the less than operator.
        /// </summary>
        /// <param name="dataSize1">The first <see cref="DataSize"/> value to compare.</param>
        /// <param name="dataSize2">The second <see cref="DataSize"/> value to compare.</param>
        /// <returns>True if <paramref name="dataSize1"/> was less than <paramref name="dataSize2"/>.</returns>
        public static bool operator <(DataSize dataSize1, DataSize dataSize2)
        {
            return Compare(dataSize1, dataSize2) < 0;
        }

        /// <summary>
        /// Supports the greater than operator.
        /// </summary>
        /// <param name="dataSize1">The first <see cref="DataSize"/> value to compare.</param>
        /// <param name="dataSize2">The second <see cref="DataSize"/> value to compare.</param>
        /// <returns>True if <paramref name="dataSize1"/> was greater than <paramref name="dataSize2"/>.</returns>
        public static bool operator >(DataSize dataSize1, DataSize dataSize2)
        {
            return Compare(dataSize1, dataSize2) > 0;
        }

        /// <summary>
        /// Supports the equal to operator.
        /// </summary>
        /// <param name="dataSize1">The first <see cref="DataSize"/> value to compare.</param>
        /// <param name="dataSize2">The second <see cref="DataSize"/> value to compare.</param>
        /// <returns>True if <paramref name="dataSize1"/> was equal to <paramref name="dataSize2"/>.</returns>
        public static bool operator ==(DataSize dataSize1, DataSize dataSize2)
        {
            return Compare(dataSize1, dataSize2) == 0;
        }

        /// <summary>
        /// Supports the not equal to operator.
        /// </summary>
        /// <param name="dataSize1">The first <see cref="DataSize"/> value to compare.</param>
        /// <param name="dataSize2">The second <see cref="DataSize"/> value to compare.</param>
        /// <returns>True if <paramref name="dataSize1"/> was not equal to <paramref name="dataSize2"/>.</returns>
        public static bool operator !=(DataSize dataSize1, DataSize dataSize2)
        {
            return Compare(dataSize1, dataSize2) != 0;
        }

        /// <summary>
        /// Supports the less than or equal to operator.
        /// </summary>
        /// <param name="dataSize1">The first <see cref="DataSize"/> value to compare.</param>
        /// <param name="dataSize2">The second <see cref="DataSize"/> value to compare.</param>
        /// <returns>True if <paramref name="dataSize1"/> was less than or equal to <paramref name="dataSize2"/>.</returns>
        public static bool operator <=(DataSize dataSize1, DataSize dataSize2)
        {
            return Compare(dataSize1, dataSize2) <= 0;
        }

        /// <summary>
        /// Supports the greater than or equal to operator.
        /// </summary>
        /// <param name="dataSize1">The first <see cref="DataSize"/> value to compare.</param>
        /// <param name="dataSize2">The second <see cref="DataSize"/> value to compare.</param>
        /// <returns>True if <paramref name="dataSize1"/> was greater than or equal to <paramref name="dataSize2"/>.</returns>
        public static bool operator >=(DataSize dataSize1, DataSize dataSize2)
        {
            return Compare(dataSize1, dataSize2) >= 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSize" /> class from a specific data size unit.
        /// </summary>
        /// <param name="value">The data size value in the units specified.</param>
        /// <param name="unit">The units of the value.</param>
        /// <returns>The initialized DataSize object.</returns>
        public static DataSize FromUnit(double value, DataSizeUnit unit)
        {
            DataSize dataSize;
            switch (unit)
            {
                case DataSizeUnit.Bytes:
                    dataSize = FromBytes((long)value);
                    break;
                case DataSizeUnit.Kilobytes:
                    dataSize = FromKilobytes(value);
                    break;
                case DataSizeUnit.Megabytes:
                    dataSize = FromMegabytes(value);
                    break;
                case DataSizeUnit.Gigabytes:
                    dataSize = FromGigabytes(value);
                    break;
                case DataSizeUnit.Terabytes:
                    dataSize = FromTerabytes(value);
                    break;
                default:
                    string errorMessage = string.Format(
                        CultureInfo.CurrentCulture,
                        "Unsupported data size unit: {0}",
                        unit);
                    throw new NotSupportedException(errorMessage);
            }

            return dataSize;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSize" /> class from a number of bytes.
        /// </summary>
        /// <param name="bytes">The number of bytes.</param>
        /// <returns>The initialized DataSize object.</returns>
        public static DataSize FromBytes(long bytes)
        {
            return new DataSize(bytes);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSize" /> class from a number of kilobytes.
        /// </summary>
        /// <param name="kilobytes">The number of kilobytes.</param>
        /// <returns>The initialized DataSize object.</returns>
        public static DataSize FromKilobytes(double kilobytes)
        {
            return new DataSize(DataSizeConvert.BytesFromKilobytes(kilobytes));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSize" /> class from a number of megabytes.
        /// </summary>
        /// <param name="megabytes">The number of megabytes.</param>
        /// <returns>The initialized DataSize object.</returns>
        public static DataSize FromMegabytes(double megabytes)
        {
            return new DataSize(DataSizeConvert.BytesFromMegabytes(megabytes));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSize" /> class from a number of gigabytes.
        /// </summary>
        /// <param name="gigabytes">The number of gigabytes.</param>
        /// <returns>The initialized DataSize object.</returns>
        public static DataSize FromGigabytes(double gigabytes)
        {
            return new DataSize(DataSizeConvert.BytesFromGigabytes(gigabytes));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSize" /> class from a number of terabytes.
        /// </summary>
        /// <param name="terabytes">The number of terabytes.</param>
        /// <returns>The initialized DataSize object.</returns>
        public static DataSize FromTerabytes(double terabytes)
        {
            return new DataSize(DataSizeConvert.BytesFromTerabytes(terabytes));
        }

        /// <summary>
        /// Compares two <see cref="DataSize"/> objects.
        /// </summary>
        /// <param name="left">The left object to compare.</param>
        /// <param name="right">The right object to compare.</param>
        /// <returns>
        /// Less than zero if <paramref name="left"/> was less than <paramref name="right"/>.
        /// Zero if <paramref name="left"/> was equal to <paramref name="right"/>.
        /// Greater than zero if <paramref name="left"/> was greater than <paramref name="right"/>.
        /// </returns>
        public static int Compare(DataSize left, DataSize right)
        {
            int result;
            if (ReferenceEquals(left, null))
            {
                result = ReferenceEquals(right, null) ? 0 : 1;
            }
            else
            {
                if (ReferenceEquals(right, null))
                {
                    result = -1;
                }
                else
                {
                    result = left.Bytes.CompareTo(right.Bytes);
                }
            }

            return result;
        }

        /// <summary>
        /// Compares this object to another.
        /// </summary>
        /// <param name="other">The object to compare this against.</param>
        /// <returns>A value indicating the comparison result.</returns>
        public int CompareTo(DataSize other)
        {
            return Compare(this, other);
        }

        /// <summary>
        /// Checks if this object is equal to another.
        /// </summary>
        /// <param name="obj">The first <see cref="DataSize"/> value to compare.</param>
        /// <returns>True if <paramref name="obj"/> was equal to this object.</returns>
        /// <exception cref="InvalidCastException">Thrown if <paramref name="obj"/> could not be cast to <see cref="DataSize"/>.</exception>
        public override bool Equals(object obj)
        {
            return this == (DataSize)obj;
        }

        /// <summary>
        /// Gets a hash code for this object.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return this.Bytes.GetHashCode();
        }

        /// <summary>
        /// Converts this DataSize into the best units to represent the value.
        /// </summary>
        /// <param name="value">The value result.</param>
        /// <param name="unit">The units that the value is expressed in.</param>
        public void ToBest(out double value, out DataSizeUnit unit)
        {
            unit = this.GetBestUnit();
            value = this.ToUnit(unit);
        }

        /// <summary>
        /// Converts this DataSize into a specific unit.
        /// </summary>
        /// <param name="unit">The unit to express the DataSize in.</param>
        /// <returns>The converted value.</returns>
        public double ToUnit(DataSizeUnit unit)
        {
            double value;
            switch (unit)
            {
                case DataSizeUnit.Bytes:
                    value = this.Bytes;
                    break;
                case DataSizeUnit.Kilobytes:
                    value = this.ToKilobytes();
                    break;
                case DataSizeUnit.Megabytes:
                    value = this.ToMegabytes();
                    break;
                case DataSizeUnit.Gigabytes:
                    value = this.ToGigabytes();
                    break;
                case DataSizeUnit.Terabytes:
                    value = this.ToTerabytes();
                    break;
                default:
                    string errorMessage = string.Format(
                        CultureInfo.CurrentCulture,
                        "Unsupported data size unit: {0}",
                        unit);
                    throw new NotSupportedException(errorMessage);
            }

            return value;
        }

        /// <summary>
        /// Converts this DataSize into the <see cref="DataSizeUnit.Kilobytes"/> unit.
        /// </summary>
        /// <returns>The converted value.</returns>
        public double ToKilobytes()
        {
            return DataSizeConvert.KilobytesFromBytes(this.Bytes);
        }

        /// <summary>
        /// Converts this DataSize into the <see cref="DataSizeUnit.Megabytes"/> unit.
        /// </summary>
        /// <returns>The converted value.</returns>
        public double ToMegabytes()
        {
            return DataSizeConvert.MegabytesFromBytes(this.Bytes);
        }

        /// <summary>
        /// Converts this DataSize into the <see cref="DataSizeUnit.Gigabytes"/> unit.
        /// </summary>
        /// <returns>The converted value.</returns>
        public double ToGigabytes()
        {
            return DataSizeConvert.GigabytesFromBytes(this.Bytes);
        }

        /// <summary>
        /// Converts this DataSize into the <see cref="DataSizeUnit.Terabytes"/> unit.
        /// </summary>
        /// <returns>The converted value.</returns>
        public double ToTerabytes()
        {
            return DataSizeConvert.TerabytesFromBytes(this.Bytes);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return this.ToString(DefaultOutputFormat, null);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <param name="format">The format string to use.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <returns>A string that represents the current object.</returns>
        /// <exception cref="FormatException">Thrown if <paramref name="format"/> is not in the expected format.</exception>
        /// <remarks>
        /// The following format types can be used:
        /// <ul>
        /// <li>B, Bytes     : displays the value as a number of bytes.</li>
        /// <li>KB, Kilobytes: displays the value as a number of kilobytes.</li>
        /// <li>MB, Megabytes: displays the value as a number of megabytes.</li>
        /// <li>GB, Gigabytes: displays the value as a number of gigabytes.</li>
        /// <li>TB, Terabytes: displays the value as a number of terabytes.</li>
        /// <li>Best         : displays the value using the best units.</li>
        /// </ul>
        /// </remarks>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            bool shortForm;
            DataSizeUnit unit;
            this.ParseFormat(format, out unit, out shortForm);

            double value = this.ToUnit(unit);
            string unitDescription = GetUnitDescription(unit, shortForm);
            return string.Format(CultureInfo.CurrentCulture, "{0:0.0} {1}", value, unitDescription);
        }

        /// <summary>
        /// Gets a string description of the unit.
        /// </summary>
        /// <param name="unit">The unit to get a description for.</param>
        /// <param name="shortForm">Whether to use the short form or not.</param>
        /// <returns>The description of the unit.</returns>
        private static string GetUnitDescription(DataSizeUnit unit, bool shortForm)
        {
            string description;
            switch (unit)
            {
                case DataSizeUnit.Bytes:
                    description = shortForm ? "B" : "Bytes";
                    break;
                case DataSizeUnit.Kilobytes:
                    description = shortForm ? "KB" : "Kilobytes";
                    break;
                case DataSizeUnit.Megabytes:
                    description = shortForm ? "MB" : "Megabytes";
                    break;
                case DataSizeUnit.Gigabytes:
                    description = shortForm ? "GB" : "Gigabytes";
                    break;
                case DataSizeUnit.Terabytes:
                    description = shortForm ? "TB" : "Terabytes";
                    break;
                default:
                    string errorMessage = string.Format(
                        CultureInfo.CurrentCulture,
                        "The unit is not supported: {0}",
                        unit);
                    throw new NotSupportedException(errorMessage);
            }

            return description;
        }

        /// <summary>
        /// Parses the format string into the requested unit and form.
        /// </summary>
        /// <param name="format">The format to parse.</param>
        /// <param name="unit">The parsed unit.</param>
        /// <param name="shortForm">The parsed form.</param>
        /// <exception cref="FormatException">Thrown if <paramref name="format"/> is not in the expected format.</exception>
        private void ParseFormat(string format, out DataSizeUnit unit, out bool shortForm)
        {
            string upperFormat = string.IsNullOrEmpty(format) ? DefaultOutputFormat : format.ToUpper(CultureInfo.InvariantCulture);
            switch (upperFormat)
            {
                case DataSizeFormat.BytesShort:
                    shortForm = true;
                    unit = DataSizeUnit.Bytes;
                    break;
                case DataSizeFormat.BytesLong:
                    shortForm = false;
                    unit = DataSizeUnit.Bytes;
                    break;
                case DataSizeFormat.KilobytesShort:
                    shortForm = true;
                    unit = DataSizeUnit.Kilobytes;
                    break;
                case DataSizeFormat.KilobytesLong:
                    shortForm = false;
                    unit = DataSizeUnit.Kilobytes;
                    break;
                case DataSizeFormat.MegabytesShort:
                    shortForm = true;
                    unit = DataSizeUnit.Megabytes;
                    break;
                case DataSizeFormat.MegabytesLong:
                    shortForm = false;
                    unit = DataSizeUnit.Megabytes;
                    break;
                case DataSizeFormat.GigabytesShort:
                    shortForm = true;
                    unit = DataSizeUnit.Gigabytes;
                    break;
                case DataSizeFormat.GigabytesLong:
                    shortForm = false;
                    unit = DataSizeUnit.Gigabytes;
                    break;
                case DataSizeFormat.TerabytesShort:
                    shortForm = true;
                    unit = DataSizeUnit.Terabytes;
                    break;
                case DataSizeFormat.TerabytesLong:
                    shortForm = false;
                    unit = DataSizeUnit.Terabytes;
                    break;
                case DataSizeFormat.BestShort:
                case DataSizeFormat.BestShort2:
                    shortForm = true;
                    unit = this.GetBestUnit();
                    break;
                case DataSizeFormat.BestLong:
                    shortForm = false;
                    unit = this.GetBestUnit();
                    break;
                default:
                    string errorMessage = string.Format(
                        CultureInfo.CurrentCulture,
                        "The format is not supported: {0}",
                        format);
                    throw new FormatException(errorMessage);
            }
        }

        /// <summary>
        /// Gets the best unit for this data size.
        /// </summary>
        /// <returns>The best unit.</returns>
        private DataSizeUnit GetBestUnit()
        {
            DataSizeUnit unit;
            if (this.Bytes > DataSizeConvert.BytesInTerabyte)
            {
                unit = DataSizeUnit.Terabytes;
            }
            else if (this.Bytes > DataSizeConvert.BytesInGigabyte)
            {
                unit = DataSizeUnit.Gigabytes;
            }
            else if (this.Bytes > DataSizeConvert.BytesInMegabyte)
            {
                unit = DataSizeUnit.Megabytes;
            }
            else if (this.Bytes > DataSizeConvert.BytesInKilobyte)
            {
                unit = DataSizeUnit.Kilobytes;
            }
            else
            {
                unit = DataSizeUnit.Bytes;
            }

            return unit;
        }
    }
}
