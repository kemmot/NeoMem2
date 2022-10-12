using System;
using System.Windows.Forms;

namespace NeoMem2.Gui
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
        }

        public void AddTarget(object target, string name)
        {
            listBox1.Items.Add(new Wrapper { Target = target, Text = name });
        }

        private void OptionsFormLoad(object sender, EventArgs e)
        {

        }

        private void ListBox1SelectedIndexChanged(object sender, EventArgs e)
        {
            PgProperties.SelectedObject = ((Wrapper)listBox1.SelectedItem).Target;
        }


        class Wrapper
        {
            public object Target { get; set; }
            public string Text { get; set; }

            public override string ToString()
            {
                return string.IsNullOrEmpty(Text) ? Target.ToString() : Text;
            }
        }
    }
}
