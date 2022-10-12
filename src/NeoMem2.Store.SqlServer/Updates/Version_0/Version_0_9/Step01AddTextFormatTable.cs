// <copyright file="Step01AddTextFormatTable.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_0.Version_0_9
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 0.9 step 1.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 0.9, 1, "Create TextFormat table")]
    public class Step01AddTextFormatTable : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql1 = @"
            CREATE TABLE dbo.TextFormat
            (
                Id int NOT NULL,
                Name nvarchar(50) NOT NULL,
                Description nvarchar(100) NOT NULL
            )  ON [PRIMARY]";

        /// <summary>
        /// The SQL statement to execute for change 2.
        /// </summary>
        private const string Sql2 = @"
            ALTER TABLE dbo.TextFormat ADD CONSTRAINT
            PK_TextFormat PRIMARY KEY CLUSTERED 
            (
                Id
            ) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
        
        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            Context.ExecuteDefaultNonQueryText(Sql1);
            Context.ExecuteDefaultNonQueryText(Sql2);
        }
    }
}
