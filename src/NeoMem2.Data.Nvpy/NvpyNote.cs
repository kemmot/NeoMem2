using System;
using System.Collections.Generic;
using System.ComponentModel;

using NeoMem2.Core;

namespace NeoMem2.Data.Nvpy
{
    public class NvpyNote : Note
    {
        public const string SystemTagPinned = "pinned";

        private readonly NvpyNoteMarkup m_InternalNote;


        internal NvpyNote(NvpyNoteMarkup internalNote)
        {
            if (internalNote == null) throw new ArgumentNullException("internalNote");

            m_InternalNote = internalNote;
        }


        internal NvpyNoteMarkup InternalNote { get { return m_InternalNote; } }


        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            switch (e.PropertyName)
            {
                case PropertyNameIsPinned:
                    if (IsPinned)
                    {
                        AddSystemTag(SystemTagPinned);
                    }
                    else
                    {
                        RemoveSystemTag(SystemTagPinned);
                    }
                    break;
                case PropertyNameTags:
                    SetTags();
                    break;
                case PropertyNameText:
                    m_InternalNote.content = Text;
                    break;
            }
        }

        private void AddSystemTag(string tag)
        {
            var newTags = new List<string>();
            if (m_InternalNote.systemtags != null)
            {
                newTags.AddRange(m_InternalNote.systemtags);
            }
            if (!newTags.Contains(tag))
            {
                newTags.Add(tag);
            }

            m_InternalNote.systemtags = newTags.ToArray();
        }

        private void RemoveSystemTag(string tag)
        {
            var newTags = new List<string>();
            if (m_InternalNote.systemtags != null)
            {
                newTags.AddRange(m_InternalNote.systemtags);
            }
            for (int index = newTags.Count - 1; index >= 0; index--)
            {
                if (newTags[index] == tag)
                {
                    newTags.RemoveAt(index);
                }
            }

            m_InternalNote.systemtags = newTags.ToArray();
        }

        private void SetTags()
        {
            InternalNote.tags = TagText.Split(';');
        }
    }
}
