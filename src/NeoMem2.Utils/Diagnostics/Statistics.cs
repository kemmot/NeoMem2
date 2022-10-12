// <copyright file="Statistics.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Utils.Diagnostics
{
    using System;
    using System.Text;

    /// <summary>
    /// Holds statistics related to operations within the application.
    /// </summary>
    public class Statistics
    {
        /// <summary>
        /// The backing field for the <see cref="StaticInstance"/> property.
        /// </summary>
        private static readonly Statistics StaticInstance = new Statistics();

        /// <summary>
        /// Records statistics for all data access operations.
        /// </summary>
        private readonly StatisticsItem allDataAccessStatistics = new StatisticsItem();

        /// <summary>
        /// Records statistics for deletion operations.
        /// </summary>
        private readonly StatisticsItem deleteNoteStatistics = new StatisticsItem();

        /// <summary>
        /// Records statistics for insertion operations.
        /// </summary>
        private readonly StatisticsItem insertNoteStatistics = new StatisticsItem();

        /// <summary>
        /// Records statistics for query operations.
        /// </summary>
        private readonly StatisticsItem selectNoteStatistics = new StatisticsItem();

        /// <summary>
        /// Records statistics for update operations.
        /// </summary>
        private readonly StatisticsItem updateNoteStatistics = new StatisticsItem();

        /// <summary>
        /// Initializes static members of the <see cref="Statistics" /> class.
        /// </summary>
        static Statistics()
        {
            StaticInstance = new Statistics();
        }

        /// <summary>
        /// Gets an easily accessible instance of the class.
        /// </summary>
        public static Statistics Instance
        {
            get { return StaticInstance; }
        }

        /// <summary>
        /// Records the statistics for a deletion operation.
        /// </summary>
        /// <param name="timeTaken">The elapsed time for the operation.</param>
        public void RecordDeletion(TimeSpan timeTaken)
        {
            this.deleteNoteStatistics.Record(timeTaken);
            this.allDataAccessStatistics.Record(timeTaken);
        }

        /// <summary>
        /// Records the statistics for an insertion operation.
        /// </summary>
        /// <param name="timeTaken">The elapsed time for the operation.</param>
        public void RecordInsertion(TimeSpan timeTaken)
        {
            this.insertNoteStatistics.Record(timeTaken);
            this.allDataAccessStatistics.Record(timeTaken);
        }

        /// <summary>
        /// Records the statistics for a query operation.
        /// </summary>
        /// <param name="timeTaken">The elapsed time for the operation.</param>
        public void RecordQuery(TimeSpan timeTaken)
        {
            this.selectNoteStatistics.Record(timeTaken);
            this.allDataAccessStatistics.Record(timeTaken);
        }

        /// <summary>
        /// Records the statistics for an update operation.
        /// </summary>
        /// <param name="timeTaken">The elapsed time for the operation.</param>
        public void RecordUpdate(TimeSpan timeTaken)
        {
            this.updateNoteStatistics.Record(timeTaken);
            this.allDataAccessStatistics.Record(timeTaken);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            StringBuilder description = new StringBuilder();
            description.AppendLine(this.deleteNoteStatistics.ToString());
            description.AppendLine(this.insertNoteStatistics.ToString());
            description.AppendLine(this.selectNoteStatistics.ToString());
            description.AppendLine(this.updateNoteStatistics.ToString());
            return description.ToString();
        }
    }
}
