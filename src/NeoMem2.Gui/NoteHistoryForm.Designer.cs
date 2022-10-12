namespace NeoMem2.Gui
{
    partial class NoteHistoryForm
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
            this.LvChanges = new System.Windows.Forms.ListView();
            this.ChDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ChField = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ChType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ChValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LvChanges
            // 
            this.LvChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LvChanges.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ChDate,
            this.ChField,
            this.ChType,
            this.ChValue});
            this.LvChanges.FullRowSelect = true;
            this.LvChanges.HideSelection = false;
            this.LvChanges.Location = new System.Drawing.Point(12, 12);
            this.LvChanges.Name = "LvChanges";
            this.LvChanges.Size = new System.Drawing.Size(450, 207);
            this.LvChanges.TabIndex = 0;
            this.LvChanges.UseCompatibleStateImageBehavior = false;
            this.LvChanges.View = System.Windows.Forms.View.Details;
            this.LvChanges.DoubleClick += new System.EventHandler(this.LvChangesDoubleClick);
            // 
            // ChDate
            // 
            this.ChDate.Text = "Date";
            // 
            // ChField
            // 
            this.ChField.Text = "Field";
            // 
            // ChType
            // 
            this.ChType.Text = "Type";
            // 
            // ChValue
            // 
            this.ChValue.Text = "Value";
            // 
            // BtnOK
            // 
            this.BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOK.Location = new System.Drawing.Point(306, 225);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(75, 23);
            this.BtnOK.TabIndex = 1;
            this.BtnOK.Text = "OK";
            this.BtnOK.UseVisualStyleBackColor = true;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(387, 225);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 2;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // NoteHistoryForm
            // 
            this.AcceptButton = this.BtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(474, 260);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.LvChanges);
            this.Name = "NoteHistoryForm";
            this.Text = "Note History";
            this.Load += new System.EventHandler(this.NoteHistoryFormLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView LvChanges;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.ColumnHeader ChDate;
        private System.Windows.Forms.ColumnHeader ChField;
        private System.Windows.Forms.ColumnHeader ChType;
        private System.Windows.Forms.ColumnHeader ChValue;
    }
}