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
            ((System.ComponentModel.ISupportInitialize)numMaxAge).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMinAge).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPeople).BeginInit();
            SuspendLayout();
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(1118, 19);
            txtFirstName.Margin = new Padding(4, 4, 4, 4);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(306, 31);
            txtFirstName.TabIndex = 0;
            txtFirstName.Text = "William";
            // 
            // button1
            // 
            button1.Location = new Point(1394, 179);
            button1.Margin = new Padding(4, 4, 4, 4);
            button1.Name = "button1";
            button1.Size = new Size(106, 35);
            button1.TabIndex = 1;
            button1.Text = "Sök";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnSearch_Click;
            // 
            // txtLog
            // 
            txtLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtLog.Location = new Point(1020, 220);
            txtLog.Margin = new Padding(4, 4, 4, 4);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ScrollBars = ScrollBars.Both;
            txtLog.Size = new Size(478, 328);
            txtLog.TabIndex = 3;
            // 
            // txtLocation
            // 
            txtLocation.Location = new Point(1074, 110);
            txtLocation.Margin = new Padding(4, 5, 4, 5);
            txtLocation.Name = "txtLocation";
            txtLocation.Size = new Size(350, 31);
            txtLocation.TabIndex = 4;
            txtLocation.Text = "Helsingborg";
            // 
            // numMaxAge
            // 
            numMaxAge.Location = new Point(1074, 159);
            numMaxAge.Margin = new Padding(4, 5, 4, 5);
            numMaxAge.Name = "numMaxAge";
            numMaxAge.Size = new Size(84, 31);
            numMaxAge.TabIndex = 5;
            numMaxAge.Value = new decimal(new int[] { 25, 0, 0, 0 });
            // 
            // numMinAge
            // 
            numMinAge.Location = new Point(1251, 159);
            numMinAge.Margin = new Padding(4, 5, 4, 5);
            numMinAge.Name = "numMinAge";
            numMinAge.Size = new Size(84, 31);
            numMinAge.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1020, 161);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(49, 25);
            label1.TabIndex = 7;
            label1.Text = "Max:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1198, 161);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(46, 25);
            label2.TabIndex = 8;
            label2.Text = "Min:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(1020, 115);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(46, 25);
            label3.TabIndex = 9;
            label3.Text = "City:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1020, 24);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(101, 25);
            label4.TabIndex = 10;
            label4.Text = "First Name:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(1020, 69);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(96, 25);
            label5.TabIndex = 12;
            label5.Text = "Last name:";
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(1118, 64);
            txtLastName.Margin = new Padding(4, 4, 4, 4);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(306, 31);
            txtLastName.TabIndex = 11;
            txtLastName.Text = "Danielsson";
            // 
            // dgvPeople
            // 
            dgvPeople.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPeople.Location = new Point(12, 12);
            dgvPeople.Name = "dgvPeople";
            dgvPeople.RowHeadersWidth = 62;
            dgvPeople.Size = new Size(982, 536);
            dgvPeople.TabIndex = 13;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1518, 564);
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
            Margin = new Padding(4, 4, 4, 4);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)numMaxAge).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMinAge).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPeople).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtFirstName;
        private Button button1;
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
        private DataGridView dgvPeople;
    }
}
