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
        /// <param name="disposing">true om managed resources ska tas bort; annars false.</param>
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
        ///  Metod som krävs för Designer support - ändra inte innehållet i denna metod med kodredigeraren.
        /// </summary>
        private void InitializeComponent()
        {
            txtFirstName = new TextBox();
            button1 = new Button();
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
            dgvPeople = new DataGridView();
            progressBar = new ProgressBar();
            ((System.ComponentModel.ISupportInitialize)numMaxAge).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinAge).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPeople).BeginInit();
            SuspendLayout();
            // 
            // txtFirstName
            // 
            txtFirstName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtFirstName.Location = new Point(894, 15);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(246, 27);
            txtFirstName.TabIndex = 0;
            txtFirstName.Text = "William";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.Location = new Point(1115, 143);
            button1.Name = "button1";
            button1.Size = new Size(85, 28);
            button1.TabIndex = 1;
            button1.Text = "Sök";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnSearch_Click;
            // 
            // txtLog
            // 
            txtLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            txtLog.Location = new Point(816, 176);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ScrollBars = ScrollBars.Both;
            txtLog.Size = new Size(383, 237);
            txtLog.TabIndex = 3;
            // 
            // txtLocation
            // 
            txtLocation.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtLocation.Location = new Point(859, 88);
            txtLocation.Margin = new Padding(3, 4, 3, 4);
            txtLocation.Name = "txtLocation";
            txtLocation.Size = new Size(281, 27);
            txtLocation.TabIndex = 4;
            txtLocation.Text = "Helsingborg";
            // 
            // numMaxAge
            // 
            numMaxAge.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numMaxAge.Location = new Point(859, 127);
            numMaxAge.Margin = new Padding(3, 4, 3, 4);
            numMaxAge.Name = "numMaxAge";
            numMaxAge.Size = new Size(67, 27);
            numMaxAge.TabIndex = 5;
            numMaxAge.Value = new decimal(new int[] { 25, 0, 0, 0 });
            // 
            // numMinAge
            // 
            numMinAge.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numMinAge.Location = new Point(1001, 127);
            numMinAge.Margin = new Padding(3, 4, 3, 4);
            numMinAge.Name = "numMinAge";
            numMinAge.Size = new Size(67, 27);
            numMinAge.TabIndex = 6;
            numMinAge.Value = new decimal(new int[] { 15, 0, 0, 0 });
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(816, 129);
            label1.Name = "label1";
            label1.Size = new Size(40, 20);
            label1.TabIndex = 7;
            label1.Text = "Max:";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(958, 129);
            label2.Name = "label2";
            label2.Size = new Size(37, 20);
            label2.TabIndex = 8;
            label2.Text = "Min:";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(816, 92);
            label3.Name = "label3";
            label3.Size = new Size(37, 20);
            label3.TabIndex = 9;
            label3.Text = "City:";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(816, 19);
            label4.Name = "label4";
            label4.Size = new Size(83, 20);
            label4.TabIndex = 10;
            label4.Text = "First Name:";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(816, 55);
            label5.Name = "label5";
            label5.Size = new Size(79, 20);
            label5.TabIndex = 12;
            label5.Text = "Last name:";
            // 
            // txtLastName
            // 
            txtLastName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtLastName.Location = new Point(894, 51);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(246, 27);
            txtLastName.TabIndex = 11;
            txtLastName.Text = "Danielsson";
            // 
            // dgvPeople
            // 
            dgvPeople.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvPeople.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPeople.Location = new Point(10, 10);
            dgvPeople.Margin = new Padding(2);
            dgvPeople.Name = "dgvPeople";
            dgvPeople.RowHeadersWidth = 62;
            dgvPeople.Size = new Size(786, 403);
            dgvPeople.TabIndex = 13;
            // 
            // progressBar
            // 
            progressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressBar.Location = new Point(10, 418);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(1190, 29);
            progressBar.TabIndex = 14;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1214, 451);
            Controls.Add(progressBar);
            Controls.Add(dgvPeople);
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
            Controls.Add(button1);
            Controls.Add(txtFirstName);
            Name = "Form1";
            Text = "StalkerPro";
            ((System.ComponentModel.ISupportInitialize)numMaxAge).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinAge).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPeople).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.NumericUpDown numMaxAge;
        private System.Windows.Forms.NumericUpDown numMinAge;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.DataGridView dgvPeople;
        private ProgressBar progressBar;
    }
}
