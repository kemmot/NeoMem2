namespace NeoMem2.Gui
{
    partial class NewPropertyForm
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
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.LblName = new System.Windows.Forms.Label();
            this.LblClrDataType = new System.Windows.Forms.Label();
            this.CboName = new System.Windows.Forms.ComboBox();
            this.CboClrDataType = new System.Windows.Forms.ComboBox();
            this.LblValue = new System.Windows.Forms.Label();
            this.CboValue = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // BtnOK
            // 
            this.BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOK.Location = new System.Drawing.Point(191, 93);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(75, 23);
            this.BtnOK.TabIndex = 3;
            this.BtnOK.Text = "OK";
            this.BtnOK.UseVisualStyleBackColor = true;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(272, 93);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 4;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // LblName
            // 
            this.LblName.AutoSize = true;
            this.LblName.Location = new System.Drawing.Point(58, 15);
            this.LblName.Name = "LblName";
            this.LblName.Size = new System.Drawing.Size(35, 13);
            this.LblName.TabIndex = 2;
            this.LblName.Text = "Name";
            // 
            // LblClrDataType
            // 
            this.LblClrDataType.AutoSize = true;
            this.LblClrDataType.Location = new System.Drawing.Point(12, 42);
            this.LblClrDataType.Name = "LblClrDataType";
            this.LblClrDataType.Size = new System.Drawing.Size(81, 13);
            this.LblClrDataType.TabIndex = 3;
            this.LblClrDataType.Text = "CLR Data Type";
            // 
            // CboName
            // 
            this.CboName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CboName.FormattingEnabled = true;
            this.CboName.Location = new System.Drawing.Point(99, 12);
            this.CboName.Name = "CboName";
            this.CboName.Size = new System.Drawing.Size(248, 21);
            this.CboName.Sorted = true;
            this.CboName.TabIndex = 0;
            this.CboName.SelectedIndexChanged += new System.EventHandler(this.CboNameSelectedIndexChanged);
            // 
            // CboClrDataType
            // 
            this.CboClrDataType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CboClrDataType.FormattingEnabled = true;
            this.CboClrDataType.Location = new System.Drawing.Point(99, 39);
            this.CboClrDataType.Name = "CboClrDataType";
            this.CboClrDataType.Size = new System.Drawing.Size(248, 21);
            this.CboClrDataType.Sorted = true;
            this.CboClrDataType.TabIndex = 1;
            // 
            // LblValue
            // 
            this.LblValue.AutoSize = true;
            this.LblValue.Location = new System.Drawing.Point(59, 69);
            this.LblValue.Name = "LblValue";
            this.LblValue.Size = new System.Drawing.Size(34, 13);
            this.LblValue.TabIndex = 7;
            this.LblValue.Text = "Value";
            // 
            // CboValue
            // 
            this.CboValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CboValue.FormattingEnabled = true;
            this.CboValue.Location = new System.Drawing.Point(99, 66);
            this.CboValue.Name = "CboValue";
            this.CboValue.Size = new System.Drawing.Size(248, 21);
            this.CboValue.Sorted = true;
            this.CboValue.TabIndex = 2;
            // 
            // NewPropertyForm
            // 
            this.AcceptButton = this.BtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(351, 119);
            this.Controls.Add(this.CboValue);
            this.Controls.Add(this.LblValue);
            this.Controls.Add(this.CboClrDataType);
            this.Controls.Add(this.CboName);
            this.Controls.Add(this.LblClrDataType);
            this.Controls.Add(this.LblName);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOK);
            this.Name = "NewPropertyForm";
            this.Text = "New Property";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Label LblName;
        private System.Windows.Forms.Label LblClrDataType;
        private System.Windows.Forms.ComboBox CboName;
        private System.Windows.Forms.ComboBox CboClrDataType;
        private System.Windows.Forms.Label LblValue;
        private System.Windows.Forms.ComboBox CboValue;
    }
}