namespace NeoMem2.Gui
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.TvNotes = new System.Windows.Forms.TreeView();
            this.CmnuNotes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CmiNotesAddLinkedNote = new System.Windows.Forms.ToolStripMenuItem();
            this.MiLinkToCurrentNote = new System.Windows.Forms.ToolStripMenuItem();
            this.MiMakeRoot = new System.Windows.Forms.ToolStripMenuItem();
            this.CmiNotesOpenInNewWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.MiViewHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.SplitterLeftSidebar = new System.Windows.Forms.Splitter();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MiFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.MiFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MiFileBackup = new System.Windows.Forms.ToolStripMenuItem();
            this.MiFileImport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.MiShowNoteList = new System.Windows.Forms.ToolStripMenuItem();
            this.MiShowTabs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.MiRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.MiNoteFind = new System.Windows.Forms.ToolStripMenuItem();
            this.MiSearchCaseSensitive = new System.Windows.Forms.ToolStripMenuItem();
            this.MiHighlightMatches = new System.Windows.Forms.ToolStripMenuItem();
            this.MiSearchAllFields = new System.Windows.Forms.ToolStripMenuItem();
            this.MiSearchNoteText = new System.Windows.Forms.ToolStripMenuItem();
            this.MiTools = new System.Windows.Forms.ToolStripMenuItem();
            this.MiNoteNew = new System.Windows.Forms.ToolStripMenuItem();
            this.MiAddLinkedNote = new System.Windows.Forms.ToolStripMenuItem();
            this.MiNoteDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.MiBatchChanges = new System.Windows.Forms.ToolStripMenuItem();
            this.MiImportFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.MiCleanNotes = new System.Windows.Forms.ToolStripMenuItem();
            this.MiScriptEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.MiOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.MiWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.MiNewNoteWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MiWindowCascade = new System.Windows.Forms.ToolStripMenuItem();
            this.MiWindowTileHorizontal = new System.Windows.Forms.ToolStripMenuItem();
            this.MiWindowTileVertical = new System.Windows.Forms.ToolStripMenuItem();
            this.MiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.MiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.PanelTop = new System.Windows.Forms.Panel();
            this.BtnBrowseBack = new System.Windows.Forms.Button();
            this.BtnBrowseForward = new System.Windows.Forms.Button();
            this.CboSearchText = new System.Windows.Forms.ComboBox();
            this.CboMatcherType = new System.Windows.Forms.ComboBox();
            this.BtnClear = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.TslblAutoSaveEnabled = new System.Windows.Forms.ToolStripStatusLabel();
            this.LblNoteCounts = new System.Windows.Forms.ToolStripStatusLabel();
            this.TslblMemory = new System.Windows.Forms.ToolStripStatusLabel();
            this.TmrAutoSaver = new System.Windows.Forms.Timer(this.components);
            this.PanelLeftSidebar = new System.Windows.Forms.Panel();
            this.SplitterNotePinned = new System.Windows.Forms.Splitter();
            this.TvPinnedNotes = new System.Windows.Forms.TreeView();
            this.CboNoteModel = new System.Windows.Forms.ComboBox();
            this.SplitterCategoryNote = new System.Windows.Forms.Splitter();
            this.TvCategories = new System.Windows.Forms.TreeView();
            this.TxtCategoryFilter = new System.Windows.Forms.TextBox();
            this.CboTreeModel = new System.Windows.Forms.ComboBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.LvNotes = new System.Windows.Forms.ListView();
            this.ChName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SplitterTop = new System.Windows.Forms.Splitter();
            this.PanelRightSidebar = new System.Windows.Forms.Panel();
            this.SplitterRightSidebar = new System.Windows.Forms.Splitter();
            this.TcClients = new System.Windows.Forms.TabControl();
            this.CmnuNotes.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.PanelTop.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.PanelLeftSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // TvNotes
            // 
            this.TvNotes.AllowDrop = true;
            this.TvNotes.ContextMenuStrip = this.CmnuNotes;
            this.TvNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TvNotes.FullRowSelect = true;
            this.TvNotes.HideSelection = false;
            this.TvNotes.Location = new System.Drawing.Point(0, 162);
            this.TvNotes.Name = "TvNotes";
            this.TvNotes.Size = new System.Drawing.Size(200, 218);
            this.TvNotes.TabIndex = 2;
            this.TvNotes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TvNotesMouseDown);
            // 
            // CmnuNotes
            // 
            this.CmnuNotes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CmiNotesAddLinkedNote,
            this.MiLinkToCurrentNote,
            this.MiMakeRoot,
            this.CmiNotesOpenInNewWindow,
            this.MiViewHistory});
            this.CmnuNotes.Name = "CmnuNotes";
            this.CmnuNotes.Size = new System.Drawing.Size(191, 114);
            this.CmnuNotes.Opening += new System.ComponentModel.CancelEventHandler(this.CmnuNotesOpening);
            // 
            // CmiNotesAddLinkedNote
            // 
            this.CmiNotesAddLinkedNote.Name = "CmiNotesAddLinkedNote";
            this.CmiNotesAddLinkedNote.Size = new System.Drawing.Size(190, 22);
            this.CmiNotesAddLinkedNote.Text = "Add New Linked Note";
            // 
            // MiLinkToCurrentNote
            // 
            this.MiLinkToCurrentNote.Name = "MiLinkToCurrentNote";
            this.MiLinkToCurrentNote.Size = new System.Drawing.Size(190, 22);
            this.MiLinkToCurrentNote.Text = "Link to Current Note";
            this.MiLinkToCurrentNote.Click += new System.EventHandler(this.MiLinkToCurrentNoteClick);
            // 
            // MiMakeRoot
            // 
            this.MiMakeRoot.Name = "MiMakeRoot";
            this.MiMakeRoot.Size = new System.Drawing.Size(190, 22);
            this.MiMakeRoot.Text = "Make Root";
            this.MiMakeRoot.Click += new System.EventHandler(this.MiMakeRootClick);
            // 
            // CmiNotesOpenInNewWindow
            // 
            this.CmiNotesOpenInNewWindow.Name = "CmiNotesOpenInNewWindow";
            this.CmiNotesOpenInNewWindow.Size = new System.Drawing.Size(190, 22);
            this.CmiNotesOpenInNewWindow.Text = "Open In New Window";
            this.CmiNotesOpenInNewWindow.Click += new System.EventHandler(this.CmiNotesOpenInNewWindowClick);
            // 
            // MiViewHistory
            // 
            this.MiViewHistory.Name = "MiViewHistory";
            this.MiViewHistory.Size = new System.Drawing.Size(190, 22);
            this.MiViewHistory.Text = "View History...";
            this.MiViewHistory.Click += new System.EventHandler(this.MiViewHistoryClick);
            // 
            // SplitterLeftSidebar
            // 
            this.SplitterLeftSidebar.Location = new System.Drawing.Point(200, 52);
            this.SplitterLeftSidebar.Name = "SplitterLeftSidebar";
            this.SplitterLeftSidebar.Size = new System.Drawing.Size(3, 480);
            this.SplitterLeftSidebar.TabIndex = 1;
            this.SplitterLeftSidebar.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiFile,
            this.toolStripMenuItem3,
            this.toolStripMenuItem1,
            this.MiTools,
            this.MiWindow,
            this.MiHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1061, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MiFile
            // 
            this.MiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiFileNew,
            this.MiFileOpen,
            this.toolStripSeparator1,
            this.MiFileBackup,
            this.MiFileImport,
            this.toolStripSeparator2,
            this.MiExit});
            this.MiFile.Name = "MiFile";
            this.MiFile.Size = new System.Drawing.Size(37, 20);
            this.MiFile.Text = "File";
            // 
            // MiFileNew
            // 
            this.MiFileNew.Name = "MiFileNew";
            this.MiFileNew.Size = new System.Drawing.Size(180, 22);
            this.MiFileNew.Text = "New";
            // 
            // MiFileOpen
            // 
            this.MiFileOpen.Name = "MiFileOpen";
            this.MiFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.MiFileOpen.Size = new System.Drawing.Size(180, 22);
            this.MiFileOpen.Text = "Open...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // MiFileBackup
            // 
            this.MiFileBackup.Name = "MiFileBackup";
            this.MiFileBackup.Size = new System.Drawing.Size(180, 22);
            this.MiFileBackup.Text = "Backup...";
            // 
            // MiFileImport
            // 
            this.MiFileImport.Name = "MiFileImport";
            this.MiFileImport.Size = new System.Drawing.Size(180, 22);
            this.MiFileImport.Text = "Import/Export...";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // MiExit
            // 
            this.MiExit.Name = "MiExit";
            this.MiExit.Size = new System.Drawing.Size(180, 22);
            this.MiExit.Text = "Exit";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiShowNoteList,
            this.MiShowTabs,
            this.toolStripSeparator5,
            this.MiRefresh});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(44, 20);
            this.toolStripMenuItem3.Text = "View";
            // 
            // MiShowNoteList
            // 
            this.MiShowNoteList.CheckOnClick = true;
            this.MiShowNoteList.Name = "MiShowNoteList";
            this.MiShowNoteList.Size = new System.Drawing.Size(153, 22);
            this.MiShowNoteList.Text = "Show Note List";
            // 
            // MiShowTabs
            // 
            this.MiShowTabs.Checked = true;
            this.MiShowTabs.CheckOnClick = true;
            this.MiShowTabs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MiShowTabs.Name = "MiShowTabs";
            this.MiShowTabs.Size = new System.Drawing.Size(153, 22);
            this.MiShowTabs.Text = "Show Tabs";
            this.MiShowTabs.Click += new System.EventHandler(this.MiShowTabsClick);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(150, 6);
            // 
            // MiRefresh
            // 
            this.MiRefresh.Name = "MiRefresh";
            this.MiRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.MiRefresh.Size = new System.Drawing.Size(153, 22);
            this.MiRefresh.Text = "Refresh";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiNoteFind,
            this.MiSearchCaseSensitive,
            this.MiHighlightMatches,
            this.MiSearchAllFields,
            this.MiSearchNoteText});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(54, 20);
            this.toolStripMenuItem1.Text = "Search";
            // 
            // MiNoteFind
            // 
            this.MiNoteFind.Name = "MiNoteFind";
            this.MiNoteFind.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.MiNoteFind.Size = new System.Drawing.Size(172, 22);
            this.MiNoteFind.Text = "Find";
            // 
            // MiSearchCaseSensitive
            // 
            this.MiSearchCaseSensitive.CheckOnClick = true;
            this.MiSearchCaseSensitive.Name = "MiSearchCaseSensitive";
            this.MiSearchCaseSensitive.Size = new System.Drawing.Size(172, 22);
            this.MiSearchCaseSensitive.Text = "Case Sensitive";
            // 
            // MiHighlightMatches
            // 
            this.MiHighlightMatches.CheckOnClick = true;
            this.MiHighlightMatches.Name = "MiHighlightMatches";
            this.MiHighlightMatches.Size = new System.Drawing.Size(172, 22);
            this.MiHighlightMatches.Text = "Highlight Matches";
            // 
            // MiSearchAllFields
            // 
            this.MiSearchAllFields.Checked = true;
            this.MiSearchAllFields.CheckOnClick = true;
            this.MiSearchAllFields.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MiSearchAllFields.Name = "MiSearchAllFields";
            this.MiSearchAllFields.Size = new System.Drawing.Size(172, 22);
            this.MiSearchAllFields.Text = "Search All Fields";
            // 
            // MiSearchNoteText
            // 
            this.MiSearchNoteText.CheckOnClick = true;
            this.MiSearchNoteText.Name = "MiSearchNoteText";
            this.MiSearchNoteText.Size = new System.Drawing.Size(172, 22);
            this.MiSearchNoteText.Text = "Search Note Text";
            // 
            // MiTools
            // 
            this.MiTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiNoteNew,
            this.MiAddLinkedNote,
            this.MiNoteDelete,
            this.toolStripSeparator4,
            this.MiBatchChanges,
            this.MiImportFiles,
            this.MiCleanNotes,
            this.MiScriptEditor,
            this.MiOptions,
            this.toolStripMenuItem2});
            this.MiTools.Name = "MiTools";
            this.MiTools.Size = new System.Drawing.Size(46, 20);
            this.MiTools.Text = "Tools";
            this.MiTools.DropDownOpening += new System.EventHandler(this.MiToolsDropDownOpening);
            // 
            // MiNoteNew
            // 
            this.MiNoteNew.Name = "MiNoteNew";
            this.MiNoteNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.MiNoteNew.Size = new System.Drawing.Size(203, 22);
            this.MiNoteNew.Text = "New Note";
            // 
            // MiAddLinkedNote
            // 
            this.MiAddLinkedNote.Name = "MiAddLinkedNote";
            this.MiAddLinkedNote.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.MiAddLinkedNote.Size = new System.Drawing.Size(203, 22);
            this.MiAddLinkedNote.Text = "Add Linked Note";
            this.MiAddLinkedNote.Click += new System.EventHandler(this.MiAddLinkedNoteClick);
            // 
            // MiNoteDelete
            // 
            this.MiNoteDelete.Name = "MiNoteDelete";
            this.MiNoteDelete.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.MiNoteDelete.Size = new System.Drawing.Size(203, 22);
            this.MiNoteDelete.Text = "Delete Note";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(200, 6);
            // 
            // MiBatchChanges
            // 
            this.MiBatchChanges.Name = "MiBatchChanges";
            this.MiBatchChanges.Size = new System.Drawing.Size(203, 22);
            this.MiBatchChanges.Text = "Batch Changes...";
            // 
            // MiImportFiles
            // 
            this.MiImportFiles.Name = "MiImportFiles";
            this.MiImportFiles.Size = new System.Drawing.Size(203, 22);
            this.MiImportFiles.Text = "Import Files...";
            this.MiImportFiles.Click += new System.EventHandler(this.MiImportFiles_Click);
            // 
            // MiCleanNotes
            // 
            this.MiCleanNotes.Name = "MiCleanNotes";
            this.MiCleanNotes.Size = new System.Drawing.Size(203, 22);
            this.MiCleanNotes.Text = "Clean Notes...";
            this.MiCleanNotes.Click += new System.EventHandler(this.MiCleanNotes_Click);
            // 
            // MiScriptEditor
            // 
            this.MiScriptEditor.Name = "MiScriptEditor";
            this.MiScriptEditor.Size = new System.Drawing.Size(203, 22);
            this.MiScriptEditor.Text = "Script Editor...";
            // 
            // MiOptions
            // 
            this.MiOptions.Name = "MiOptions";
            this.MiOptions.Size = new System.Drawing.Size(203, 22);
            this.MiOptions.Text = "Options...";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(200, 6);
            // 
            // MiWindow
            // 
            this.MiWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiNewNoteWindow,
            this.toolStripSeparator3,
            this.MiWindowCascade,
            this.MiWindowTileHorizontal,
            this.MiWindowTileVertical});
            this.MiWindow.Name = "MiWindow";
            this.MiWindow.Size = new System.Drawing.Size(63, 20);
            this.MiWindow.Text = "Window";
            this.MiWindow.DropDownOpening += new System.EventHandler(this.MiWindowDropDownOpening);
            // 
            // MiNewNoteWindow
            // 
            this.MiNewNoteWindow.Name = "MiNewNoteWindow";
            this.MiNewNoteWindow.Size = new System.Drawing.Size(183, 22);
            this.MiNewNoteWindow.Text = "New Note Window...";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(180, 6);
            // 
            // MiWindowCascade
            // 
            this.MiWindowCascade.Name = "MiWindowCascade";
            this.MiWindowCascade.Size = new System.Drawing.Size(183, 22);
            this.MiWindowCascade.Text = "Cascade";
            // 
            // MiWindowTileHorizontal
            // 
            this.MiWindowTileHorizontal.Name = "MiWindowTileHorizontal";
            this.MiWindowTileHorizontal.Size = new System.Drawing.Size(183, 22);
            this.MiWindowTileHorizontal.Text = "Tile Horizontal";
            // 
            // MiWindowTileVertical
            // 
            this.MiWindowTileVertical.Name = "MiWindowTileVertical";
            this.MiWindowTileVertical.Size = new System.Drawing.Size(183, 22);
            this.MiWindowTileVertical.Text = "Tile Vertically";
            // 
            // MiHelp
            // 
            this.MiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiAbout});
            this.MiHelp.Name = "MiHelp";
            this.MiHelp.Size = new System.Drawing.Size(44, 20);
            this.MiHelp.Text = "Help";
            // 
            // MiAbout
            // 
            this.MiAbout.Name = "MiAbout";
            this.MiAbout.Size = new System.Drawing.Size(116, 22);
            this.MiAbout.Text = "About...";
            // 
            // PanelTop
            // 
            this.PanelTop.Controls.Add(this.BtnBrowseBack);
            this.PanelTop.Controls.Add(this.BtnBrowseForward);
            this.PanelTop.Controls.Add(this.CboSearchText);
            this.PanelTop.Controls.Add(this.CboMatcherType);
            this.PanelTop.Controls.Add(this.BtnClear);
            this.PanelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelTop.Location = new System.Drawing.Point(0, 24);
            this.PanelTop.Name = "PanelTop";
            this.PanelTop.Size = new System.Drawing.Size(1061, 28);
            this.PanelTop.TabIndex = 4;
            // 
            // BtnBrowseBack
            // 
            this.BtnBrowseBack.Location = new System.Drawing.Point(12, -1);
            this.BtnBrowseBack.Name = "BtnBrowseBack";
            this.BtnBrowseBack.Size = new System.Drawing.Size(31, 23);
            this.BtnBrowseBack.TabIndex = 7;
            this.BtnBrowseBack.Text = "<<";
            this.BtnBrowseBack.UseVisualStyleBackColor = true;
            this.BtnBrowseBack.Click += new System.EventHandler(this.BtnBrowseBack_Click);
            // 
            // BtnBrowseForward
            // 
            this.BtnBrowseForward.Location = new System.Drawing.Point(49, -1);
            this.BtnBrowseForward.Name = "BtnBrowseForward";
            this.BtnBrowseForward.Size = new System.Drawing.Size(31, 23);
            this.BtnBrowseForward.TabIndex = 6;
            this.BtnBrowseForward.Text = ">>";
            this.BtnBrowseForward.UseVisualStyleBackColor = true;
            this.BtnBrowseForward.Click += new System.EventHandler(this.BtnBrowseForward_Click);
            // 
            // CboSearchText
            // 
            this.CboSearchText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CboSearchText.FormattingEnabled = true;
            this.CboSearchText.Location = new System.Drawing.Point(220, 1);
            this.CboSearchText.Name = "CboSearchText";
            this.CboSearchText.Size = new System.Drawing.Size(748, 21);
            this.CboSearchText.TabIndex = 5;
            this.CboSearchText.TextChanged += new System.EventHandler(this.CboSearchTextTextChanged);
            this.CboSearchText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CboSearchTextKeyUp);
            // 
            // CboMatcherType
            // 
            this.CboMatcherType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboMatcherType.FormattingEnabled = true;
            this.CboMatcherType.Location = new System.Drawing.Point(86, 1);
            this.CboMatcherType.Name = "CboMatcherType";
            this.CboMatcherType.Size = new System.Drawing.Size(128, 21);
            this.CboMatcherType.TabIndex = 0;
            // 
            // BtnClear
            // 
            this.BtnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClear.Location = new System.Drawing.Point(974, -1);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(75, 23);
            this.BtnClear.TabIndex = 3;
            this.BtnClear.Text = "Clear";
            this.BtnClear.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TslblAutoSaveEnabled,
            this.LblNoteCounts,
            this.TslblMemory});
            this.statusStrip1.Location = new System.Drawing.Point(0, 532);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1061, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // TslblAutoSaveEnabled
            // 
            this.TslblAutoSaveEnabled.Name = "TslblAutoSaveEnabled";
            this.TslblAutoSaveEnabled.Size = new System.Drawing.Size(105, 17);
            this.TslblAutoSaveEnabled.Text = "AutoSave: Enabled";
            this.TslblAutoSaveEnabled.Click += new System.EventHandler(this.TslblAutoSaveEnabledClick);
            // 
            // LblNoteCounts
            // 
            this.LblNoteCounts.Name = "LblNoteCounts";
            this.LblNoteCounts.Size = new System.Drawing.Size(114, 17);
            this.LblNoteCounts.Text = "Displaying 0/0 notes";
            // 
            // TslblMemory
            // 
            this.TslblMemory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TslblMemory.Name = "TslblMemory";
            this.TslblMemory.Size = new System.Drawing.Size(827, 17);
            this.TslblMemory.Spring = true;
            this.TslblMemory.Text = "Memory Usage";
            this.TslblMemory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TslblMemory.Click += new System.EventHandler(this.TslblMemoryClick);
            // 
            // TmrAutoSaver
            // 
            this.TmrAutoSaver.Interval = 1000;
            this.TmrAutoSaver.Tick += new System.EventHandler(this.TmrAutoSaverTick);
            // 
            // PanelLeftSidebar
            // 
            this.PanelLeftSidebar.Controls.Add(this.TvNotes);
            this.PanelLeftSidebar.Controls.Add(this.SplitterNotePinned);
            this.PanelLeftSidebar.Controls.Add(this.TvPinnedNotes);
            this.PanelLeftSidebar.Controls.Add(this.CboNoteModel);
            this.PanelLeftSidebar.Controls.Add(this.SplitterCategoryNote);
            this.PanelLeftSidebar.Controls.Add(this.TvCategories);
            this.PanelLeftSidebar.Controls.Add(this.TxtCategoryFilter);
            this.PanelLeftSidebar.Controls.Add(this.CboTreeModel);
            this.PanelLeftSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelLeftSidebar.Location = new System.Drawing.Point(0, 52);
            this.PanelLeftSidebar.Name = "PanelLeftSidebar";
            this.PanelLeftSidebar.Size = new System.Drawing.Size(200, 480);
            this.PanelLeftSidebar.TabIndex = 8;
            // 
            // SplitterNotePinned
            // 
            this.SplitterNotePinned.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SplitterNotePinned.Location = new System.Drawing.Point(0, 380);
            this.SplitterNotePinned.Name = "SplitterNotePinned";
            this.SplitterNotePinned.Size = new System.Drawing.Size(200, 3);
            this.SplitterNotePinned.TabIndex = 14;
            this.SplitterNotePinned.TabStop = false;
            // 
            // TvPinnedNotes
            // 
            this.TvPinnedNotes.AllowDrop = true;
            this.TvPinnedNotes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TvPinnedNotes.Location = new System.Drawing.Point(0, 383);
            this.TvPinnedNotes.Name = "TvPinnedNotes";
            this.TvPinnedNotes.Size = new System.Drawing.Size(200, 97);
            this.TvPinnedNotes.TabIndex = 13;
            // 
            // CboNoteModel
            // 
            this.CboNoteModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.CboNoteModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboNoteModel.FormattingEnabled = true;
            this.CboNoteModel.Location = new System.Drawing.Point(0, 141);
            this.CboNoteModel.Name = "CboNoteModel";
            this.CboNoteModel.Size = new System.Drawing.Size(200, 21);
            this.CboNoteModel.TabIndex = 15;
            this.CboNoteModel.SelectedIndexChanged += new System.EventHandler(this.CboNoteModelSelectedIndexChanged);
            // 
            // SplitterCategoryNote
            // 
            this.SplitterCategoryNote.Dock = System.Windows.Forms.DockStyle.Top;
            this.SplitterCategoryNote.Location = new System.Drawing.Point(0, 138);
            this.SplitterCategoryNote.Name = "SplitterCategoryNote";
            this.SplitterCategoryNote.Size = new System.Drawing.Size(200, 3);
            this.SplitterCategoryNote.TabIndex = 11;
            this.SplitterCategoryNote.TabStop = false;
            // 
            // TvCategories
            // 
            this.TvCategories.CheckBoxes = true;
            this.TvCategories.Dock = System.Windows.Forms.DockStyle.Top;
            this.TvCategories.HideSelection = false;
            this.TvCategories.Location = new System.Drawing.Point(0, 41);
            this.TvCategories.Name = "TvCategories";
            this.TvCategories.Size = new System.Drawing.Size(200, 97);
            this.TvCategories.TabIndex = 1;
            // 
            // TxtCategoryFilter
            // 
            this.TxtCategoryFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtCategoryFilter.Location = new System.Drawing.Point(0, 21);
            this.TxtCategoryFilter.Name = "TxtCategoryFilter";
            this.TxtCategoryFilter.Size = new System.Drawing.Size(200, 20);
            this.TxtCategoryFilter.TabIndex = 12;
            this.TxtCategoryFilter.TextChanged += new System.EventHandler(this.TxtCategoryFilterTextChanged);
            // 
            // CboTreeModel
            // 
            this.CboTreeModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.CboTreeModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboTreeModel.FormattingEnabled = true;
            this.CboTreeModel.Location = new System.Drawing.Point(0, 0);
            this.CboTreeModel.Name = "CboTreeModel";
            this.CboTreeModel.Size = new System.Drawing.Size(200, 21);
            this.CboTreeModel.TabIndex = 0;
            this.CboTreeModel.SelectedIndexChanged += new System.EventHandler(this.CboTreeModelSelectedIndexChanged);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // LvNotes
            // 
            this.LvNotes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ChName});
            this.LvNotes.Dock = System.Windows.Forms.DockStyle.Top;
            this.LvNotes.FullRowSelect = true;
            this.LvNotes.HideSelection = false;
            this.LvNotes.Location = new System.Drawing.Point(203, 52);
            this.LvNotes.Name = "LvNotes";
            this.LvNotes.Size = new System.Drawing.Size(655, 97);
            this.LvNotes.TabIndex = 10;
            this.LvNotes.UseCompatibleStateImageBehavior = false;
            this.LvNotes.View = System.Windows.Forms.View.Details;
            this.LvNotes.VirtualMode = true;
            this.LvNotes.Visible = false;
            // 
            // ChName
            // 
            this.ChName.Text = "Name";
            // 
            // SplitterTop
            // 
            this.SplitterTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.SplitterTop.Location = new System.Drawing.Point(203, 149);
            this.SplitterTop.Name = "SplitterTop";
            this.SplitterTop.Size = new System.Drawing.Size(655, 3);
            this.SplitterTop.TabIndex = 11;
            this.SplitterTop.TabStop = false;
            // 
            // PanelRightSidebar
            // 
            this.PanelRightSidebar.Dock = System.Windows.Forms.DockStyle.Right;
            this.PanelRightSidebar.Location = new System.Drawing.Point(861, 52);
            this.PanelRightSidebar.Name = "PanelRightSidebar";
            this.PanelRightSidebar.Size = new System.Drawing.Size(200, 480);
            this.PanelRightSidebar.TabIndex = 13;
            this.PanelRightSidebar.Visible = false;
            // 
            // SplitterRightSidebar
            // 
            this.SplitterRightSidebar.Dock = System.Windows.Forms.DockStyle.Right;
            this.SplitterRightSidebar.Location = new System.Drawing.Point(858, 52);
            this.SplitterRightSidebar.Name = "SplitterRightSidebar";
            this.SplitterRightSidebar.Size = new System.Drawing.Size(3, 480);
            this.SplitterRightSidebar.TabIndex = 14;
            this.SplitterRightSidebar.TabStop = false;
            this.SplitterRightSidebar.Visible = false;
            // 
            // TcClients
            // 
            this.TcClients.Dock = System.Windows.Forms.DockStyle.Top;
            this.TcClients.Location = new System.Drawing.Point(203, 152);
            this.TcClients.Name = "TcClients";
            this.TcClients.SelectedIndex = 0;
            this.TcClients.Size = new System.Drawing.Size(655, 24);
            this.TcClients.TabIndex = 16;
            this.TcClients.SelectedIndexChanged += new System.EventHandler(this.TcClients_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 554);
            this.Controls.Add(this.TcClients);
            this.Controls.Add(this.SplitterTop);
            this.Controls.Add(this.LvNotes);
            this.Controls.Add(this.SplitterRightSidebar);
            this.Controls.Add(this.SplitterLeftSidebar);
            this.Controls.Add(this.PanelLeftSidebar);
            this.Controls.Add(this.PanelRightSidebar);
            this.Controls.Add(this.PanelTop);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "NeoMem2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.MdiChildActivate += new System.EventHandler(this.MainFormMdiChildActivate);
            this.CmnuNotes.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.PanelTop.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.PanelLeftSidebar.ResumeLayout(false);
            this.PanelLeftSidebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Splitter SplitterLeftSidebar;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MiFile;
        private System.Windows.Forms.ToolStripMenuItem MiFileNew;
        private System.Windows.Forms.ToolStripMenuItem MiFileOpen;
        private System.Windows.Forms.ToolStripMenuItem MiFileImport;
        private System.Windows.Forms.ToolStripMenuItem MiExit;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel PanelTop;
        private System.Windows.Forms.Button BtnClear;
        private System.Windows.Forms.ToolStripMenuItem MiWindow;
        private System.Windows.Forms.ToolStripMenuItem MiNewNoteWindow;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel LblNoteCounts;
        private System.Windows.Forms.ToolStripMenuItem MiWindowTileHorizontal;
        private System.Windows.Forms.ToolStripMenuItem MiWindowTileVertical;
        private System.Windows.Forms.ToolStripMenuItem MiWindowCascade;
        private System.Windows.Forms.Timer TmrAutoSaver;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Panel PanelLeftSidebar;
        private System.Windows.Forms.ComboBox CboTreeModel;
        private System.Windows.Forms.Splitter SplitterCategoryNote;
        private System.Windows.Forms.ComboBox CboMatcherType;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ContextMenuStrip CmnuNotes;
        private System.Windows.Forms.ToolStripMenuItem CmiNotesOpenInNewWindow;
        private System.Windows.Forms.ToolStripMenuItem MiFileBackup;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem MiSearchCaseSensitive;
        private System.Windows.Forms.ToolStripMenuItem MiSearchNoteText;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem MiTools;
        private System.Windows.Forms.ToolStripMenuItem MiScriptEditor;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem MiSearchAllFields;
        private System.Windows.Forms.ToolStripMenuItem MiHelp;
        private System.Windows.Forms.ToolStripMenuItem MiAbout;
        private System.Windows.Forms.Splitter SplitterTop;
        private System.Windows.Forms.ColumnHeader ChName;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem MiNoteFind;
        private System.Windows.Forms.ToolStripMenuItem MiRefresh;
        private System.Windows.Forms.ToolStripMenuItem MiNoteNew;
        private System.Windows.Forms.ToolStripMenuItem MiNoteDelete;
        private System.Windows.Forms.ToolStripMenuItem MiOptions;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        public System.Windows.Forms.ComboBox CboSearchText;
        private System.Windows.Forms.ToolStripMenuItem MiBatchChanges;
        public System.Windows.Forms.ListView LvNotes;
        public System.Windows.Forms.ToolStripMenuItem MiShowNoteList;
        private System.Windows.Forms.ToolStripMenuItem MiHighlightMatches;
        private System.Windows.Forms.TextBox TxtCategoryFilter;
        private System.Windows.Forms.Splitter SplitterNotePinned;
        public System.Windows.Forms.TreeView TvPinnedNotes;
        public System.Windows.Forms.TreeView TvNotes;
        public System.Windows.Forms.TreeView TvCategories;
        private System.Windows.Forms.ComboBox CboNoteModel;
        private System.Windows.Forms.ToolStripMenuItem CmiNotesAddLinkedNote;
        private System.Windows.Forms.ToolStripMenuItem MiAddLinkedNote;
        private System.Windows.Forms.ToolStripMenuItem MiViewHistory;
        private System.Windows.Forms.ToolStripStatusLabel TslblMemory;
        private System.Windows.Forms.ToolStripStatusLabel TslblAutoSaveEnabled;
        private System.Windows.Forms.ToolStripMenuItem MiMakeRoot;
        private System.Windows.Forms.ToolStripMenuItem MiImportFiles;
        private System.Windows.Forms.Splitter SplitterRightSidebar;
        private System.Windows.Forms.Panel PanelRightSidebar;
        private System.Windows.Forms.Button BtnBrowseBack;
        private System.Windows.Forms.Button BtnBrowseForward;
        private System.Windows.Forms.ToolStripMenuItem MiCleanNotes;
        private System.Windows.Forms.TabControl TcClients;
        private System.Windows.Forms.ToolStripMenuItem MiShowTabs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem MiLinkToCurrentNote;
    }
}

