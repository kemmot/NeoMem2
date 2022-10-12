// <copyright file="Step02CreateTagTable.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_1.Version_1_2
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 1.2 step 2.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 1.2, 2, "Create Tag table")]
    public class Step02CreateTagTable : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql = @"
            CREATE TABLE [dbo].[Tag](
	            [Id] [int] IDENTITY(1,1) NOT NULL,
	            [Name] [nvarchar](255) NOT NULL,
                CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED 
                (
                    [Id] ASC
                ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
            ) ON [PRIMARY]";

        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            Context.ExecuteDefaultNonQueryText(Sql);
        }
    }
}
