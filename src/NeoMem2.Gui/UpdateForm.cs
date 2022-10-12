using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

using NeoMem2.Automation.Updates;
using NeoMem2.Utils;

namespace NeoMem2.Gui
{
    public partial class UpdateForm : Form
    {
        private readonly Dictionary<StepInfo, StepListViewItem> m_StepItems = new Dictionary<StepInfo, StepListViewItem>();
        private List<Tuple<ComponentInfo, UpdateContext>> m_ComponentUpdates;


        public UpdateForm()
        {
            InitializeComponent();
        }


        public void SetUpdates(List<Tuple<ComponentInfo, UpdateContext>> componentUpdates)
        {
            m_ComponentUpdates = componentUpdates;

            foreach (var component in componentUpdates)
            {
                foreach (var version in component.Item1.Versions)
                {
                    foreach (var step in version.Steps)
                    {
                        var item = new StepListViewItem(component.Item1.Name, version.Version, step);
                        m_StepItems[step] = item;
                        LvUpdates.Items.Add(item);
                    }
                }
            }

            ChUpdateComponent.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            ChUpdateDescription.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void BtnStartClick(object sender, EventArgs e)
        {
            BtnClose.Enabled = false;
            BtnStart.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
        }

        private void ManagerStepComplete(object sender, ItemCompleteEventArgs<StepInfo> e)
        {
            Invoke((Action)(() => m_StepItems[e.Item].SetComplete(e.Exception)));
        }

        private void ManagerStepStarted(object sender, ItemEventArgs<StepInfo> e)
        {
            Invoke((Action)(() => m_StepItems[e.Item].SetStarted()));
        }

        private void LvUpdatesDoubleClick(object sender, EventArgs e)
        {
            if (LvUpdates.SelectedItems.Count > 0)
            {
                var item = (StepListViewItem)LvUpdates.SelectedItems[0];
                if (item.Exception != null)
                {
                    MessageBox.Show(item.Exception.ToString(), "Step Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnCloseClick(object sender, EventArgs e)
        {
            Close();
        }

        private void BackgroundWorker1DoWork(object sender, DoWorkEventArgs e)
        {
            if (m_ComponentUpdates != null)
            {
                foreach (var componentUpdate in m_ComponentUpdates)
                {
                    var manager = new ManualUpdateManager(componentUpdate.Item2);
                    manager.StepStarted += ManagerStepStarted;
                    manager.StepComplete += ManagerStepComplete;
                    manager.Update(componentUpdate.Item1);
                }
            }
        }

        private void BackgroundWorker1RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BtnClose.Enabled = true;
        }


        public class StepListViewItem : ListViewItem
        {
            public StepListViewItem(string component, double version, StepInfo step)
            {
                Step = step;

                Text = component;
                SubItems.Add(version.ToString(CultureInfo.InvariantCulture));
                SubItems.Add(step.StepIndex.ToString(CultureInfo.InvariantCulture));
                SubItems.Add(step.Description);
                SubItems.Add("Waiting");
            }

            public Exception Exception { get; private set; }
            public StepInfo Step { get; private set; }

            public void SetStarted()
            {
                SetStatus("In Progress");
            }

            public void SetComplete(Exception exception = null)
            {
                if (exception == null)
                {
                    SetStatus("Complete");
                }
                else
                {
                    Exception = exception;
                    SetStatus("Failed");
                }
            }

            private void SetStatus(string status)
            {
                SubItems[4].Text = status;
            }
        }
    }
}
