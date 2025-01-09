namespace StalkerPro
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtUrl = new TextBox();
            button1 = new Button();
            lstResults = new ListBox();
            txtLog = new TextBox();
            SuspendLayout();
            // 
            // txtUrl
            // 
            txtUrl.Location = new Point(402, 74);
            txtUrl.Name = "txtUrl";
            txtUrl.Size = new Size(299, 27);
            txtUrl.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(707, 73);
            button1.Name = "button1";
            button1.Size = new Size(53, 28);
            button1.TabIndex = 1;
            button1.Text = "Sök";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // lstResults
            // 
            lstResults.FormattingEnabled = true;
            lstResults.Location = new Point(12, 14);
            lstResults.Name = "lstResults";
            lstResults.Size = new Size(357, 424);
            lstResults.TabIndex = 2;
            // 
            // txtLog
            // 
            txtLog.Location = new Point(402, 176);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.Size = new Size(299, 262);
            txtLog.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtLog);
            Controls.Add(lstResults);
            Controls.Add(button1);
            Controls.Add(txtUrl);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtUrl;
        private Button button1;
        private ListBox lstResults;
        private TextBox txtLog;
    }
}
