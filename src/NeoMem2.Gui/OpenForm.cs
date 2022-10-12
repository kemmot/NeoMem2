using System;
using System.Windows.Forms;

using NeoMem2.Core.Stores;

namespace NeoMem2.Gui
{
    public partial class OpenForm : Form
    {
        public OpenForm()
        {
            InitializeComponent();
        }


        public string ConnectionString
        {
            get { return CboConnectionString.Text; }
            set
            {
                if (!string.IsNullOrEmpty(CboConnectionString.Text)
                    && !CboConnectionString.Items.Contains(CboConnectionString.Items))
                {
                    CboConnectionString.Items.Add(CboConnectionString.Text);
                }

                CboConnectionString.Text = value;
            }
        }

        public StoreFactory StoreFactory { get; set; }

        public string StoreType
        {
            get { return (string)CboStoreType.SelectedItem; }
            set { CboStoreType.SelectedItem = value; }
        }


        private void OpenFormLoad(object sender, EventArgs e)
        {
            foreach (var sourceType in StoreFactory.GetStoreTypes())
            {
                CboStoreType.Items.Add(sourceType);
            }

            CboStoreType.SelectedIndex = 0;
        }

        private void CboStoreTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ConnectionString = StoreFactory.GetDefaultConnectionString(StoreType);
            }
            catch (NotSupportedException)
            {
                // do nothing
            }
        }
    }
}
