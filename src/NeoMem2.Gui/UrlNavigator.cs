using System;
using System.Text;

using NeoMem2.Core;
using NeoMem2.Gui.Models;

namespace NeoMem2.Gui
{
    public class UrlNavigator
    {
        private readonly Model m_Model;

        public UrlNavigator(Model model)
        {
            if (model == null) throw new ArgumentNullException("model");

            m_Model = model;
        }

        public UrlNavigateResult IdentifyLink(Uri url)
        {
            return IdentifyLink(url.ToString());
        }

        public UrlNavigateResult IdentifyLink(string urlString)
        {
            UrlNavigateResult result = new UrlNavigateResult();
            if (IsUrlBlank(urlString))
            {
                result.Destination = urlString;
                result.NavigateType = UrlNavigateType.None;
            }
            else if (IsWebUrl(urlString))
            {
                result.Destination = urlString;
                result.NavigateType = UrlNavigateType.Web;
            }
            else
            {
                string[] urlParts = urlString.Split(':');
                string urlNotePart = urlParts[1];

                Note note;
                if (IsNoteIdLink(urlNotePart, out note))
                {
                    result.NavigateType = UrlNavigateType.Note;
                    result.Note = note;
                }
                else if (IsNoteNameLink(urlNotePart, out note))
                {
                    result.NavigateType = UrlNavigateType.Note;
                    result.Note = note;
                }
                else
                {
                    result.NavigateType = UrlNavigateType.Search;
                }

                result.Destination = urlNotePart;
            }

            return result;
        }

        private bool IsUrlBlank(string urlString)
        {
            return urlString == "about:blank";
        }

        private bool IsWebUrl(string urlString)
        {
            return urlString.ToUpper().StartsWith("HTTP://");
        }

        private bool IsNoteIdLink(string urlString, out Note note)
        {
            long noteId;
            note = long.TryParse(urlString, out noteId) ? m_Model.GetNoteById(noteId) : null;
            return note != null;
        }

        private bool IsNoteNameLink(string urlString, out Note note)
        {
            note = m_Model.GetNoteByName(urlString);
            return note != null;
        }
    }

    public class UrlNavigateResult
    {
        public UrlNavigateType NavigateType { get; set; }
        public Note Note { get; set; }
        public string Destination { get; set; }

        public override string ToString()
        {
            var description = new StringBuilder();
            switch (NavigateType)
            {
                case UrlNavigateType.None:
                    description.Append("Not navigating");
                    break;
                case UrlNavigateType.Note:
                    description.AppendFormat("Navigating to note, ID: {0}, name: {1}", Note.Id, Note.Name);
                    break;
                case UrlNavigateType.Search:
                    description.Append("Searching for " + Destination);
                    break;
                case UrlNavigateType.Web:
                    description.Append("Navigating to URL: " + Destination);
                    break;
            }

            return description.ToString();
        }
    }

    public enum UrlNavigateType
    {
        None,
        Web,
        Note,
        Search
    }
}
