namespace NeoMem2.Gui
{
    partial class BatchChangeForm
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
            this.OptAddTag = new System.Windows.Forms.RadioButton();
            this.OptRemoveTag = new System.Windows.Forms.RadioButton();
            this.OptSetTag = new System.Windows.Forms.RadioButton();
            this.LblTag = new System.Windows.Forms.Label();
            this.BtnBatchChangeTags = new System.Windows.Forms.Button();
            this.LvTagsPreview = new System.Windows.Forms.ListView();
            this.ChName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ChOldTag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ChNewTag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TpTags = new System.Windows.Forms.TabPage();
            this.CboTag = new System.Windows.Forms.ComboBox();
            this.TpProperties = new System.Windows.Forms.TabPage();
            this.BtnBatchChangeProperties = new System.Windows.Forms.Button();
            this.LblPropertyValue = new System.Windows.Forms.Label();
            this.CboPropertyValue = new System.Windows.Forms.ComboBox();
            this.CboPropertyName = new System.Windows.Forms.ComboBox();
            this.OptAddProperty = new System.Windows.Forms.RadioButton();
            this.OptRemoveProperty = new System.Windows.Forms.RadioButton();
            this.LblPropertyName = new System.Windows.Forms.Label();
            this.LvPropertiesPreview = new System.Windows.Forms.ListView();
            this.ChPropertyNoteName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ChPropertyNoteOldValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ChPropertyNoteNewValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OptSetProperty = new System.Windows.Forms.RadioButton();
            this.tabControl1.SuspendLayout();
            this.TpTags.SuspendLayout();
            this.TpProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // OptAddTag
            // 
            this.OptAddTag.AutoSize = true;
            this.OptAddTag.Checked = true;
            this.OptAddTag.Location = new System.Drawing.Point(6, 6);
            this.OptAddTag.Name = "OptAddTag";
            this.OptAddTag.Size = new System.Drawing.Size(66, 17);
            this.OptAddTag.TabIndex = 0;
            this.OptAddTag.TabStop = true;
            this.OptAddTag.Text = "Add Tag";
            this.OptAddTag.UseVisualStyleBackColor = true;
            this.OptAddTag.CheckedChanged += new System.EventHandler(this.BatchChangeCheckedChanged);
            // 
            // OptRemoveTag
            // 
            this.OptRemoveTag.AutoSize = true;
            this.OptRemoveTag.Location = new System.Drawing.Point(6, 29);
            this.OptRemoveTag.Name = "OptRemoveTag";
            this.OptRemoveTag.Size = new System.Drawing.Size(87, 17);
            this.OptRemoveTag.TabIndex = 1;
            this.OptRemoveTag.Text = "Remove Tag";
            this.OptRemoveTag.UseVisualStyleBackColor = true;
            this.OptRemoveTag.CheckedChanged += new System.EventHandler(this.BatchChangeCheckedChanged);
            // 
            // OptSetTag
            // 
            this.OptSetTag.AutoSize = true;
            this.OptSetTag.Location = new System.Drawing.Point(6, 52);
            this.OptSetTag.Name = "OptSetTag";
            this.OptSetTag.Size = new System.Drawing.Size(63, 17);
            this.OptSetTag.TabIndex = 2;
            this.OptSetTag.Text = "Set Tag";
            this.OptSetTag.UseVisualStyleBackColor = true;
            this.OptSetTag.CheckedChanged += new System.EventHandler(this.BatchChangeCheckedChanged);
            // 
            // LblTag
            // 
            this.LblTag.AutoSize = true;
            this.LblTag.Location = new System.Drawing.Point(6, 78);
            this.LblTag.Name = "LblTag";
            this.LblTag.Size = new System.Drawing.Size(26, 13);
            this.LblTag.TabIndex = 3;
            this.LblTag.Text = "Tag";
            // 
            // BtnBatchChangeTags
            // 
            this.BtnBatchChangeTags.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnBatchChangeTags.Location = new System.Drawing.Point(336, 358);
            this.BtnBatchChangeTags.Name = "BtnBatchChangeTags";
            this.BtnBatchChangeTags.Size = new System.Drawing.Size(75, 23);
            this.BtnBatchChangeTags.TabIndex = 5;
            this.BtnBatchChangeTags.Text = "Change";
            this.BtnBatchChangeTags.UseVisualStyleBackColor = true;
            this.BtnBatchChangeTags.Click += new System.EventHandler(this.BtnBatchChangeTagsClick);
            // 
            // LvTagsPreview
            // 
            this.LvTagsPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LvTagsPreview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ChName,
            this.ChOldTag,
            this.ChNewTag});
            this.LvTagsPreview.Location = new System.Drawing.Point(38, 101);
            this.LvTagsPreview.Name = "LvTagsPreview";
            this.LvTagsPreview.Size = new System.Drawing.Size(375, 251);
            this.LvTagsPreview.TabIndex = 7;
            this.LvTagsPreview.UseCompatibleStateImageBehavior = false;
            this.LvTagsPreview.View = System.Windows.Forms.View.Details;
            // 
            // ChName
            // 
            this.ChName.Text = "Name";
            // 
            // ChOldTag
            // 
            this.ChOldTag.Text = "Old Tag";
            // 
            // ChNewTag
            // 
            this.ChNewTag.Text = "New Tag";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TpTags);
            this.tabControl1.Controls.Add(this.TpProperties);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(427, 413);
            this.tabControl1.TabIndex = 9;
            // 
            // TpTags
            // 
            this.TpTags.Controls.Add(this.CboTag);
            this.TpTags.Controls.Add(this.OptAddTag);
            this.TpTags.Controls.Add(this.BtnBatchChangeTags);
            this.TpTags.Controls.Add(this.OptRemoveTag);
            this.TpTags.Controls.Add(this.LblTag);
            this.TpTags.Controls.Add(this.LvTagsPreview);
            this.TpTags.Controls.Add(this.OptSetTag);
            this.TpTags.Location = new System.Drawing.Point(4, 22);
            this.TpTags.Name = "TpTags";
            this.TpTags.Padding = new System.Windows.Forms.Padding(3);
            this.TpTags.Size = new System.Drawing.Size(419, 387);
            this.TpTags.TabIndex = 0;
            this.TpTags.Text = "Tags";
            this.TpTags.UseVisualStyleBackColor = true;
            // 
            // CboTag
            // 
            this.CboTag.FormattingEnabled = true;
            this.CboTag.Location = new System.Drawing.Point(38, 75);
            this.CboTag.Name = "CboTag";
            this.CboTag.Size = new System.Drawing.Size(375, 21);
            this.CboTag.Sorted = true;
            this.CboTag.TabIndex = 8;
            this.CboTag.SelectedIndexChanged += new System.EventHandler(this.BatchChangeCheckedChanged);
            this.CboTag.TextChanged += new System.EventHandler(this.BatchChangeCheckedChanged);
            // 
            // TpProperties
            // 
            this.TpProperties.Controls.Add(this.BtnBatchChangeProperties);
            this.TpProperties.Controls.Add(this.LblPropertyValue);
            this.TpProperties.Controls.Add(this.CboPropertyValue);
            this.TpProperties.Controls.Add(this.CboPropertyName);
            this.TpProperties.Controls.Add(this.OptAddProperty);
            this.TpProperties.Controls.Add(this.OptRemoveProperty);
            this.TpProperties.Controls.Add(this.LblPropertyName);
            this.TpProperties.Controls.Add(this.LvPropertiesPreview);
            this.TpProperties.Controls.Add(this.OptSetProperty);
            this.TpProperties.Location = new System.Drawing.Point(4, 22);
            this.TpProperties.Name = "TpProperties";
            this.TpProperties.Padding = new System.Windows.Forms.Padding(3);
            this.TpProperties.Size = new System.Drawing.Size(419, 387);
            this.TpProperties.TabIndex = 1;
            this.TpProperties.Text = "Properties";
            this.TpProperties.UseVisualStyleBackColor = true;
            // 
            // BtnBatchChangeProperties
            // 
            this.BtnBatchChangeProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnBatchChangeProperties.Location = new System.Drawing.Point(336, 356);
            this.BtnBatchChangeProperties.Name = "BtnBatchChangeProperties";
            this.BtnBatchChangeProperties.Size = new System.Drawing.Size(75, 23);
            this.BtnBatchChangeProperties.TabIndex = 18;
            this.BtnBatchChangeProperties.Text = "Change";
            this.BtnBatchChangeProperties.UseVisualStyleBackColor = true;
            this.BtnBatchChangeProperties.Click += new System.EventHandler(this.BtnBatchChangePropertiesClick);
            // 
            // LblPropertyValue
            // 
            this.LblPropertyValue.AutoSize = true;
            this.LblPropertyValue.Location = new System.Drawing.Point(8, 104);
            this.LblPropertyValue.Name = "LblPropertyValue";
            this.LblPropertyValue.Size = new System.Drawing.Size(76, 13);
            this.LblPropertyValue.TabIndex = 17;
            this.LblPropertyValue.Text = "Property Value";
            // 
            // CboPropertyValue
            // 
            this.CboPropertyValue.FormattingEnabled = true;
            this.CboPropertyValue.Location = new System.Drawing.Point(89, 101);
            this.CboPropertyValue.Name = "CboPropertyValue";
            this.CboPropertyValue.Size = new System.Drawing.Size(322, 21);
            this.CboPropertyValue.Sorted = true;
            this.CboPropertyValue.TabIndex = 16;
            this.CboPropertyValue.SelectedIndexChanged += new System.EventHandler(this.CboPropertyValueSelectedIndexChanged);
            this.CboPropertyValue.TextChanged += new System.EventHandler(this.CboPropertyValueTextChanged);
            // 
            // CboPropertyName
            // 
            this.CboPropertyName.FormattingEnabled = true;
            this.CboPropertyName.Location = new System.Drawing.Point(89, 74);
            this.CboPropertyName.Name = "CboPropertyName";
            this.CboPropertyName.Size = new System.Drawing.Size(322, 21);
            this.CboPropertyName.Sorted = true;
            this.CboPropertyName.TabIndex = 15;
            this.CboPropertyName.SelectedIndexChanged += new System.EventHandler(this.CboPropertyNameSelectedIndexChanged);
            this.CboPropertyName.TextChanged += new System.EventHandler(this.CboPropertyNameTextChanged);
            // 
            // OptAddProperty
            // 
            this.OptAddProperty.AutoSize = true;
            this.OptAddProperty.Checked = true;
            this.OptAddProperty.Location = new System.Drawing.Point(6, 6);
            this.OptAddProperty.Name = "OptAddProperty";
            this.OptAddProperty.Size = new System.Drawing.Size(86, 17);
            this.OptAddProperty.TabIndex = 8;
            this.OptAddProperty.TabStop = true;
            this.OptAddProperty.Text = "Add Property";
            this.OptAddProperty.UseVisualStyleBackColor = true;
            this.OptAddProperty.CheckedChanged += new System.EventHandler(this.BatchPropertyChanged);
            // 
            // OptRemoveProperty
            // 
            this.OptRemoveProperty.AutoSize = true;
            this.OptRemoveProperty.Location = new System.Drawing.Point(6, 29);
            this.OptRemoveProperty.Name = "OptRemoveProperty";
            this.OptRemoveProperty.Size = new System.Drawing.Size(107, 17);
            this.OptRemoveProperty.TabIndex = 9;
            this.OptRemoveProperty.Text = "Remove Property";
            this.OptRemoveProperty.UseVisualStyleBackColor = true;
            this.OptRemoveProperty.CheckedChanged += new System.EventHandler(this.BatchPropertyChanged);
            // 
            // LblPropertyName
            // 
            this.LblPropertyName.AutoSize = true;
            this.LblPropertyName.Location = new System.Drawing.Point(6, 78);
            this.LblPropertyName.Name = "LblPropertyName";
            this.LblPropertyName.Size = new System.Drawing.Size(77, 13);
            this.LblPropertyName.TabIndex = 11;
            this.LblPropertyName.Text = "Property Name";
            // 
            // LvPropertiesPreview
            // 
            this.LvPropertiesPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LvPropertiesPreview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ChPropertyNoteName,
            this.ChPropertyNoteOldValue,
            this.ChPropertyNoteNewValue});
            this.LvPropertiesPreview.Location = new System.Drawing.Point(89, 128);
            this.LvPropertiesPreview.Name = "LvPropertiesPreview";
            this.LvPropertiesPreview.Size = new System.Drawing.Size(324, 224);
            this.LvPropertiesPreview.TabIndex = 14;
            this.LvPropertiesPreview.UseCompatibleStateImageBehavior = false;
            this.LvPropertiesPreview.View = System.Windows.Forms.View.Details;
            // 
            // ChPropertyNoteName
            // 
            this.ChPropertyNoteName.Text = "Name";
            // 
            // ChPropertyNoteOldValue
            // 
            this.ChPropertyNoteOldValue.Text = "Old Value";
            this.ChPropertyNoteOldValue.Width = 100;
            // 
            // ChPropertyNoteNewValue
            // 
            this.ChPropertyNoteNewValue.Text = "New Value";
            this.ChPropertyNoteNewValue.Width = 100;
            // 
            // OptSetProperty
            // 
            this.OptSetProperty.AutoSize = true;
            this.OptSetProperty.Location = new System.Drawing.Point(6, 52);
            this.OptSetProperty.Name = "OptSetProperty";
            this.OptSetProperty.Size = new System.Drawing.Size(83, 17);
            this.OptSetProperty.TabIndex = 10;
            this.OptSetProperty.Text = "Set Property";
            this.OptSetProperty.UseVisualStyleBackColor = true;
            this.OptSetProperty.CheckedChanged += new System.EventHandler(this.BatchPropertyChanged);
            // 
            // BatchChangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 413);
            this.Controls.Add(this.tabControl1);
            this.Name = "BatchChangeForm";
            this.Text = "Batch Change";
            this.tabControl1.ResumeLayout(false);
            this.TpTags.ResumeLayout(false);
            this.TpTags.PerformLayout();
            this.TpProperties.ResumeLayout(false);
            this.TpProperties.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton OptAddTag;
        private System.Windows.Forms.RadioButton OptRemoveTag;
        private System.Windows.Forms.RadioButton OptSetTag;
        private System.Windows.Forms.Label LblTag;
        private System.Windows.Forms.Button BtnBatchChangeTags;
        private System.Windows.Forms.ListView LvTagsPreview;
        private System.Windows.Forms.ColumnHeader ChName;
        private System.Windows.Forms.ColumnHeader ChOldTag;
        private System.Windows.Forms.ColumnHeader ChNewTag;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TpTags;
        private System.Windows.Forms.TabPage TpProperties;
        private System.Windows.Forms.RadioButton OptAddProperty;
        private System.Windows.Forms.RadioButton OptRemoveProperty;
        private System.Windows.Forms.Label LblPropertyName;
        private System.Windows.Forms.ListView LvPropertiesPreview;
        private System.Windows.Forms.ColumnHeader ChPropertyNoteName;
        private System.Windows.Forms.ColumnHeader ChPropertyNoteOldValue;
        private System.Windows.Forms.ColumnHeader ChPropertyNoteNewValue;
        private System.Windows.Forms.RadioButton OptSetProperty;
        private System.Windows.Forms.ComboBox CboPropertyName;
        private System.Windows.Forms.Label LblPropertyValue;
        private System.Windows.Forms.ComboBox CboPropertyValue;
        private System.Windows.Forms.Button BtnBatchChangeProperties;
        private System.Windows.Forms.ComboBox CboTag;
    }
}