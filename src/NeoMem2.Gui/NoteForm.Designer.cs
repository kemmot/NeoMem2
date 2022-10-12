namespace NeoMem2.Gui
{
    partial class NoteForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NoteForm));
            this.TcMain = new System.Windows.Forms.TabControl();
            this.TpText = new System.Windows.Forms.TabPage();
            this.TsMarkdown = new System.Windows.Forms.ToolStrip();
            this.TsbtnMarkdownSwitch = new System.Windows.Forms.ToolStripButton();
            this.TsbtnShowHtml = new System.Windows.Forms.ToolStripButton();
            this.TsRichText = new System.Windows.Forms.ToolStrip();
            this.TsbtnBold = new System.Windows.Forms.ToolStripButton();
            this.TsbtnItalic = new System.Windows.Forms.ToolStripButton();
            this.TsbtnUnderline = new System.Windows.Forms.ToolStripButton();
            this.TsbtnRemoveFormatting = new System.Windows.Forms.ToolStripButton();
            this.CboTextFormat = new System.Windows.Forms.ComboBox();
            this.TpProperties = new System.Windows.Forms.TabPage();
            this.PgProperties = new System.Windows.Forms.PropertyGrid();
            this.BtnNewProperty = new System.Windows.Forms.Button();
            this.BtnDeleteProperty = new System.Windows.Forms.Button();
            this.TpLinks = new System.Windows.Forms.TabPage();
            this.LvLinks = new System.Windows.Forms.ListView();
            this.ChLinkType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ChLinkName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.CboLinkNoteFilterField = new System.Windows.Forms.ComboBox();
            this.CboLinkNoteFilterText = new System.Windows.Forms.ComboBox();
            this.LblLinkNoteFilter = new System.Windows.Forms.Label();
            this.TxtName = new System.Windows.Forms.TextBox();
            this.CmnuEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MiPastePlain = new System.Windows.Forms.ToolStripMenuItem();
            this.MiUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.SsStatus = new System.Windows.Forms.StatusStrip();
            this.LblNoteState = new System.Windows.Forms.ToolStripStatusLabel();
            this.LblPinned = new System.Windows.Forms.ToolStripStatusLabel();
            this.LblExternalEditor = new System.Windows.Forms.ToolStripStatusLabel();
            this.LblIsTemplate = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsbtnApplyTemplate = new System.Windows.Forms.ToolStripDropDownButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.MiCopyNoteLink = new System.Windows.Forms.ToolStripMenuItem();
            this.MiView = new System.Windows.Forms.ToolStripMenuItem();
            this.MiNotesNavigateForwards = new System.Windows.Forms.ToolStripMenuItem();
            this.MiNotesNavigateBackwards = new System.Windows.Forms.ToolStripMenuItem();
            this.MiLinksLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.MiLinksLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.MiLinksRight = new System.Windows.Forms.ToolStripMenuItem();
            this.MiLinksCentre = new System.Windows.Forms.ToolStripMenuItem();
            this.MiLinksBottom = new System.Windows.Forms.ToolStripMenuItem();
            this.MiPropertiesLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.MiPropertiesLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.MiPropertiesRight = new System.Windows.Forms.ToolStripMenuItem();
            this.MiPropertiesCentre = new System.Windows.Forms.ToolStripMenuItem();
            this.MiPropertiesBottom = new System.Windows.Forms.ToolStripMenuItem();
            this.MiReports = new System.Windows.Forms.ToolStripMenuItem();
            this.MiLinkedNoteReport = new System.Windows.Forms.ToolStripMenuItem();
            this.MiNotePropertiesReport = new System.Windows.Forms.ToolStripMenuItem();
            this.TcBottom = new System.Windows.Forms.TabControl();
            this.CmnuNoteLinks = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MiOpenInNewWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.MiPasteNoteLink = new System.Windows.Forms.ToolStripMenuItem();
            this.SplitterBottom = new System.Windows.Forms.Splitter();
            this.TcLeft = new System.Windows.Forms.TabControl();
            this.TcRight = new System.Windows.Forms.TabControl();
            this.SplitterLeft = new System.Windows.Forms.Splitter();
            this.SplitterRight = new System.Windows.Forms.Splitter();
            this.TimerExternalEditorCheck = new System.Windows.Forms.Timer(this.components);
            this.TcMain.SuspendLayout();
            this.TpText.SuspendLayout();
            this.TsMarkdown.SuspendLayout();
            this.TsRichText.SuspendLayout();
            this.TpProperties.SuspendLayout();
            this.TpLinks.SuspendLayout();
            this.panel1.SuspendLayout();
            this.CmnuEditor.SuspendLayout();
            this.SsStatus.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.CmnuNoteLinks.SuspendLayout();
            this.SuspendLayout();
            // 
            // TcMain
            // 
            this.TcMain.Controls.Add(this.TpText);
            this.TcMain.Controls.Add(this.TpProperties);
            this.TcMain.Controls.Add(this.TpLinks);
            this.TcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TcMain.Location = new System.Drawing.Point(203, 20);
            this.TcMain.Name = "TcMain";
            this.TcMain.SelectedIndex = 0;
            this.TcMain.Size = new System.Drawing.Size(926, 423);
            this.TcMain.TabIndex = 4;
            // 
            // TpText
            // 
            this.TpText.Controls.Add(this.TsMarkdown);
            this.TpText.Controls.Add(this.TsRichText);
            this.TpText.Controls.Add(this.CboTextFormat);
            this.TpText.Location = new System.Drawing.Point(4, 22);
            this.TpText.Name = "TpText";
            this.TpText.Padding = new System.Windows.Forms.Padding(3);
            this.TpText.Size = new System.Drawing.Size(918, 397);
            this.TpText.TabIndex = 0;
            this.TpText.Text = "Text";
            this.TpText.UseVisualStyleBackColor = true;
            // 
            // TsMarkdown
            // 
            this.TsMarkdown.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbtnMarkdownSwitch,
            this.TsbtnShowHtml});
            this.TsMarkdown.Location = new System.Drawing.Point(3, 49);
            this.TsMarkdown.Name = "TsMarkdown";
            this.TsMarkdown.Size = new System.Drawing.Size(912, 25);
            this.TsMarkdown.TabIndex = 11;
            this.TsMarkdown.Text = "toolStrip1";
            // 
            // TsbtnMarkdownSwitch
            // 
            this.TsbtnMarkdownSwitch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbtnMarkdownSwitch.Image = ((System.Drawing.Image)(resources.GetObject("TsbtnMarkdownSwitch.Image")));
            this.TsbtnMarkdownSwitch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbtnMarkdownSwitch.Name = "TsbtnMarkdownSwitch";
            this.TsbtnMarkdownSwitch.Size = new System.Drawing.Size(46, 22);
            this.TsbtnMarkdownSwitch.Text = "Switch";
            this.TsbtnMarkdownSwitch.Click += new System.EventHandler(this.TsbtnMarkdownSwitchClick);
            // 
            // TsbtnShowHtml
            // 
            this.TsbtnShowHtml.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbtnShowHtml.Image = ((System.Drawing.Image)(resources.GetObject("TsbtnShowHtml.Image")));
            this.TsbtnShowHtml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbtnShowHtml.Name = "TsbtnShowHtml";
            this.TsbtnShowHtml.Size = new System.Drawing.Size(44, 22);
            this.TsbtnShowHtml.Text = "HTML";
            this.TsbtnShowHtml.Click += new System.EventHandler(this.TsbtnShowHtmlClick);
            // 
            // TsRichText
            // 
            this.TsRichText.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbtnBold,
            this.TsbtnItalic,
            this.TsbtnUnderline,
            this.TsbtnRemoveFormatting});
            this.TsRichText.Location = new System.Drawing.Point(3, 24);
            this.TsRichText.Name = "TsRichText";
            this.TsRichText.Size = new System.Drawing.Size(912, 25);
            this.TsRichText.TabIndex = 10;
            this.TsRichText.Text = "toolStrip1";
            // 
            // TsbtnBold
            // 
            this.TsbtnBold.CheckOnClick = true;
            this.TsbtnBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbtnBold.Image = ((System.Drawing.Image)(resources.GetObject("TsbtnBold.Image")));
            this.TsbtnBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbtnBold.Name = "TsbtnBold";
            this.TsbtnBold.Size = new System.Drawing.Size(35, 22);
            this.TsbtnBold.Text = "Bold";
            this.TsbtnBold.Click += new System.EventHandler(this.TsbtnFormatClick);
            // 
            // TsbtnItalic
            // 
            this.TsbtnItalic.CheckOnClick = true;
            this.TsbtnItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbtnItalic.Image = ((System.Drawing.Image)(resources.GetObject("TsbtnItalic.Image")));
            this.TsbtnItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbtnItalic.Name = "TsbtnItalic";
            this.TsbtnItalic.Size = new System.Drawing.Size(36, 22);
            this.TsbtnItalic.Text = "Italic";
            this.TsbtnItalic.Click += new System.EventHandler(this.TsbtnFormatClick);
            // 
            // TsbtnUnderline
            // 
            this.TsbtnUnderline.CheckOnClick = true;
            this.TsbtnUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbtnUnderline.Image = ((System.Drawing.Image)(resources.GetObject("TsbtnUnderline.Image")));
            this.TsbtnUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbtnUnderline.Name = "TsbtnUnderline";
            this.TsbtnUnderline.Size = new System.Drawing.Size(62, 22);
            this.TsbtnUnderline.Text = "Underline";
            this.TsbtnUnderline.Click += new System.EventHandler(this.TsbtnFormatClick);
            // 
            // TsbtnRemoveFormatting
            // 
            this.TsbtnRemoveFormatting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbtnRemoveFormatting.Image = ((System.Drawing.Image)(resources.GetObject("TsbtnRemoveFormatting.Image")));
            this.TsbtnRemoveFormatting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbtnRemoveFormatting.Name = "TsbtnRemoveFormatting";
            this.TsbtnRemoveFormatting.Size = new System.Drawing.Size(116, 22);
            this.TsbtnRemoveFormatting.Text = "Remove Formatting";
            this.TsbtnRemoveFormatting.Click += new System.EventHandler(this.TsbtnRemoveFormattingClick);
            // 
            // CboTextFormat
            // 
            this.CboTextFormat.Dock = System.Windows.Forms.DockStyle.Top;
            this.CboTextFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboTextFormat.FormattingEnabled = true;
            this.CboTextFormat.Location = new System.Drawing.Point(3, 3);
            this.CboTextFormat.Name = "CboTextFormat";
            this.CboTextFormat.Size = new System.Drawing.Size(912, 21);
            this.CboTextFormat.TabIndex = 1;
            this.CboTextFormat.SelectedIndexChanged += new System.EventHandler(this.CboTextFormatSelectedIndexChanged);
            // 
            // TpProperties
            // 
            this.TpProperties.Controls.Add(this.PgProperties);
            this.TpProperties.Controls.Add(this.BtnNewProperty);
            this.TpProperties.Controls.Add(this.BtnDeleteProperty);
            this.TpProperties.Location = new System.Drawing.Point(4, 22);
            this.TpProperties.Name = "TpProperties";
            this.TpProperties.Padding = new System.Windows.Forms.Padding(3);
            this.TpProperties.Size = new System.Drawing.Size(918, 397);
            this.TpProperties.TabIndex = 1;
            this.TpProperties.Text = "Properties";
            this.TpProperties.UseVisualStyleBackColor = true;
            // 
            // PgProperties
            // 
            this.PgProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PgProperties.Enabled = false;
            this.PgProperties.Location = new System.Drawing.Point(3, 3);
            this.PgProperties.Name = "PgProperties";
            this.PgProperties.Size = new System.Drawing.Size(909, 359);
            this.PgProperties.TabIndex = 0;
            // 
            // BtnNewProperty
            // 
            this.BtnNewProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnNewProperty.Location = new System.Drawing.Point(837, 368);
            this.BtnNewProperty.Name = "BtnNewProperty";
            this.BtnNewProperty.Size = new System.Drawing.Size(75, 23);
            this.BtnNewProperty.TabIndex = 2;
            this.BtnNewProperty.Text = "New";
            this.BtnNewProperty.UseVisualStyleBackColor = true;
            this.BtnNewProperty.Click += new System.EventHandler(this.BtnNewPropertyClick);
            // 
            // BtnDeleteProperty
            // 
            this.BtnDeleteProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnDeleteProperty.Location = new System.Drawing.Point(756, 368);
            this.BtnDeleteProperty.Name = "BtnDeleteProperty";
            this.BtnDeleteProperty.Size = new System.Drawing.Size(75, 23);
            this.BtnDeleteProperty.TabIndex = 1;
            this.BtnDeleteProperty.Text = "Delete";
            this.BtnDeleteProperty.UseVisualStyleBackColor = true;
            this.BtnDeleteProperty.Click += new System.EventHandler(this.BtnDeletePropertyClick);
            // 
            // TpLinks
            // 
            this.TpLinks.Controls.Add(this.LvLinks);
            this.TpLinks.Controls.Add(this.panel1);
            this.TpLinks.Location = new System.Drawing.Point(4, 22);
            this.TpLinks.Name = "TpLinks";
            this.TpLinks.Padding = new System.Windows.Forms.Padding(3);
            this.TpLinks.Size = new System.Drawing.Size(918, 397);
            this.TpLinks.TabIndex = 2;
            this.TpLinks.Text = "Links";
            this.TpLinks.UseVisualStyleBackColor = true;
            // 
            // LvLinks
            // 
            this.LvLinks.AllowDrop = true;
            this.LvLinks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ChLinkType,
            this.ChLinkName});
            this.LvLinks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LvLinks.FullRowSelect = true;
            this.LvLinks.HideSelection = false;
            this.LvLinks.Location = new System.Drawing.Point(3, 35);
            this.LvLinks.Name = "LvLinks";
            this.LvLinks.Size = new System.Drawing.Size(912, 359);
            this.LvLinks.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.LvLinks.TabIndex = 0;
            this.LvLinks.UseCompatibleStateImageBehavior = false;
            this.LvLinks.View = System.Windows.Forms.View.Details;
            this.LvLinks.DragDrop += new System.Windows.Forms.DragEventHandler(this.LvLinksDragDrop);
            this.LvLinks.DragEnter += new System.Windows.Forms.DragEventHandler(this.LvLinksDragEnter);
            this.LvLinks.DoubleClick += new System.EventHandler(this.LvLinksDoubleClick);
            this.LvLinks.KeyUp += new System.Windows.Forms.KeyEventHandler(this.LvLinksKeyUp);
            // 
            // ChLinkType
            // 
            this.ChLinkType.Text = "Type";
            this.ChLinkType.Width = 100;
            // 
            // ChLinkName
            // 
            this.ChLinkName.Text = "Name";
            this.ChLinkName.Width = 150;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CboLinkNoteFilterField);
            this.panel1.Controls.Add(this.CboLinkNoteFilterText);
            this.panel1.Controls.Add(this.LblLinkNoteFilter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(912, 32);
            this.panel1.TabIndex = 1;
            // 
            // CboLinkNoteFilterField
            // 
            this.CboLinkNoteFilterField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboLinkNoteFilterField.FormattingEnabled = true;
            this.CboLinkNoteFilterField.Location = new System.Drawing.Point(38, 6);
            this.CboLinkNoteFilterField.Name = "CboLinkNoteFilterField";
            this.CboLinkNoteFilterField.Size = new System.Drawing.Size(150, 21);
            this.CboLinkNoteFilterField.Sorted = true;
            this.CboLinkNoteFilterField.TabIndex = 4;
            this.CboLinkNoteFilterField.SelectedIndexChanged += new System.EventHandler(this.CboLinkNoteFilterField_SelectedIndexChanged);
            // 
            // CboLinkNoteFilterText
            // 
            this.CboLinkNoteFilterText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CboLinkNoteFilterText.FormattingEnabled = true;
            this.CboLinkNoteFilterText.Location = new System.Drawing.Point(194, 6);
            this.CboLinkNoteFilterText.Name = "CboLinkNoteFilterText";
            this.CboLinkNoteFilterText.Size = new System.Drawing.Size(715, 21);
            this.CboLinkNoteFilterText.Sorted = true;
            this.CboLinkNoteFilterText.TabIndex = 3;
            this.CboLinkNoteFilterText.SelectedIndexChanged += new System.EventHandler(this.CboLinkNoteFilterText_SelectedIndexChanged);
            this.CboLinkNoteFilterText.TextChanged += new System.EventHandler(this.CboLinkNoteFilterText_TextChanged);
            // 
            // LblLinkNoteFilter
            // 
            this.LblLinkNoteFilter.AutoSize = true;
            this.LblLinkNoteFilter.Location = new System.Drawing.Point(3, 9);
            this.LblLinkNoteFilter.Name = "LblLinkNoteFilter";
            this.LblLinkNoteFilter.Size = new System.Drawing.Size(29, 13);
            this.LblLinkNoteFilter.TabIndex = 0;
            this.LblLinkNoteFilter.Text = "Filter";
            // 
            // TxtName
            // 
            this.TxtName.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtName.Location = new System.Drawing.Point(203, 0);
            this.TxtName.Name = "TxtName";
            this.TxtName.Size = new System.Drawing.Size(926, 20);
            this.TxtName.TabIndex = 12;
            // 
            // CmnuEditor
            // 
            this.CmnuEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiPastePlain,
            this.MiUndo});
            this.CmnuEditor.Name = "contextMenuStrip1";
            this.CmnuEditor.Size = new System.Drawing.Size(132, 48);
            this.CmnuEditor.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip1Opening);
            // 
            // MiPastePlain
            // 
            this.MiPastePlain.Name = "MiPastePlain";
            this.MiPastePlain.Size = new System.Drawing.Size(131, 22);
            this.MiPastePlain.Text = "Paste Plain";
            this.MiPastePlain.Click += new System.EventHandler(this.MiPastePlainClick);
            // 
            // MiUndo
            // 
            this.MiUndo.Name = "MiUndo";
            this.MiUndo.Size = new System.Drawing.Size(131, 22);
            this.MiUndo.Text = "Undo";
            this.MiUndo.Click += new System.EventHandler(this.MiUndo_Click);
            // 
            // SsStatus
            // 
            this.SsStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LblNoteState,
            this.LblPinned,
            this.LblExternalEditor,
            this.LblIsTemplate,
            this.TsbtnApplyTemplate});
            this.SsStatus.Location = new System.Drawing.Point(0, 546);
            this.SsStatus.Name = "SsStatus";
            this.SsStatus.Size = new System.Drawing.Size(1332, 22);
            this.SsStatus.TabIndex = 5;
            this.SsStatus.Text = "statusStrip1";
            // 
            // LblNoteState
            // 
            this.LblNoteState.AutoSize = false;
            this.LblNoteState.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.LblNoteState.Name = "LblNoteState";
            this.LblNoteState.Size = new System.Drawing.Size(70, 17);
            this.LblNoteState.Text = "Unmodified";
            // 
            // LblPinned
            // 
            this.LblPinned.AutoSize = false;
            this.LblPinned.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.LblPinned.Name = "LblPinned";
            this.LblPinned.Size = new System.Drawing.Size(59, 17);
            this.LblPinned.Text = "Unpinned";
            this.LblPinned.Click += new System.EventHandler(this.LblPinnedClick);
            // 
            // LblExternalEditor
            // 
            this.LblExternalEditor.Name = "LblExternalEditor";
            this.LblExternalEditor.Size = new System.Drawing.Size(82, 17);
            this.LblExternalEditor.Text = "External Editor";
            this.LblExternalEditor.Click += new System.EventHandler(this.LblExternalEditorClick);
            // 
            // LblIsTemplate
            // 
            this.LblIsTemplate.Name = "LblIsTemplate";
            this.LblIsTemplate.Size = new System.Drawing.Size(91, 17);
            this.LblIsTemplate.Text = "Is Not Template";
            this.LblIsTemplate.Click += new System.EventHandler(this.LblIsTemplateClick);
            // 
            // TsbtnApplyTemplate
            // 
            this.TsbtnApplyTemplate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbtnApplyTemplate.Image = ((System.Drawing.Image)(resources.GetObject("TsbtnApplyTemplate.Image")));
            this.TsbtnApplyTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbtnApplyTemplate.Name = "TsbtnApplyTemplate";
            this.TsbtnApplyTemplate.Size = new System.Drawing.Size(104, 20);
            this.TsbtnApplyTemplate.Text = "Apply Template";
            this.TsbtnApplyTemplate.DropDownOpening += new System.EventHandler(this.TsbtnApplyTemplateDropDownOpening);
            this.TsbtnApplyTemplate.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.TsbtnApplyTemplateDropDownItemClicked);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiEdit,
            this.MiView});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1332, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // MiEdit
            // 
            this.MiEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiCopyNoteLink});
            this.MiEdit.Name = "MiEdit";
            this.MiEdit.Size = new System.Drawing.Size(39, 20);
            this.MiEdit.Text = "Edit";
            // 
            // MiCopyNoteLink
            // 
            this.MiCopyNoteLink.Name = "MiCopyNoteLink";
            this.MiCopyNoteLink.Size = new System.Drawing.Size(156, 22);
            this.MiCopyNoteLink.Text = "Copy Note Link";
            this.MiCopyNoteLink.Click += new System.EventHandler(this.MiCopyNoteLink_Click);
            // 
            // MiView
            // 
            this.MiView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiNotesNavigateForwards,
            this.MiNotesNavigateBackwards,
            this.MiLinksLocation,
            this.MiPropertiesLocation,
            this.MiReports});
            this.MiView.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.MiView.Name = "MiView";
            this.MiView.Size = new System.Drawing.Size(44, 20);
            this.MiView.Text = "View";
            // 
            // MiNotesNavigateForwards
            // 
            this.MiNotesNavigateForwards.Enabled = false;
            this.MiNotesNavigateForwards.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MiNotesNavigateForwards.MergeIndex = 1;
            this.MiNotesNavigateForwards.Name = "MiNotesNavigateForwards";
            this.MiNotesNavigateForwards.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Right)));
            this.MiNotesNavigateForwards.Size = new System.Drawing.Size(230, 22);
            this.MiNotesNavigateForwards.Text = "Navigate Forwards";
            this.MiNotesNavigateForwards.Click += new System.EventHandler(this.MiNotesNavigateForwardsClick);
            // 
            // MiNotesNavigateBackwards
            // 
            this.MiNotesNavigateBackwards.Enabled = false;
            this.MiNotesNavigateBackwards.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.MiNotesNavigateBackwards.MergeIndex = 2;
            this.MiNotesNavigateBackwards.Name = "MiNotesNavigateBackwards";
            this.MiNotesNavigateBackwards.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Left)));
            this.MiNotesNavigateBackwards.Size = new System.Drawing.Size(230, 22);
            this.MiNotesNavigateBackwards.Text = "Navigate Backwards";
            this.MiNotesNavigateBackwards.Click += new System.EventHandler(this.MiNotesNavigateBackwardsClick);
            // 
            // MiLinksLocation
            // 
            this.MiLinksLocation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiLinksLeft,
            this.MiLinksRight,
            this.MiLinksCentre,
            this.MiLinksBottom});
            this.MiLinksLocation.Name = "MiLinksLocation";
            this.MiLinksLocation.Size = new System.Drawing.Size(230, 22);
            this.MiLinksLocation.Text = "Links Location";
            // 
            // MiLinksLeft
            // 
            this.MiLinksLeft.Name = "MiLinksLeft";
            this.MiLinksLeft.Size = new System.Drawing.Size(114, 22);
            this.MiLinksLeft.Text = "Left";
            this.MiLinksLeft.Click += new System.EventHandler(this.MiLinksLeftClick);
            // 
            // MiLinksRight
            // 
            this.MiLinksRight.Name = "MiLinksRight";
            this.MiLinksRight.Size = new System.Drawing.Size(114, 22);
            this.MiLinksRight.Text = "Right";
            this.MiLinksRight.Click += new System.EventHandler(this.MiLinksRightClick);
            // 
            // MiLinksCentre
            // 
            this.MiLinksCentre.Checked = true;
            this.MiLinksCentre.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MiLinksCentre.Name = "MiLinksCentre";
            this.MiLinksCentre.Size = new System.Drawing.Size(114, 22);
            this.MiLinksCentre.Text = "Centre";
            this.MiLinksCentre.Click += new System.EventHandler(this.MiLinksCentreClick);
            // 
            // MiLinksBottom
            // 
            this.MiLinksBottom.Name = "MiLinksBottom";
            this.MiLinksBottom.Size = new System.Drawing.Size(114, 22);
            this.MiLinksBottom.Text = "Bottom";
            this.MiLinksBottom.Click += new System.EventHandler(this.MiLinksBottomClick);
            // 
            // MiPropertiesLocation
            // 
            this.MiPropertiesLocation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiPropertiesLeft,
            this.MiPropertiesRight,
            this.MiPropertiesCentre,
            this.MiPropertiesBottom});
            this.MiPropertiesLocation.Name = "MiPropertiesLocation";
            this.MiPropertiesLocation.Size = new System.Drawing.Size(230, 22);
            this.MiPropertiesLocation.Text = "Properties Location";
            // 
            // MiPropertiesLeft
            // 
            this.MiPropertiesLeft.CheckOnClick = true;
            this.MiPropertiesLeft.Name = "MiPropertiesLeft";
            this.MiPropertiesLeft.Size = new System.Drawing.Size(114, 22);
            this.MiPropertiesLeft.Text = "Left";
            this.MiPropertiesLeft.Click += new System.EventHandler(this.MiPropertiesLeftClick);
            // 
            // MiPropertiesRight
            // 
            this.MiPropertiesRight.CheckOnClick = true;
            this.MiPropertiesRight.Name = "MiPropertiesRight";
            this.MiPropertiesRight.Size = new System.Drawing.Size(114, 22);
            this.MiPropertiesRight.Text = "Right";
            this.MiPropertiesRight.Click += new System.EventHandler(this.MiPropertiesRightClick);
            // 
            // MiPropertiesCentre
            // 
            this.MiPropertiesCentre.Checked = true;
            this.MiPropertiesCentre.CheckOnClick = true;
            this.MiPropertiesCentre.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MiPropertiesCentre.Name = "MiPropertiesCentre";
            this.MiPropertiesCentre.Size = new System.Drawing.Size(114, 22);
            this.MiPropertiesCentre.Text = "Centre";
            this.MiPropertiesCentre.Click += new System.EventHandler(this.MiPropertiesCentreClick);
            // 
            // MiPropertiesBottom
            // 
            this.MiPropertiesBottom.CheckOnClick = true;
            this.MiPropertiesBottom.Name = "MiPropertiesBottom";
            this.MiPropertiesBottom.Size = new System.Drawing.Size(114, 22);
            this.MiPropertiesBottom.Text = "Bottom";
            this.MiPropertiesBottom.Click += new System.EventHandler(this.MiPropertiesBottomClick);
            // 
            // MiReports
            // 
            this.MiReports.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiLinkedNoteReport,
            this.MiNotePropertiesReport});
            this.MiReports.Name = "MiReports";
            this.MiReports.Size = new System.Drawing.Size(230, 22);
            this.MiReports.Text = "Reports";
            // 
            // MiLinkedNoteReport
            // 
            this.MiLinkedNoteReport.Name = "MiLinkedNoteReport";
            this.MiLinkedNoteReport.Size = new System.Drawing.Size(156, 22);
            this.MiLinkedNoteReport.Text = "Linked Notes";
            this.MiLinkedNoteReport.Click += new System.EventHandler(this.MiLinkedNoteReportClick);
            // 
            // MiNotePropertiesReport
            // 
            this.MiNotePropertiesReport.Name = "MiNotePropertiesReport";
            this.MiNotePropertiesReport.Size = new System.Drawing.Size(156, 22);
            this.MiNotePropertiesReport.Text = "Note Properties";
            this.MiNotePropertiesReport.Click += new System.EventHandler(this.MiNotePropertiesReportClick);
            // 
            // TcBottom
            // 
            this.TcBottom.ContextMenuStrip = this.CmnuNoteLinks;
            this.TcBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TcBottom.Location = new System.Drawing.Point(0, 446);
            this.TcBottom.Name = "TcBottom";
            this.TcBottom.SelectedIndex = 0;
            this.TcBottom.Size = new System.Drawing.Size(1332, 100);
            this.TcBottom.TabIndex = 10;
            this.TcBottom.Visible = false;
            // 
            // CmnuNoteLinks
            // 
            this.CmnuNoteLinks.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiOpenInNewWindow,
            this.MiPasteNoteLink});
            this.CmnuNoteLinks.Name = "CmnuNoteLinks";
            this.CmnuNoteLinks.Size = new System.Drawing.Size(191, 70);
            this.CmnuNoteLinks.Opening += new System.ComponentModel.CancelEventHandler(this.CmnuNoteLinks_Opening);
            // 
            // MiOpenInNewWindow
            // 
            this.MiOpenInNewWindow.Name = "MiOpenInNewWindow";
            this.MiOpenInNewWindow.Size = new System.Drawing.Size(190, 22);
            this.MiOpenInNewWindow.Text = "Open In New Window";
            this.MiOpenInNewWindow.Click += new System.EventHandler(this.MiOpenInNewWindow_Click);
            // 
            // MiPasteNoteLink
            // 
            this.MiPasteNoteLink.Name = "MiPasteNoteLink";
            this.MiPasteNoteLink.Size = new System.Drawing.Size(190, 22);
            this.MiPasteNoteLink.Text = "Paste Note Link";
            this.MiPasteNoteLink.Click += new System.EventHandler(this.MiPasteNoteLink_Click);
            // 
            // SplitterBottom
            // 
            this.SplitterBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SplitterBottom.Location = new System.Drawing.Point(0, 443);
            this.SplitterBottom.Name = "SplitterBottom";
            this.SplitterBottom.Size = new System.Drawing.Size(1332, 3);
            this.SplitterBottom.TabIndex = 11;
            this.SplitterBottom.TabStop = false;
            this.SplitterBottom.Visible = false;
            // 
            // TcLeft
            // 
            this.TcLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.TcLeft.Location = new System.Drawing.Point(0, 0);
            this.TcLeft.Name = "TcLeft";
            this.TcLeft.SelectedIndex = 0;
            this.TcLeft.Size = new System.Drawing.Size(200, 443);
            this.TcLeft.TabIndex = 12;
            this.TcLeft.Visible = false;
            // 
            // TcRight
            // 
            this.TcRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.TcRight.Location = new System.Drawing.Point(1132, 0);
            this.TcRight.Name = "TcRight";
            this.TcRight.SelectedIndex = 0;
            this.TcRight.Size = new System.Drawing.Size(200, 443);
            this.TcRight.TabIndex = 13;
            this.TcRight.Visible = false;
            // 
            // SplitterLeft
            // 
            this.SplitterLeft.Location = new System.Drawing.Point(200, 0);
            this.SplitterLeft.Name = "SplitterLeft";
            this.SplitterLeft.Size = new System.Drawing.Size(3, 443);
            this.SplitterLeft.TabIndex = 14;
            this.SplitterLeft.TabStop = false;
            this.SplitterLeft.Visible = false;
            // 
            // SplitterRight
            // 
            this.SplitterRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.SplitterRight.Location = new System.Drawing.Point(1129, 0);
            this.SplitterRight.Name = "SplitterRight";
            this.SplitterRight.Size = new System.Drawing.Size(3, 443);
            this.SplitterRight.TabIndex = 15;
            this.SplitterRight.TabStop = false;
            this.SplitterRight.Visible = false;
            // 
            // TimerExternalEditorCheck
            // 
            this.TimerExternalEditorCheck.Interval = 1000;
            this.TimerExternalEditorCheck.Tick += new System.EventHandler(this.TimerExternalEditorCheck_Tick);
            // 
            // NoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1332, 568);
            this.Controls.Add(this.TcMain);
            this.Controls.Add(this.TxtName);
            this.Controls.Add(this.SplitterRight);
            this.Controls.Add(this.SplitterLeft);
            this.Controls.Add(this.TcRight);
            this.Controls.Add(this.TcLeft);
            this.Controls.Add(this.SplitterBottom);
            this.Controls.Add(this.TcBottom);
            this.Controls.Add(this.SsStatus);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "NoteForm";
            this.Text = "NoteForm";
            this.TcMain.ResumeLayout(false);
            this.TpText.ResumeLayout(false);
            this.TpText.PerformLayout();
            this.TsMarkdown.ResumeLayout(false);
            this.TsMarkdown.PerformLayout();
            this.TsRichText.ResumeLayout(false);
            this.TsRichText.PerformLayout();
            this.TpProperties.ResumeLayout(false);
            this.TpLinks.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.CmnuEditor.ResumeLayout(false);
            this.SsStatus.ResumeLayout(false);
            this.SsStatus.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.CmnuNoteLinks.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip SsStatus;
        private System.Windows.Forms.ToolStripStatusLabel LblNoteState;
        private System.Windows.Forms.ToolStripStatusLabel LblPinned;
        private System.Windows.Forms.ContextMenuStrip CmnuEditor;
        private System.Windows.Forms.ToolStripMenuItem MiPastePlain;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MiView;
        public System.Windows.Forms.ToolStripMenuItem MiNotesNavigateForwards;
        public System.Windows.Forms.ToolStripMenuItem MiNotesNavigateBackwards;
        public System.Windows.Forms.TabControl TcMain;
        public System.Windows.Forms.TabPage TpProperties;
        public System.Windows.Forms.PropertyGrid PgProperties;
        public System.Windows.Forms.TabPage TpText;
        private System.Windows.Forms.Button BtnNewProperty;
        private System.Windows.Forms.Button BtnDeleteProperty;
        private System.Windows.Forms.ToolStripDropDownButton TsbtnApplyTemplate;
        private System.Windows.Forms.TabPage TpLinks;
        private System.Windows.Forms.ListView LvLinks;
        private System.Windows.Forms.ColumnHeader ChLinkName;
        private System.Windows.Forms.ToolStripStatusLabel LblIsTemplate;
        private System.Windows.Forms.ColumnHeader ChLinkType;
        private System.Windows.Forms.ToolStripMenuItem MiPropertiesLocation;
        private System.Windows.Forms.ToolStripMenuItem MiPropertiesLeft;
        private System.Windows.Forms.ToolStripMenuItem MiPropertiesRight;
        private System.Windows.Forms.ToolStripMenuItem MiPropertiesCentre;
        private System.Windows.Forms.ToolStripMenuItem MiPropertiesBottom;
        private System.Windows.Forms.TabControl TcBottom;
        private System.Windows.Forms.Splitter SplitterBottom;
        private System.Windows.Forms.TabControl TcLeft;
        private System.Windows.Forms.TabControl TcRight;
        private System.Windows.Forms.Splitter SplitterLeft;
        private System.Windows.Forms.Splitter SplitterRight;
        private System.Windows.Forms.ToolStripMenuItem MiReports;
        private System.Windows.Forms.ToolStripMenuItem MiLinkedNoteReport;
        private System.Windows.Forms.ToolStripMenuItem MiNotePropertiesReport;
        private System.Windows.Forms.ToolStrip TsRichText;
        private System.Windows.Forms.ToolStripButton TsbtnBold;
        private System.Windows.Forms.ToolStripButton TsbtnItalic;
        private System.Windows.Forms.ToolStripButton TsbtnUnderline;
        private System.Windows.Forms.ToolStripButton TsbtnRemoveFormatting;
        private System.Windows.Forms.ComboBox CboTextFormat;
        private System.Windows.Forms.ToolStrip TsMarkdown;
        private System.Windows.Forms.ToolStripButton TsbtnMarkdownSwitch;
        private System.Windows.Forms.ToolStripButton TsbtnShowHtml;
        private System.Windows.Forms.ToolStripMenuItem MiLinksLocation;
        private System.Windows.Forms.ToolStripMenuItem MiLinksLeft;
        private System.Windows.Forms.ToolStripMenuItem MiLinksRight;
        private System.Windows.Forms.ToolStripMenuItem MiLinksCentre;
        private System.Windows.Forms.ToolStripMenuItem MiLinksBottom;
        public System.Windows.Forms.TextBox TxtName;
        private System.Windows.Forms.ToolStripStatusLabel LblExternalEditor;
        private System.Windows.Forms.Timer TimerExternalEditorCheck;
        private System.Windows.Forms.ToolStripMenuItem MiUndo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox CboLinkNoteFilterText;
        private System.Windows.Forms.Label LblLinkNoteFilter;
        private System.Windows.Forms.ComboBox CboLinkNoteFilterField;
        private System.Windows.Forms.ContextMenuStrip CmnuNoteLinks;
        private System.Windows.Forms.ToolStripMenuItem MiOpenInNewWindow;
        private System.Windows.Forms.ToolStripMenuItem MiEdit;
        private System.Windows.Forms.ToolStripMenuItem MiCopyNoteLink;
        private System.Windows.Forms.ToolStripMenuItem MiPasteNoteLink;
    }
}