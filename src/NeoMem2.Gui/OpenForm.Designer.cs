namespace NeoMem2.Gui
{
    partial class OpenForm
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
            this.CboStoreType = new System.Windows.Forms.ComboBox();
            this.LblStoreType = new System.Windows.Forms.Label();
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.LblConnectionString = new System.Windows.Forms.Label();
            this.CboConnectionString = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // CboStoreType
            // 
            this.CboStoreType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CboStoreType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboStoreType.FormattingEnabled = true;
            this.CboStoreType.Location = new System.Drawing.Point(109, 12);
            this.CboStoreType.Name = "CboStoreType";
            this.CboStoreType.Size = new System.Drawing.Size(401, 21);
            this.CboStoreType.TabIndex = 0;
            this.CboStoreType.SelectedIndexChanged += new System.EventHandler(this.CboStoreTypeSelectedIndexChanged);
            // 
            // LblStoreType
            // 
            this.LblStoreType.AutoSize = true;
            this.LblStoreType.Location = new System.Drawing.Point(44, 15);
            this.LblStoreType.Name = "LblStoreType";
            this.LblStoreType.Size = new System.Drawing.Size(59, 13);
            this.LblStoreType.TabIndex = 1;
            this.LblStoreType.Text = "Store Type";
            // 
            // BtnOK
            // 
            this.BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOK.Location = new System.Drawing.Point(354, 68);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(75, 23);
            this.BtnOK.TabIndex = 2;
            this.BtnOK.Text = "OK";
            this.BtnOK.UseVisualStyleBackColor = true;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(435, 68);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 3;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // LblConnectionString
            // 
            this.LblConnectionString.AutoSize = true;
            this.LblConnectionString.Location = new System.Drawing.Point(12, 44);
            this.LblConnectionString.Name = "LblConnectionString";
            this.LblConnectionString.Size = new System.Drawing.Size(91, 13);
            this.LblConnectionString.TabIndex = 4;
            this.LblConnectionString.Text = "Connection String";
            // 
            // CboConnectionString
            // 
            this.CboConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CboConnectionString.FormattingEnabled = true;
            this.CboConnectionString.Location = new System.Drawing.Point(109, 41);
            this.CboConnectionString.Name = "CboConnectionString";
            this.CboConnectionString.Size = new System.Drawing.Size(401, 21);
            this.CboConnectionString.TabIndex = 5;
            // 
            // OpenForm
            // 
            this.AcceptButton = this.BtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(522, 99);
            this.Controls.Add(this.CboConnectionString);
            this.Controls.Add(this.LblConnectionString);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.LblStoreType);
            this.Controls.Add(this.CboStoreType);
            this.Name = "OpenForm";
            this.Text = "Open Store";
            this.Load += new System.EventHandler(this.OpenFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CboStoreType;
        private System.Windows.Forms.Label LblStoreType;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Label LblConnectionString;
        private System.Windows.Forms.ComboBox CboConnectionString;
    }
}