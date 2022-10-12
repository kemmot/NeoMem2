// <copyright file="Step02InsertTextFormatData.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_0.Version_0_9
{
    using System;

    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;
    using NeoMem2.Core;
    using NeoMem2.Utils;

    /// <summary>
    /// Update to version 0.9 step 2.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 0.9, 2, "Insert TextFormat data")]
    public class Step02InsertTextFormatData : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql1 = "INSERT INTO dbo.TextFormat (Id, Name, Description) VALUES ({0}, '{1}', '{2}')";

        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            foreach (TextFormat textFormat in Enum.GetValues(typeof(TextFormat)))
            {
                string description = new EnumWrapper<TextFormat>(textFormat).Description;
                string sql = string.Format(Sql1, (int)textFormat, textFormat.ToString(), description);
                Context.ExecuteDefaultNonQueryText(sql);
            }
        }
    }
}
