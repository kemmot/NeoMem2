using System;
using System.IO;
using System.Windows.Forms;

using NeoMem2.Core.Scripting;

namespace NeoMem2.Gui
{
    public class ScriptMenuItem : ToolStripMenuItem
    {
        private readonly Script m_Script;
        private readonly IScriptHost m_ScriptHost;

        public ScriptMenuItem(IScriptHost scriptHost, Script script)
        {
            m_ScriptHost = scriptHost;
            m_Script = script;

            Text = Path.GetFileNameWithoutExtension(m_Script.Filename);
        }

        public Script Script { get { return m_Script; } }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            try
            {
                m_ScriptHost.ExecuteScript(Script);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void HandleException(Exception ex)
        {
            MessageBox.Show(ex.ToString(), ex.GetType().FullName);
        }
    }
}
