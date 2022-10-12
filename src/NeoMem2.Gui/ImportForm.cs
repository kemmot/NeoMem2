using System;
using System.Windows.Forms;

using NeoMem2.Core.Stores;

namespace NeoMem2.Gui
{
    public partial class ImportForm : Form
    {
        public ImportForm()
        {
            InitializeComponent();

            foreach (string importerName in ImporterFactory.Instance.GetImporterTypes())
            {
                CboSourceFormat.Items.Add(importerName);
            }
            CboSourceFormat.SelectedIndex = 0;

            foreach (string exporterName in ExporterFactory.Instance.GetExporterTypes())
            {
                CboDestinationFormat.Items.Add(exporterName);
            }
            CboDestinationFormat.SelectedIndex = 0;
        }

        public string SourceFormat { get { return (string)CboSourceFormat.SelectedItem; } }
        public string DestinationFormat { get { return (string)CboDestinationFormat.SelectedItem; } }
        public string ImportConnectionString { get { return CboInputFile.Text; } set { CboInputFile.Text = value; } }
        public string OutputConnectionString { get { return CboOutputFolder.Text; } set { CboOutputFolder.Text = value; } }

        private void BtnBrowseInputFileClick(object sender, EventArgs e)
        {
            openFileDialog1.FileName = CboInputFile.Text;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                CboInputFile.Text = openFileDialog1.FileName;
            }
        }

        private void BtnBrowseOutputFolderClick(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = CboOutputFolder.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                CboOutputFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
