using System.Collections.Generic;
using NeoMem2.Core;

namespace NeoMem2.Gui
{
    public class FileImportVerifier
    {
        public FileImportVerifier()
        {
            Add("\\.git\\");
            Add("\\$RECYCLE.BIN\\");
            Add("\\packages\\");
            Add("~");
            Add(".suo");
            Add(".dll");
            Add("\\obj\\debug\\");
        }

        public List<string> DisallowedStrings { get; } = new List<string>();

        public void Add(string disallowString)
        {
            DisallowedStrings.Add(disallowString.ToUpperInvariant());
        }

        public bool IsValid(FileNoteWrapper fileNote)
        {
            return IsValid(fileNote.Path);
        }

        public bool IsValid(string path)
        {
            string upperPath = path.ToUpperInvariant();

            bool isValid = true;
            foreach (string disallowString in DisallowedStrings)
            {
                if (upperPath.Contains(disallowString))
                {
                    isValid = false;
                    break;
                }
            }

            return isValid;
        }
    }
}