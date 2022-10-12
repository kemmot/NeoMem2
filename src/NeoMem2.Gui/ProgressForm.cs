using System.Windows.Forms;

namespace NeoMem2.Gui
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
        }

        public string Title { get { return Text; } set { Text = value; } }
        public string Status { get { return TxtStatus.Text; } set { TxtStatus.Text = value; } }
        public int PercentageComplete { get { return progressBar1.Value; } set { progressBar1.Value = value; } }
    }
}
