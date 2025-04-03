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
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.numMaxAge = new System.Windows.Forms.NumericUpDown();
            this.numMinAge = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.dgvPeople = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxAge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinAge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeople)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(1118, 19);
            this.txtFirstName.Margin = new System.Windows.Forms.Padding(4);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(306, 31);
            this.txtFirstName.TabIndex = 0;
            this.txtFirstName.Text = "William";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1394, 179);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 35);
            this.button1.TabIndex = 1;
            this.button1.Text = "Sök";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                    | System.Windows.Forms.AnchorStyles.Left)
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(1020, 220);
            this.txtLog.Margin = new System.Windows.Forms.Padding(4);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(478, 328);
            this.txtLog.TabIndex = 3;
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(1074, 110);
            this.txtLocation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(350, 31);
            this.txtLocation.TabIndex = 4;
            this.txtLocation.Text = "Helsingborg";
            // 
            // numMaxAge
            // 
            this.numMaxAge.Location = new System.Drawing.Point(1074, 159);
            this.numMaxAge.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numMaxAge.Name = "numMaxAge";
            this.numMaxAge.Size = new System.Drawing.Size(84, 31);
            this.numMaxAge.TabIndex = 5;
            this.numMaxAge.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // numMinAge
            // 
            this.numMinAge.Location = new System.Drawing.Point(1251, 159);
            this.numMinAge.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numMinAge.Name = "numMinAge";
            this.numMinAge.Size = new System.Drawing.Size(84, 31);
            this.numMinAge.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1020, 161);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Max:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1198, 161);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Min:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1020, 115);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "City:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1020, 24);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 25);
            this.label4.TabIndex = 10;
            this.label4.Text = "First Name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1020, 69);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 25);
            this.label5.TabIndex = 12;
            this.label5.Text = "Last name:";
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(1118, 64);
            this.txtLastName.Margin = new System.Windows.Forms.Padding(4);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(306, 31);
            this.txtLastName.TabIndex = 11;
            this.txtLastName.Text = "Danielsson";
            // 
            // dgvPeople
            // 
            this.dgvPeople.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPeople.Location = new System.Drawing.Point(12, 12);
            this.dgvPeople.Name = "dgvPeople";
            this.dgvPeople.RowHeadersWidth = 62;
            this.dgvPeople.Size = new System.Drawing.Size(982, 536);
            this.dgvPeople.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1518, 564);
            this.Controls.Add(this.dgvPeople);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numMinAge);
            this.Controls.Add(this.numMaxAge);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtFirstName);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "StalkerPro";
            ((System.ComponentModel.ISupportInitialize)(this.numMaxAge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinAge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeople)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
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
    }
}
