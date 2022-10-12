using System;
using System.Threading;
using System.Windows.Forms;

using NeoMem2.Core;
using NeoMem2.Core.Stores;

namespace NeoMem2.Gui.Commands
{
    public class FileImportCommand : CommandBase
    {
        private Converter m_Converter;
        private ProgressForm m_ProgressForm;

        public FileImportCommand(MainForm form)
            : base(form)
        {
        }

        protected override void DoExecute(EventArgs e)
        {
            using (ImportForm form = new ImportForm())
            {
                form.ImportConnectionString = Properties.Settings.Default.ImportConnectionString;
                form.OutputConnectionString = Properties.Settings.Default.ExportConnectionString;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.ImportConnectionString = form.ImportConnectionString;
                    Properties.Settings.Default.ExportConnectionString = form.OutputConnectionString;
                    Properties.Settings.Default.Save();

                    m_Converter = new Converter
                    {
                        ImportConnectionString = form.ImportConnectionString,
                        Importer = ImporterFactory.Instance.GetImporter(form.SourceFormat),
                        ExportConnectionString = form.OutputConnectionString,
                        Exporter = ExporterFactory.Instance.GetExporter(form.DestinationFormat)
                    };

                    var htmlExporter = m_Converter.Exporter as HtmlExporter;
                    htmlExporter?.ClassesToExclude.AddRange(Properties.Settings.Default.HtmlExportClassesToExclude.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries));

                    m_ProgressForm = new ProgressForm();
                    m_ProgressForm.Title = "Importing";
                    m_ProgressForm.Show();
                    m_Converter.ProgressChanged += (sender, args) =>
                        {
                            ThreadStart del = delegate()
                                {
                                    m_ProgressForm.Status = args.UserState.ToString();
                                    m_ProgressForm.PercentageComplete = args.ProgressPercentage;
                                };
                            m_ProgressForm.Invoke(del);
                        };

                    ThreadPool.QueueUserWorkItem(Import);
                }
            }
        }

        private void Import(object state)
        {
            try
            {
                ConvertStats stats = m_Converter.Convert();

                string importStats = string.Format(
                    "Import complete, note count: {0}",
                    stats.NoteCount);
                MessageBox.Show(importStats);
            }
            catch (Exception ex)
            {
                var exceptionForm = new ExceptionForm();
                exceptionForm.Text = "Import Failed";
                exceptionForm.SetException(ex);
                exceptionForm.ShowDialog();
            }
            finally
            {
                ThreadStart del = delegate ()
                {
                    m_ProgressForm.Close();
                };
                m_ProgressForm.Invoke(del);
            }
        }
    }
}
