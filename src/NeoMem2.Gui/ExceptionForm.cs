using System;
using System.IO;
using System.Windows.Forms;

namespace NeoMem2.Gui
{
    public partial class ExceptionForm : Form
    {
        public ExceptionForm()
        {
            InitializeComponent();
        }

        public void SetException(Exception exception)
        {
            richTextBox1.Text = exception.ToString();
        }

        private void BtnCopyClick(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.Text, TextDataFormat.Text);
            Clipboard.SetText(richTextBox1.Text, TextDataFormat.UnicodeText);
        }

        private void BtnSaveClick(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text);
            }
        }
    }
}
