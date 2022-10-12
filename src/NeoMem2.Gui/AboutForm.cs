using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

using NeoMem2.Utils;
using NeoMem2.Utils.Diagnostics;

namespace NeoMem2.Gui
{
    public partial class AboutForm : Form
    {
        private readonly Dictionary<string, ListViewItem> m_Items = new Dictionary<string, ListViewItem>();

        public AboutForm()
        {
            InitializeComponent();
        }

        public GcMonitor GcMonitor { get; set; }

        private void Panel1Paint(object sender, PaintEventArgs e)
        {
            FillTextBox(RtxtRoadmap, "Roadmap.txt");
            FillTextBox(RtxtReleaseNotes, "ReleaseNotes.txt");
            FillTextBox(RtxtFeatures, "Features.txt");            
        }

        private void FillTextBox(RichTextBox box, string resourceShortName)
        {
            string resourceFullName = "NeoMem2.Gui.Resources." + resourceShortName;
            using (var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceFullName))
            {
                if (resourceStream == null)
                {
                    box.Text = string.Format("Resource not found: {0}", resourceFullName);
                }
                else
                {
                    box.LoadFile(resourceStream, RichTextBoxStreamType.PlainText);
                }
            }
        }

        private void AboutFormFormClosed(object sender, FormClosedEventArgs e)
        {
            if (GcMonitor != null)
            {
                GcMonitor.GcStatusChanged -= GcMonitorGcStatusChanged;
                GcMonitor.EnableMemoryMonitoring = false;
            }
        }

        private void AboutFormLoad(object sender, System.EventArgs e)
        {
            if (GcMonitor != null)
            {
                GcMonitor.GcStatusChanged += GcMonitorGcStatusChanged;
                GcMonitor.EnableMemoryMonitoring = true;
            }

            RtxtStatistics.Text = Statistics.Instance.ToString();

            LblVersion.Text = "Version " + Assembly.GetEntryAssembly().GetName().Version;
        }

        private void GcMonitorGcStatusChanged(object sender, GcMonitorEventArgs e)
        {
            ThreadStart del = delegate
            {
                AddOrUpdateValue("Total Memory", e.TotalMemory);

                foreach (var customProperty in e.CustomProperties)
                {
                    AddOrUpdateValue(customProperty.Key, customProperty.Value);
                }

                foreach (var generation in e.Generations.Values)
                {
                    AddOrUpdateValue(string.Format("Gen {0} Collection Count", generation.GenerationId), generation.CollectionCount);
                    AddOrUpdateValue(string.Format("Gen {0} Last Collection Time", generation.GenerationId), generation.LastCollectionTime.ToString());

                    foreach (var customProperty in generation.CustomProperties)
                    {
                        AddOrUpdateValue(customProperty.Key, customProperty.Value);
                    }
                }

                foreach (ColumnHeader column in LvMemoryDetails.Columns)
                {
                    column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                }
            };
            Invoke(del);
        }
        
        private void AddOrUpdateValue(string key, object value)
        {
            string valueString = value == null ? "[null]" : value.ToString();

            ListViewItem item;
            if (!m_Items.TryGetValue(key, out item))
            {
                item = new ListViewItem();
                item.Text = key;
                item.SubItems.Add(valueString);

                m_Items[key] = item;
                LvMemoryDetails.Items.Add(item);
            }
            else
            {
                item.SubItems[1].Text = valueString;
            }
        }
    }
}
