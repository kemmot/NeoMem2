using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using NeoMem2.Core;
using NeoMem2.Core.Scripting;
using NeoMem2.Gui.Models;
using NeoMem2.Utils.Text;

using Fireball.Windows.Forms;

namespace NeoMem2.Gui
{
    public partial class ScriptEditorForm : Form
    {
        private Script m_CurrentScript;
        private CodeEditorControl m_Editor;


        public ScriptEditorForm()
        {
            InitializeComponent();

            m_Editor = new CodeEditorControl
            {
                Dock = DockStyle.Fill,
                HighLightActiveLine = true,
                HighLightedLineColor = Color.Yellow
            };
            //CodeEditorSyntaxLoader.SetSyntax(m_Editor, SyntaxLanguage.Text);
            Controls.Add(m_Editor);
            m_Editor.BringToFront();
        }


        public Model Model { get; set; }


        private void ScriptEditorFormLoad(object sender, EventArgs e)
        {
            foreach (var script in Model.ScriptHost.GetAvailableScripts())
            {
                TvScripts.Nodes.Add(new ScriptTreeNode(script));
            }
        }

        private void TvScriptsAfterSelect(object sender, TreeViewEventArgs e)
        {
            var node = e.Node as ScriptTreeNode;
            if (node != null)
            {
                SetScript(node.Script);
            }
        }

        private void SetScript(Script script)
        {
            if (m_CurrentScript != null)
            {
                SaveFile();
            }

            m_CurrentScript = script;
            LoadFile();
        }

        private void LoadFile()
        {
            m_Editor.Open(m_CurrentScript.Filename);
        }

        private void SaveFile()
        {
            m_Editor.Save(m_CurrentScript.Filename);
        }

        private void MiScriptRunClick(object sender, EventArgs e)
        {
            try
            {
                var output = new StringBuilder();
                output.AppendLine("{0} Starting Script", DateTime.Now);
                
                ScriptResult result;
                if (m_CurrentScript != null)
                {
                    SaveFile();

                    var mainForm = new MainForm();
                    var note = new Note();
                    var noteForm = new NoteForm();

                    var variables = new Dictionary<string, object>();
                    variables[ScriptVariableNames.CurrentNoteForm] = noteForm;
                    variables[ScriptVariableNames.MainForm] = mainForm;
                    variables[ScriptVariableNames.Note] = note;
                    switch (m_CurrentScript.ScriptType)
                    {
                        case ScriptType.MainFormLoad:
                            result = Model.ScriptHost.ExecuteScript(ScriptType.MainFormLoad, variables);
                            mainForm.Show();
                            break;
                        case ScriptType.NewNoteCreated:
                            result = Model.ScriptHost.ExecuteScript(ScriptType.NewNoteCreated, variables);
                            output.AppendLine(variables[ScriptVariableNames.Note].ToString());
                            break;
                        case ScriptType.NoteFormLoad:
                            result = Model.ScriptHost.ExecuteScript(ScriptType.NoteFormLoad, variables);
                            noteForm.Show();
                            break;
                        default:
                            var arguments = new ScriptArguments { ScriptText = m_Editor.Document.Text };
                            result = Model.ScriptHost.Execute(arguments, variables);
                            break;
                    }
                }
                else
                {
                    var arguments = new ScriptArguments { ScriptText = m_Editor.Document.Text };
                    result = Model.ScriptHost.Execute(arguments);
                }

                output.AppendLine(result.Output);
                output.AppendLine("Done");
                RtxtOutput.Text += output.ToString();
            }
            catch (Exception ex)
            {
                RtxtOutput.Text = ex.ToString();
            }
        }
    }

    public class ScriptTreeNode : TreeNode
    {
        private readonly Script m_Script;

        public ScriptTreeNode(Script script)
        {
            m_Script = script;
            Text = string.Format(
                "{0} ({1})",
                Path.GetFileNameWithoutExtension(m_Script.Filename),
                m_Script.ScriptType);
        }

        public Script Script { get { return m_Script; } }
    }
}
