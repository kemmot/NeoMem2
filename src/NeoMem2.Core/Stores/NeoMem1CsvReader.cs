// <copyright file="NeoMem1CsvReader.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace NeoMem2.Core.Stores
{
    public class NeoMem1CsvReader : IImporter
    {
        private const int CustomPropertyStartIndex = 9;
        private const int SystemPropertyCount = 10;

        private string m_FileName;
        private readonly NeoMemFile m_File = new NeoMemFile();

        public NeoMem1CsvReader(string fileName)
        {
            m_FileName = fileName;
        }

        public NeoMem1CsvReader()
        {
        }

        public string ConnectionString { get { return m_FileName; } set { m_FileName = value; } }
        private int RtfFieldIndex { get { return m_File.ColumnNames.Count - 2; } }

        public NeoMemFile Read()
        {
            using (StreamReader reader = new StreamReader(m_FileName))
            {
                ParseHeaderLine(reader.ReadLine());
                ParseNotes(reader);
            }

            OrganiseNotes();

            return m_File;
        }

        private void ParseHeaderLine(string headerLine)
        {
            string[] columnNames = Split(headerLine);
            for (int columnIndex = 0; columnIndex < columnNames.Length; columnIndex++)
            {
                string columnName = columnNames[columnIndex];
                //if (columnName == "Text (Rtf)")
                //{
                //    m_RtfColumnIndex = columnIndex;
                //}

                m_File.ColumnNames.Add(columnName);
            }
        }

        private void ParseNotes(StreamReader reader)
        {
            Regex startOfNoteRegex = new Regex("^\\d{1,7},[a-zA-Z0-9\" ]+,\\d{1,7},");

            string line;
            StringBuilder noteLine = null;
            while ((line = reader.ReadLine()) != null)
            {
                if (startOfNoteRegex.IsMatch(line))
                {
                        if (noteLine != null)
                        {
                            m_File.AllNotes.Add(ParseNoteLine(noteLine.ToString()));
                        }

                        noteLine = new StringBuilder();
                        noteLine.AppendLine(line);
                }
                else
                {
                    if (noteLine != null)
                    {
                        noteLine.AppendLine(line);
                    }
                }
            }

            if (noteLine != null && noteLine.Length > 0)
            {
                m_File.AllNotes.Add(ParseNoteLine(noteLine.ToString()));
            }
        }

        private Note ParseNoteLine(string noteLine)
        {
            Queue<string> rawFields = new Queue<string>(Split(noteLine));

            Queue<string> parseFields;
            if (rawFields.Count > m_File.ColumnNames.Count)
            {
                parseFields = CollapseFields(rawFields);
            }
            else
            {
                parseFields = rawFields;
            }

            return ParseNoteProperties(parseFields);
        }

        private Note ParseNoteProperties(Queue<string> parseFields)
        {
            Note note = new Note();

            int fieldIndex = 0;
            while (parseFields.Count > 0)
            {
                string propertyName = m_File.ColumnNames[fieldIndex];
                string propertyValue = parseFields.Dequeue();
                ParseNoteProperty(note, propertyName, propertyValue);
                fieldIndex++;
            }

            return note;
        }

        private void ParseNoteProperty(Note note, string propertyName, string propertyValue)
        {
            switch (propertyName)
            {
                case "Text (Rtf)":
                    note.FormattedText = propertyValue;
                    break;
                case "Text (Plain)":
                    note.Text = propertyValue;
                    break;
                case "ObjectID":
                    note.Id = long.Parse(propertyValue);
                    break;
                case "Class":
                    note.Class = propertyValue;
                    break;
                case "Class_value":
                    note.ClassValue = long.Parse(propertyValue);
                    break;
                case "Name":
                    note.Name = propertyValue;
                    break;
                case "Location (Parent)_value":
                    note.ParentId = long.Parse(propertyValue);
                    break;
                case "Description":
                    note.Description = propertyValue;
                    break;
                case "Size":
                    long size;
                    if (long.TryParse(propertyValue, out size))
                    {
                        note.Size = size;
                    }
                    break;
                case "Location (Parent)":
                    // do nothing
                    break;
                default:
                    if (!string.IsNullOrEmpty(propertyValue))
                    {
                        note.AddProperty(new Property(propertyName, propertyValue));
                    }
                    break;
            }
        }

        private Queue<string> CollapseFields(Queue<string> rawFields)
        {            
            Queue<string> collapsedFields = new Queue<string>();
            int columnIndex = 0;
            while (columnIndex < RtfFieldIndex)
            {
                collapsedFields.Enqueue(rawFields.Dequeue());
                columnIndex++;
            }

            int rtfFieldCount = rawFields.Count / 2;
            StringBuilder rtfField = new StringBuilder();
            for (int rtfFieldIndex = 0; rtfFieldIndex < rtfFieldCount; rtfFieldIndex++)
            {
                rtfField.Append(rawFields.Dequeue());
            }
            collapsedFields.Enqueue(rtfField.ToString());

            StringBuilder txtField = new StringBuilder();
            while (rawFields.Count > 0)
            {
                txtField.Append(rawFields.Dequeue());
            }
            collapsedFields.Enqueue(txtField.ToString());

            return collapsedFields;
        }

        private string[] Split(string line)
        {
            List<string> fields = new List<string>();

            string field;
            int position = 0;
            while (TryParseField(line, ref position, out field))
            {
                fields.Add(field);
            }

            return fields.ToArray();
        }

        private static bool TryParseField(string line, ref int position, out string field)
        {
            bool success;

            int nextCommaPosition = line.IndexOf(',', position);
            int nextQuotePosition = line.IndexOf('"', position);

            const string middleQuote = "\",";

            if (position >= line.Length - 1)
            {
                success = false;
                field = string.Empty;
            }
            else
            {
                int startFieldPosition;
                int endFieldPosition;
                int nextFieldStartPosition;
                if (nextQuotePosition == position)
                {
                    startFieldPosition = nextQuotePosition + 1;

                    int endQuotePosition = line.IndexOf(middleQuote, nextQuotePosition + 1);
                    if (endQuotePosition >= 0)
                    {
                        endFieldPosition = endQuotePosition;
                        nextFieldStartPosition = endFieldPosition + middleQuote.Length;
                    }
                    else
                    {
                        endFieldPosition = line.Length - 1;
                        nextFieldStartPosition = line.Length;
                    }
                    success = true;
                }
                else
                {
                    startFieldPosition = position;
                    if (nextCommaPosition >= 0)
                    {
                        endFieldPosition = nextCommaPosition;
                        nextFieldStartPosition = nextCommaPosition + 1;
                        success = true;
                    }
                    else
                    {
                        endFieldPosition = line.Length - 1;
                        nextFieldStartPosition = line.Length - 1;
                        success = true;
                    }
                }

                field = line.Substring(startFieldPosition, endFieldPosition - startFieldPosition);
                position = nextFieldStartPosition;
            }

            return success;
        }

        private void OrganiseNotes()
        {
            Dictionary<long, Note> notesById = new Dictionary<long, Note>();
            foreach (Note note in m_File.AllNotes.GetNotes())
            {
                notesById[note.Id] = note;
            }

            foreach (Note note in m_File.AllNotes.GetNotes())
            {
                Note parentNote;
                if (notesById.TryGetValue(note.ParentId, out parentNote))
                {
                    note.Parent = parentNote;
                    parentNote.Children.Add(note);
                }
                else
                {
                    m_File.RootNotes.Add(note);
                }
            }
        }
    }
}
