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
            txtFirstName = new TextBox();
            button1 = new Button();
            lstResults = new ListBox();
            txtLog = new TextBox();
            txtLocation = new TextBox();
            numMaxAge = new NumericUpDown();
            numMinAge = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            txtLastName = new TextBox();
            ((System.ComponentModel.ISupportInitialize)numMaxAge).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinAge).BeginInit();
            SuspendLayout();
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(894, 15);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(246, 27);
            txtFirstName.TabIndex = 0;
            txtFirstName.Text = "William";
            // 
            // button1
            // 
            button1.Location = new Point(1115, 143);
            button1.Name = "button1";
            button1.Size = new Size(85, 28);
            button1.TabIndex = 1;
            button1.Text = "Sök";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnSearch_Click;
            // 
            // lstResults
            // 
            lstResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lstResults.FormattingEnabled = true;
            lstResults.Location = new Point(14, 11);
            lstResults.Name = "lstResults";
            lstResults.Size = new Size(769, 424);
            lstResults.TabIndex = 2;
            // 
            // txtLog
            // 
            txtLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtLog.Location = new Point(816, 176);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ScrollBars = ScrollBars.Both;
            txtLog.Size = new Size(383, 263);
            txtLog.TabIndex = 3;
            // 
            // txtLocation
            // 
            txtLocation.Location = new Point(859, 88);
            txtLocation.Margin = new Padding(3, 4, 3, 4);
            txtLocation.Name = "txtLocation";
            txtLocation.Size = new Size(281, 27);
            txtLocation.TabIndex = 4;
            txtLocation.Text = "Helsingborg";
            // 
            // numMaxAge
            // 
            numMaxAge.Location = new Point(859, 127);
            numMaxAge.Margin = new Padding(3, 4, 3, 4);
            numMaxAge.Name = "numMaxAge";
            numMaxAge.Size = new Size(67, 27);
            numMaxAge.TabIndex = 5;
            numMaxAge.Value = new decimal(new int[] { 25, 0, 0, 0 });
            // 
            // numMinAge
            // 
            numMinAge.Location = new Point(1001, 127);
            numMinAge.Margin = new Padding(3, 4, 3, 4);
            numMinAge.Name = "numMinAge";
            numMinAge.Size = new Size(67, 27);
            numMinAge.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(816, 129);
            label1.Name = "label1";
            label1.Size = new Size(40, 20);
            label1.TabIndex = 7;
            label1.Text = "Max:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(958, 129);
            label2.Name = "label2";
            label2.Size = new Size(37, 20);
            label2.TabIndex = 8;
            label2.Text = "Min:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(816, 92);
            label3.Name = "label3";
            label3.Size = new Size(37, 20);
            label3.TabIndex = 9;
            label3.Text = "City:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(816, 19);
            label4.Name = "label4";
            label4.Size = new Size(83, 20);
            label4.TabIndex = 10;
            label4.Text = "First Name:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(816, 55);
            label5.Name = "label5";
            label5.Size = new Size(79, 20);
            label5.TabIndex = 12;
            label5.Text = "Last name:";
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(894, 51);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(246, 27);
            txtLastName.TabIndex = 11;
            txtLastName.Text = "Danielsson";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1214, 451);
            Controls.Add(label5);
            Controls.Add(txtLastName);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(numMinAge);
            Controls.Add(numMaxAge);
            Controls.Add(txtLocation);
            Controls.Add(txtLog);
            Controls.Add(lstResults);
            Controls.Add(button1);
            Controls.Add(txtFirstName);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)numMaxAge).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinAge).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtFirstName;
        private Button button1;
        private ListBox lstResults;
        private TextBox txtLog;
        private TextBox txtLocation;
        private NumericUpDown numMaxAge;
        private NumericUpDown numMinAge;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtLastName;
    }
}
