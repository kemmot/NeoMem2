namespace NeoMem2.Gui
{
    partial class UpdateForm
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
            this.LvUpdates = new System.Windows.Forms.ListView();
            this.ChUpdateComponent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ChUpdateVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ChUpdateStep = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ChUpdateDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BtnStart = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ChStepStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // LvUpdates
            // 
            this.LvUpdates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LvUpdates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ChUpdateComponent,
            this.ChUpdateVersion,
            this.ChUpdateStep,
            this.ChUpdateDescription,
            this.ChStepStatus});
            this.LvUpdates.FullRowSelect = true;
            this.LvUpdates.HideSelection = false;
            this.LvUpdates.Location = new System.Drawing.Point(12, 32);
            this.LvUpdates.Name = "LvUpdates";
            this.LvUpdates.Size = new System.Drawing.Size(542, 189);
            this.LvUpdates.TabIndex = 0;
            this.LvUpdates.UseCompatibleStateImageBehavior = false;
            this.LvUpdates.View = System.Windows.Forms.View.Details;
            this.LvUpdates.DoubleClick += new System.EventHandler(this.LvUpdatesDoubleClick);
            // 
            // ChUpdateComponent
            // 
            this.ChUpdateComponent.Text = "Component";
            this.ChUpdateComponent.Width = 100;
            // 
            // ChUpdateVersion
            // 
            this.ChUpdateVersion.Text = "Version";
            // 
            // ChUpdateStep
            // 
            this.ChUpdateStep.Text = "Step";
            // 
            // ChUpdateDescription
            // 
            this.ChUpdateDescription.Text = "Description";
            this.ChUpdateDescription.Width = 100;
            // 
            // BtnStart
            // 
            this.BtnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnStart.Location = new System.Drawing.Point(398, 227);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(75, 23);
            this.BtnStart.TabIndex = 1;
            this.BtnStart.Text = "Start";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStartClick);
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.Location = new System.Drawing.Point(479, 227);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(75, 23);
            this.BtnClose.TabIndex = 2;
            this.BtnClose.Text = "Close";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnCloseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(341, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "The following updates are required to work with this version of software";
            // 
            // ChStepStatus
            // 
            this.ChStepStatus.Text = "Status";
            this.ChStepStatus.Width = 100;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker1RunWorkerCompleted);
            // 
            // UpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 262);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.LvUpdates);
            this.Name = "UpdateForm";
            this.Text = "Updates";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView LvUpdates;
        private System.Windows.Forms.ColumnHeader ChUpdateComponent;
        private System.Windows.Forms.ColumnHeader ChUpdateVersion;
        private System.Windows.Forms.ColumnHeader ChUpdateStep;
        private System.Windows.Forms.ColumnHeader ChUpdateDescription;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader ChStepStatus;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}