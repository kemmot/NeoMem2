// <copyright file="NotePropertyReport.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

using System.Text;
using NeoMem2.Utils.Text;

namespace NeoMem2.Core.Reporting
{
    public class NotePropertyReport
    {
        public string Generate(Note note)
        {
            var output = new StringBuilder();
            output.AppendLine(note.Name);
            foreach (var p in note.Properties)
            {
                output.AppendLine("\t{0} = {1}", p.Name, p.Value);
            }

            return output.ToString();
        }
    }
}
