using System;
using System.Windows.Forms;

using NeoMem2.Core.Stores;
using NeoMem2.Gui.Properties;

namespace NeoMem2.Gui.Commands
{
    public class FileNewCommand : CommandBase
    {
        public FileNewCommand(MainForm form)
            : base(form)
        {
        }


        protected override void DoExecute(EventArgs e)
        {
            using (OpenForm openForm = new OpenForm())
            {
                openForm.StoreFactory = StoreFactory.Instance;
                openForm.ConnectionString = Settings.Default.MainConnectionString;
                openForm.StoreType = Settings.ReadStoreTypeFromSettings();

                if (openForm.ShowDialog() == DialogResult.OK)
                {
                    string connectionString = openForm.ConnectionString;
                    string storeType = openForm.StoreType;
                    Form.Model.NewStore(storeType, connectionString);
                    OpenStore(storeType, connectionString);
                }
            }
        }
    }
}
