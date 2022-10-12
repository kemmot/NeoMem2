// <copyright file="DateTimeExtensions.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Utils
{
    using System;

    /// <summary>
    /// Provides extension methods for the <see cref="DateTime"/> class.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns the date of the start of the month specified by this <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateInMonth">The date time to calculate from.</param>
        /// <returns>The result.</returns>
        public static DateTime GetStartOfMonth(this DateTime dateInMonth)
        {
            return new DateTime(dateInMonth.Year, dateInMonth.Month, 1);
        }

        /// <summary>
        /// Returns the date of the start of the week specified by this <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateInWeek">The date time to calculate from.</param>
        /// <returns>The result.</returns>
        /// <remarks>Uses Friday as the last day in the week.</remarks>
        public static DateTime GetStartOfWeek(this DateTime dateInWeek)
        {
            return dateInWeek.GetStartOfWeek(DayOfWeek.Friday);
        }

        /// <summary>
        /// Returns the date of the start of the week specified by this <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateInWeek">The date time to calculate from.</param>
        /// <param name="lastDayOfWeek">Allows a custom day of the week to be specified as the end of the week.</param>
        /// <returns>The result.</returns>
        public static DateTime GetStartOfWeek(this DateTime dateInWeek, DayOfWeek lastDayOfWeek)
        {
            return dateInWeek.GetEndOfWeek(lastDayOfWeek).AddDays(-6);
        }

        /// <summary>
        /// Returns the date of the end of the week specified by this <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateInWeek">The date time to calculate from.</param>
        /// <returns>The result.</returns>
        /// <remarks>Uses Friday as the last day in the week.</remarks>
        public static DateTime GetEndOfWeek(this DateTime dateInWeek)
        {
            return dateInWeek.GetEndOfWeek(DayOfWeek.Friday);
        }

        /// <summary>
        /// Returns the date of the end of the week specified by this <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateInWeek">The date time to calculate from.</param>
        /// <param name="lastDayOfWeek">Allows a custom day of the week to be specified as the end of the week.</param>
        /// <returns>The result.</returns>
        public static DateTime GetEndOfWeek(this DateTime dateInWeek, DayOfWeek lastDayOfWeek)
        {
            if (lastDayOfWeek != DayOfWeek.Friday)
            {
                string message = string.Format("Not implemented for end of week day: {0}", lastDayOfWeek);
                throw new NotImplementedException(message);
            }

            int dayOfWeekOffset;
            switch (dateInWeek.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    dayOfWeekOffset = 4;
                    break;
                case DayOfWeek.Tuesday:
                    dayOfWeekOffset = 3;
                    break;
                case DayOfWeek.Wednesday:
                    dayOfWeekOffset = 2;
                    break;
                case DayOfWeek.Thursday:
                    dayOfWeekOffset = 1;
                    break;
                case DayOfWeek.Friday:
                    dayOfWeekOffset = 0;
                    break;
                case DayOfWeek.Saturday:
                    dayOfWeekOffset = 6;
                    break;
                case DayOfWeek.Sunday:
                    dayOfWeekOffset = 5;
                    break;
                default:
                    throw new NotSupportedException();
            }

            DateTime date = dateInWeek.Date.AddDays(dayOfWeekOffset);
            return date;
        }

        /// <summary>
        /// Returns the date of the start of the year specified by this <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateInYear">The date time to calculate from.</param>
        /// <returns>The result.</returns>
        public static DateTime GetStartOfYear(this DateTime dateInYear)
        {
            return new DateTime(dateInYear.Year, 1, 1);
        }
    }
}
