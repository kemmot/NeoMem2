using System.Windows.Forms;

using Fireball.CodeEditor.SyntaxFiles;
using Fireball.Windows.Forms;

namespace NeoMem2.Gui
{
    public partial class InternalViewerForm : Form
    {
        private readonly CodeEditorControl m_Editor;

        public InternalViewerForm()
        {
            InitializeComponent();

            m_Editor = new CodeEditorControl();
            m_Editor.Dock = DockStyle.Fill;
            Controls.Add(m_Editor);
        }

        public void DisplayText(string text, SyntaxLanguage language)
        {
            m_Editor.Document.Text = text;
            CodeEditorSyntaxLoader.SetSyntax(m_Editor, language);
        }
    }
}
