using System;

using NeoMem2.Core.Stores;
using NeoMem2.Utils;

namespace NeoMem2.Gui.Commands
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler<ItemEventArgs<Exception>>  ExceptionOccurred = delegate { };


        private readonly MainForm m_Form;


        protected CommandBase(MainForm form)
        {
            m_Form = form;
        }


        public MainForm Form { get { return m_Form; } }


        public void Execute(EventArgs e)
        {
            ExceptionSafeBlock(() => DoExecute(e));
        }

        protected abstract void DoExecute(EventArgs e);

        private void ExceptionSafeBlock(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                OnExceptionOccurred(new ItemEventArgs<Exception>(ex));
            }
        }

        protected virtual void OnExceptionOccurred(ItemEventArgs<Exception> e)
        {
            ExceptionOccurred(this, e);
        }
        
        protected void OpenStore(string storeType, string connectionString)
        {
            Properties.Settings.Default.MainConnectionString = connectionString;
            Properties.Settings.Default.StoreType = storeType.ToString();
            Properties.Settings.Default.Save();

            Form.Model.Open(storeType, connectionString);
        }
    }
}