// <copyright file="StatisticsItem.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Utils.Diagnostics
{
    using System;
    using System.Text;

    using NeoMem2.Utils.Threading;

    /// <summary>
    /// Records statistics related to a specific item.
    /// </summary>
    public class StatisticsItem
    {
        /// <summary>
        /// The number of times this operation has been recorded.
        /// </summary>
        private readonly InterlockedInt64 count = new InterlockedInt64();

        /// <summary>
        /// The average number of ticks recorded for this operation.
        /// </summary>
        private readonly InterlockedInt64 timeAverage = new InterlockedInt64();

        /// <summary>
        /// The cumulative number of ticks recorded for this operation.
        /// </summary>
        private readonly InterlockedInt64 timeCumulative = new InterlockedInt64();

        /// <summary>
        /// The most recent number of ticks recorded for this operation.
        /// </summary>
        private readonly InterlockedInt64 timeLast = new InterlockedInt64();

        /// <summary>
        /// The maximum number of ticks recorded for this operation.
        /// </summary>
        private readonly InterlockedInt64 timeMaximum = new InterlockedInt64();

        /// <summary>
        /// The minimum number of ticks recorded for this operation.
        /// </summary>
        private readonly InterlockedInt64 timeMinimum = new InterlockedInt64();

        /// <summary>
        /// Records an operation taking the specified elapsed time.
        /// </summary>
        /// <param name="timeTaken">The elapsed time for the operation.</param>
        public void Record(TimeSpan timeTaken)
        {
            long timeTakenTicks = timeTaken.Ticks;
            long newCount = this.count.Increment();
            long newTimeCumulative = this.timeCumulative.Add(timeTaken.Ticks);
            this.timeLast.Exchange(timeTakenTicks);
            this.timeAverage.Exchange(newTimeCumulative / newCount);
            this.timeMaximum.ExchangeIfGreaterThanValue(timeTakenTicks);
            this.timeMinimum.ExchangeIfLessThanValue(timeTakenTicks);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            StringBuilder description = new StringBuilder();
            description.AppendFormat("Count: {0}", this.count.Value);
            description.AppendFormat(", Last: {0}", new TimeSpan(this.timeLast.Value));
            description.AppendFormat(", Minimum: {0}", new TimeSpan(this.timeMinimum.Value));
            description.AppendFormat(", Maximum: {0}", new TimeSpan(this.timeMaximum.Value));
            description.AppendFormat(", Average: {0}", new TimeSpan(this.timeAverage.Value));
            return description.ToString();
        }
    }
}