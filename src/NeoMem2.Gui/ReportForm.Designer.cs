namespace NeoMem2.Gui
{
    partial class ReportForm
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
            this.RtxtReportOutput = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // RtxtReportOutput
            // 
            this.RtxtReportOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RtxtReportOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RtxtReportOutput.Location = new System.Drawing.Point(0, 0);
            this.RtxtReportOutput.Name = "RtxtReportOutput";
            this.RtxtReportOutput.Size = new System.Drawing.Size(284, 262);
            this.RtxtReportOutput.TabIndex = 0;
            this.RtxtReportOutput.Text = "";
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.RtxtReportOutput);
            this.Name = "ReportForm";
            this.Text = "Report";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox RtxtReportOutput;

    }
}