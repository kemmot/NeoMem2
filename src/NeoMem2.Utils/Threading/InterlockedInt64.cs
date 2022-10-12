// <copyright file="InterlockedInt64.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Utils.Threading
{
    using System;
    using System.Threading;

    /// <summary>
    /// Provides an <see cref="Int64"/> value that is operated on using
    /// <see cref="Interlocked"/> methods.
    /// </summary>
    public class InterlockedInt64
    {
        /// <summary>
        /// The backing field for the <see cref="Value"/> property.
        /// </summary>
        private long value;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterlockedInt64" /> class.
        /// </summary>
        public InterlockedInt64()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterlockedInt64" /> class.
        /// </summary>
        /// <param name="value">The value to initialize this class to.</param>
        public InterlockedInt64(long value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the value of the <see cref="long"/> using 
        /// <see cref="Interlocked"/> calls.
        /// </summary>
        public long Value
        {
            get { return Interlocked.Read(ref this.value); }
            set { Interlocked.Exchange(ref this.value, value); }
        }

        /// <summary>
        /// Exchanges the value if it was greater than a specified value.
        /// </summary>
        /// <param name="value">The value to test and exchange.</param>
        /// <param name="newValue">The new value to set.</param>
        /// <param name="comparand">The value to compare against.</param>
        /// <returns>The original value before it was exchanged.</returns>
        public static long ExchangeIfGreaterThan(ref long value, long newValue, long comparand)
        {
            long originalValue;
            ExchangeIfGreaterThan(ref value, newValue, comparand, out originalValue);
            return originalValue;
        }

        /// <summary>
        /// Exchanges the value if it was greater than a specified value.
        /// </summary>
        /// <param name="value">The value to test and exchange.</param>
        /// <param name="newValue">The new value to set.</param>
        /// <param name="comparand">The value to compare against.</param>
        /// <param name="originalValue">The original value before it was exchanged.</param>
        /// <returns>True if the value was exchanged; false otherwise.</returns>
        public static bool ExchangeIfGreaterThan(ref long value, long newValue, long comparand, out long originalValue)
        {
            bool exchanged;
            bool greaterThan;
            do
            {
                long localValueLong = Interlocked.Read(ref value);

                greaterThan = localValueLong > comparand;
                if (greaterThan)
                {
                    exchanged = Interlocked.CompareExchange(ref value, newValue, localValueLong) == localValueLong;
                }
                else
                {
                    exchanged = false;
                }

                originalValue = localValueLong;
            }
            while (greaterThan && !exchanged);

            return exchanged;
        }
        
        /// <summary>
        /// Exchanges the value if it was greater than the existing value.
        /// </summary>
        /// <param name="value">The value to test and exchange.</param>
        /// <param name="newValue">The new value to set.</param>
        /// <param name="originalValue">The original value before it was exchanged.</param>
        /// <returns>True if the value was exchanged; false otherwise.</returns>
        public static bool ExchangeIfGreaterThanValue(ref long value, long newValue, out long originalValue)
        {
            bool exchanged;
            bool greaterThan;
            do
            {
                long localValueLong = Interlocked.Read(ref value);

                greaterThan = newValue > localValueLong;
                if (greaterThan)
                {
                    exchanged = Interlocked.CompareExchange(ref value, newValue, localValueLong) == localValueLong;
                }
                else
                {
                    exchanged = false;
                }

                originalValue = localValueLong;
            }
            while (greaterThan && !exchanged);

            return exchanged;
        }

        /// <summary>
        /// Exchanges the value if it was less than a specified value.
        /// </summary>
        /// <param name="value">The value to test and exchange.</param>
        /// <param name="newValue">The new value to set.</param>
        /// <param name="comparand">The value to compare against.</param>
        /// <returns>The original value before it was exchanged.</returns>
        public static long ExchangeIfLessThan(ref long value, long newValue, long comparand)
        {
            long originalValue;
            ExchangeIfLessThan(ref value, newValue, comparand, out originalValue);
            return originalValue;
        }

        /// <summary>
        /// Exchanges the value if it was less than a specified value.
        /// </summary>
        /// <param name="value">The value to test and exchange.</param>
        /// <param name="newValue">The new value to set.</param>
        /// <param name="comparand">The value to compare against.</param>
        /// <param name="originalValue">The original value before it was exchanged.</param>
        /// <returns>True if the value was exchanged; false otherwise.</returns>
        public static bool ExchangeIfLessThan(ref long value, long newValue, long comparand, out long originalValue)
        {
            bool exchanged;
            bool lessThan;
            do
            {
                long localValueLong = Interlocked.Read(ref value);

                lessThan = localValueLong < comparand;
                if (lessThan)
                {
                    exchanged = Interlocked.CompareExchange(ref value, newValue, localValueLong) == localValueLong;
                }
                else
                {
                    exchanged = false;
                }

                originalValue = localValueLong;
            }
            while (lessThan && !exchanged);

            return exchanged;
        }

        /// <summary>
        /// Exchanges the value if it was less than the existing value.
        /// </summary>
        /// <param name="value">The value to test and exchange.</param>
        /// <param name="newValue">The new value to set.</param>
        /// <param name="originalValue">The original value before it was exchanged.</param>
        /// <returns>True if the value was exchanged; false otherwise.</returns>
        public static bool ExchangeIfLessThanValue(ref long value, long newValue, out long originalValue)
        {
            bool exchanged;
            bool lessThan;
            do
            {
                long localValueLong = Interlocked.Read(ref value);

                lessThan = newValue < localValueLong;
                if (lessThan)
                {
                    exchanged = Interlocked.CompareExchange(ref value, newValue, localValueLong) == localValueLong;
                }
                else
                {
                    exchanged = false;
                }

                originalValue = localValueLong;
            }
            while (lessThan && !exchanged);

            return exchanged;
        }

        /// <summary>
        /// Adds the value to the internally stored value.
        /// </summary>
        /// <param name="value">The value to add.</param>
        /// <returns>The new value stored.</returns>
        public long Add(long value)
        {
            return Interlocked.Add(ref this.value, value);
        }

        /// <summary>
        /// Compares <see cref="Value"/> and <paramref name="comparand"/> for
        /// equality and, if they are equal, replaces <see cref="Value"/>
        /// with <paramref name="newValue"/>.
        /// </summary>
        /// <param name="newValue">The new value to set.</param>
        /// <param name="comparand">The value to compare against.</param>
        /// <param name="originalValue">The original value before it was exchanged.</param>
        /// <returns>True if the value was exchanged; false otherwise.</returns>
        public bool CompareExchange(long newValue, long comparand, out long originalValue)
        {
            originalValue = this.CompareExchange(newValue, comparand);
            return originalValue == comparand;
        }

        /// <summary>
        /// Compares <see cref="Value"/> and <paramref name="comparand"/> for
        /// equality and, if they are equal, replaces <see cref="Value"/>
        /// with <paramref name="newValue"/>.
        /// </summary>
        /// <param name="newValue">The new value to set.</param>
        /// <param name="comparand">The value to compare against.</param>
        /// <returns>The original value.</returns>
        public long CompareExchange(long newValue, long comparand)
        {
            return Interlocked.CompareExchange(ref this.value, newValue, comparand);
        }

        /// <summary>
        /// Exchanges the value for <paramref name="newValue"/>.
        /// </summary>
        /// <param name="newValue">The new value to set.</param>
        /// <returns>The original value.</returns>
        public long Exchange(long newValue)
        {
            return Interlocked.Exchange(ref this.value, newValue);
        }

        /// <summary>
        /// Exchanges the value if it was greater than a specified value.
        /// </summary>
        /// <param name="newValue">The new value to set.</param>
        /// <param name="comparand">The value to compare against.</param>
        /// <returns>The original value before it was exchanged.</returns>
        public long ExchangeIfGreaterThan(long newValue, long comparand)
        {
            long originalValue;
            this.ExchangeIfGreaterThan(newValue, comparand, out originalValue);
            return originalValue;
        }

        /// <summary>
        /// Exchanges the value if it was greater than a specified value.
        /// </summary>
        /// <param name="newValue">The new value to set.</param>
        /// <param name="comparand">The value to compare against.</param>
        /// <param name="originalValue">The original value before it was exchanged.</param>
        /// <returns>True if the value was exchanged; false otherwise.</returns>
        public bool ExchangeIfGreaterThan(long newValue, long comparand, out long originalValue)
        {
            return ExchangeIfGreaterThan(ref this.value, newValue, comparand, out originalValue);
        }

        /// <summary>
        /// Exchanges the value if it was greater than a specified value.
        /// </summary>
        /// <param name="newValue">The new value to set.</param>
        /// <returns>The original value before it was exchanged.</returns>
        public long ExchangeIfGreaterThanValue(long newValue)
        {
            long originalValue;
            ExchangeIfGreaterThanValue(ref this.value, newValue, out originalValue);
            return originalValue;
        }

        /// <summary>
        /// Exchanges the value if it was less than a specified value.
        /// </summary>
        /// <param name="newValue">The new value to set.</param>
        /// <param name="comparand">The value to compare against.</param>
        /// <returns>The original value before it was exchanged.</returns>
        public long ExchangeIfLessThan(long newValue, long comparand)
        {
            long originalValue;
            this.ExchangeIfLessThan(newValue, comparand, out originalValue);
            return originalValue;
        }

        /// <summary>
        /// Exchanges the value if it was less than a specified value.
        /// </summary>
        /// <param name="newValue">The new value to set.</param>
        /// <param name="comparand">The value to compare against.</param>
        /// <param name="originalValue">The original value before it was exchanged.</param>
        /// <returns>True if the value was exchanged; false otherwise.</returns>
        public bool ExchangeIfLessThan(long newValue, long comparand, out long originalValue)
        {
            return ExchangeIfLessThan(ref this.value, newValue, comparand, out originalValue);
        }

        /// <summary>
        /// Exchanges the value if it was less than a specified value.
        /// </summary>
        /// <param name="newValue">The new value to set.</param>
        /// <returns>The original value before it was exchanged.</returns>
        public long ExchangeIfLessThanValue(long newValue)
        {
            long originalValue;
            ExchangeIfLessThanValue(ref this.value, newValue, out originalValue);
            return originalValue;
        }

        /// <summary>
        /// Decrement the value.
        /// </summary>
        /// <returns>The new value stored.</returns>
        public long Decrement()
        {
            return Interlocked.Decrement(ref this.value);
        }

        /// <summary>
        /// Decrements the value if it is greater than <paramref name="comparand"/>.
        /// </summary>
        /// <param name="comparand">The value to compare against.</param>
        /// <param name="newValue">The value stored after the operation whether or not the decrement took place.</param>
        /// <returns>True if the value was decremented; false otherwise.</returns>
        public bool DecrementIfGreaterThan(long comparand, out long newValue)
        {
            bool exchanged;
            bool greater;
            do
            {
                long localValue = Interlocked.Read(ref this.value);

                greater = localValue > comparand;
                if (greater)
                {
                    long localNewValue = localValue - 1;
                    long originalValue = Interlocked.CompareExchange(ref this.value, localNewValue, localValue);
                    exchanged = originalValue == localValue;
                    newValue = exchanged ? localNewValue : originalValue;
                }
                else
                {
                    exchanged = false;
                    newValue = localValue;
                }
            }
            while (greater && !exchanged);

            return exchanged;
        }

        /// <summary>
        /// Decrements the value if it is less than <paramref name="comparand"/>.
        /// </summary>
        /// <param name="comparand">The value to compare against.</param>
        /// <param name="newValue">The stored after the operation whether or not the decrement took place.</param>
        /// <returns>True if the value was decremented; false otherwise.</returns>
        public bool DecrementIfLessThan(long comparand, out long newValue)
        {
            bool exchanged;
            bool less;
            do
            {
                long localValue = Interlocked.Read(ref this.value);

                less = localValue < comparand;
                if (less)
                {
                    long localNewValue = localValue - 1;
                    long originalValue = Interlocked.CompareExchange(ref this.value, localNewValue, localValue);
                    exchanged = originalValue == localValue;
                    newValue = exchanged ? localNewValue : originalValue;
                }
                else
                {
                    exchanged = false;
                    newValue = localValue;
                }
            }
            while (less && !exchanged);

            return exchanged;
        }

        /// <summary>
        /// Increments the value.
        /// </summary>
        /// <returns>The new value stored.</returns>
        public long Increment()
        {
            return Interlocked.Increment(ref this.value);
        }

        /// <summary>
        /// Increments the value if it is greater than <paramref name="comparand"/>.
        /// </summary>
        /// <param name="comparand">The value to compare against.</param>
        /// <param name="newValue">The stored after the operation whether or not the increment took place.</param>
        /// <returns>True if the value was incremented; false otherwise.</returns>
        public bool IncrementIfGreaterThan(long comparand, out long newValue)
        {
            bool exchanged;
            bool greater;
            do
            {
                long localValue = Interlocked.Read(ref this.value);

                greater = localValue > comparand;
                if (greater)
                {
                    long localNewValue = localValue + 1;
                    long originalValue = Interlocked.CompareExchange(ref this.value, localNewValue, localValue);
                    exchanged = originalValue == localValue;
                    newValue = exchanged ? localNewValue : originalValue;
                }
                else
                {
                    exchanged = false;
                    newValue = localValue;
                }
            }
            while (greater && !exchanged);

            return exchanged;
        }

        /// <summary>
        /// Increments the value if it is less than <paramref name="comparand"/>.
        /// </summary>
        /// <param name="comparand">The value to compare against.</param>
        /// <param name="newValue">The stored after the operation whether or not the increment took place.</param>
        /// <returns>True if the value was incremented; false otherwise.</returns>
        public bool IncrementIfLessThan(long comparand, out long newValue)
        {
            bool exchanged;
            bool less;
            do
            {
                long localValue = Interlocked.Read(ref this.value);

                less = localValue < comparand;
                if (less)
                {
                    long localNewValue = localValue + 1;
                    long originalValue = Interlocked.CompareExchange(ref this.value, localNewValue, localValue);
                    exchanged = originalValue == localValue;
                    newValue = exchanged ? localNewValue : originalValue;
                }
                else
                {
                    exchanged = false;
                    newValue = localValue;
                }
            }
            while (less && !exchanged);

            return exchanged;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return this.Value.ToString();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <returns>A string that represents the current object.</returns>
        public string ToString(string format)
        {
            return this.Value.ToString(format);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>A string that represents the current object.</returns>
        public string ToString(IFormatProvider provider)
        {
            return this.Value.ToString(provider);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>A string that represents the current object.</returns>
        public string ToString(string format, IFormatProvider provider)
        {
            return this.Value.ToString(format, provider);
        }
    }
}