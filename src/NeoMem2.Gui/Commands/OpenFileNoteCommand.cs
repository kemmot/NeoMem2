using System;
using System.Diagnostics;
using System.IO;
using NeoMem2.Core;

namespace NeoMem2.Gui.Commands
{
    public class OpenFileNoteCommand : SingleNoteCommand
    {
        protected override bool DoIsSupported(Note note)
        {
            return note.Namespace == NoteNamespace.File;
        }

        protected override void DoExecute(Note note)
        {
            string path = note.Text;
            if (!File.Exists(path))
            {
                throw new Exception($"File does not exist: {path}");
            }

            var info = new ProcessStartInfo(note.Text);
            info.UseShellExecute = true;
            Process.Start(info);
        }
    }
}