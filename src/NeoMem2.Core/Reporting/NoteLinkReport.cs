// <copyright file="NoteLinkReport.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Text;

namespace NeoMem2.Core.Reporting
{
    public class NoteLinkReport
    {
        private string m_CurrentIndent = "";
        private int m_CurrentDepth;
        private readonly List<Note> m_NotesSeen = new List<Note>();
        private readonly StringBuilder m_Report = new StringBuilder();


        public NoteLinkReport()
        {
            EmptyClass = "Note";
            IncludeClass = true;
            MaxDepth = 5;
        }


        public string EmptyClass { get; set; }
        public bool IncludeClass { get; set; }
        public int MaxDepth { get; set; }


        public string Generate(Note note)
        {
            AddNote(note);
            return m_Report.ToString();
        }

        private bool AddNote(Note note)
        {
            bool added;
            if (MaxDepth == 0 || m_CurrentDepth < MaxDepth)
            {
                m_CurrentDepth++;

                added = !m_NotesSeen.Contains(note);
                if (added)
                {
                    m_NotesSeen.Add(note);

                    m_Report.Append(m_CurrentIndent);
                    if (IncludeClass)
                    {
                        string classToUse = string.IsNullOrEmpty(note.Class) ? EmptyClass : note.Class;
                        if (!string.IsNullOrEmpty(classToUse))
                        {
                            m_Report.AppendFormat("({0}) ", classToUse);
                        }
                    }
                    m_Report.Append(note.Name);
                    m_Report.AppendLine();

                    string preIndex = m_CurrentIndent;
                    m_CurrentIndent += "\t";

                    if (AddLinkedNotes(note) > 0)
                    {
                        //m_Report.AppendLine();
                    }

                    m_CurrentIndent = preIndex;
                }

                m_CurrentDepth--;
            }
            else
            {
                added = false;
            }

            return added;
        }

        private int AddLinkedNotes(Note note)
        {
            var subNotes = new List<Note>();
            foreach (var noteLink in note.LinkedNotes)
            {
                subNotes.Add(noteLink.Note1 == note ? noteLink.Note2 : noteLink.Note1);
            }

            subNotes.Sort((left, right) =>
            {
                int result = left.Class.CompareTo(right.Class);
                if (result == 0)
                {
                    result = left.Name.CompareTo(right.Name);
                }
                return result;
            });

            int addCount = 0;
            foreach (var subNote in subNotes)
            {
                if (AddNote(subNote))
                {
                    addCount++;
                }
            }

            return addCount;
        }
    }
}
