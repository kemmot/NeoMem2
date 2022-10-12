// <copyright file="Step01RemoveUnsupportedTextFormatData.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_1.Version_1_0
{
    using System;
    using System.Data;

    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;
    using NeoMem2.Core;

    /// <summary>
    /// Update to version 1.0 step 1.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 1.0, 1, "Remove unsupported TextFormat data")]
    public class Step01RemoveUnsupportedTextFormatData : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql1 = "SELECT Id, Name, Description FROM dbo.TextFormat";

        /// <summary>
        /// The SQL statement to execute for change 2.
        /// </summary>
        private const string Sql2 = "UPDATE dbo.Note SET TextFormat = {0} WHERE TextFormat = {1}";

        /// <summary>
        /// The SQL statement to execute for change 3.
        /// </summary>
        private const string Sql3 = "DELETE FROM dbo.TextFormat WHERE Id = {0}";

        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            const int DefaultTextFormatId = (int)TextFormat.Txt;
            DataTable textFormats = Context.ExecuteDefaultDataSetText(Sql1);
            foreach (DataRow row in textFormats.Rows)
            {
                int id = Convert.ToInt32(row["Id"]);
                if (!Enum.IsDefined(typeof(TextFormat), id))
                {
                    Context.ExecuteDefaultNonQueryText(Sql2, DefaultTextFormatId, id);
                    Context.ExecuteDefaultNonQueryText(Sql3, id);
                }
            }
        }
    }
}
