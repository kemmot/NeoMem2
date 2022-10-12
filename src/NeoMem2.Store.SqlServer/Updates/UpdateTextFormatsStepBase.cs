using System;

using NeoMem2.Automation.Updates.Data;
using NeoMem2.Core;
using NeoMem2.Utils;

namespace NeoMem2.Data.SqlServer.Updates
{
    public abstract class UpdateTextFormatsStepBase : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute to insert a text format.
        /// </summary>
        private const string InsertSql = "INSERT INTO dbo.TextFormat (Id, Name, Description) VALUES ({0}, '{1}', '{2}')";

        /// <summary>
        /// The SQL statement to execute to update a text format.
        /// </summary>
        private const string UpdateSql = "UPDATE dbo.TextFormat SET Name = '{0}', Description = '{1}' WHERE Id = {2}";
        
        protected void ProcessTextFormats()
        {
            foreach (TextFormat textFormat in Enum.GetValues(typeof(TextFormat)))
            {
                ProcessTextFormat(textFormat);
            }
        }

        protected void ProcessTextFormat(TextFormat textFormat)
        {
            int id = (int)textFormat;
            string name = textFormat.ToString();
            string description = new EnumWrapper<TextFormat>(textFormat).Description;

            if (CheckTextFormatExists(id))
            {
                UpdateTextFormat(id, name, description);
            }
            else
            {
                InsertTextFormat(id, name, description);
            }
        }

        private bool CheckTextFormatExists(int id)
        {
            string sql = string.Format("SELECT COUNT(*) FROM dbo.TextFormat WHERE Id = {0}", id);
            object result = Context.ExecuteDefaultScalarText(sql);
            int count = Convert.ToInt32(result);
            return count > 0;
        }

        private void InsertTextFormat(int id, string name, string description)
        {
            string sql = string.Format(InsertSql, id, name, description);
            Context.ExecuteDefaultNonQueryText(sql);
        }

        private void UpdateTextFormat(int id, string name, string description)
        {
            string sql = string.Format(UpdateSql, name, description, id);
            Context.ExecuteDefaultNonQueryText(sql);
        }
    }
}
