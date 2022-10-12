// <copyright file="Step01CreateNoteTable.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_0.Version_0_2
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 0.2 step 2.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 0.2, 1, "Create note table")]
    public class Step01CreateNoteTable : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql1 = @"
            CREATE TABLE [dbo].[Note](
	            [Id] [bigint] IDENTITY(1,1) NOT NULL,
	            [Name] [nvarchar](255) NOT NULL,
	            [Text] [nvarchar](max) NOT NULL,
	            [CreatedDate] [datetime] NOT NULL,
	            [LastAccessedDate] [datetime] NOT NULL,
	            [LastModifiedDate] [datetime] NOT NULL,
	            [Tags] [nvarchar](255) NOT NULL,
	            [IsPinned] [bit] NOT NULL,
                CONSTRAINT [PK_Note] PRIMARY KEY CLUSTERED 
                (
                    [Id] ASC
                ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
            ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";

        /// <summary>
        /// The SQL statement to execute for change 2.
        /// </summary>
        private const string Sql2 = @"ALTER TABLE [dbo].[Note] ADD CONSTRAINT [DF_Note_Tags] DEFAULT ('') FOR [Tags]";

        /// <summary>
        /// The SQL statement to execute for change 3.
        /// </summary>
        private const string Sql3 = @"ALTER TABLE [dbo].[Note] ADD CONSTRAINT [DF_Note_IsPinned] DEFAULT ((0)) FOR [IsPinned]";

        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            Context.ExecuteDefaultNonQueryText(Sql1);
            Context.ExecuteDefaultNonQueryText(Sql2);
            Context.ExecuteDefaultNonQueryText(Sql3);
        }
    }
}
