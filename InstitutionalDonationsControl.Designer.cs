namespace Main_Disaster_Relief_Donation_System
{
    partial class InstitutionalDonationsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSubmit = new System.Windows.Forms.Button();
            this.dgvDonations = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblCompletedCount = new System.Windows.Forms.Label();
            this.lblCompletedAmount = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblProcessingCount = new System.Windows.Forms.Label();
            this.lblProcessingAmount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonations)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(865, 46);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(131, 39);
            this.btnSubmit.TabIndex = 0;
            this.btnSubmit.Text = "Donate";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click_1);
            // 
            // dgvDonations
            // 
            this.dgvDonations.AllowUserToAddRows = false;
            this.dgvDonations.AllowUserToDeleteRows = false;
            this.dgvDonations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDonations.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvDonations.BackgroundColor = System.Drawing.Color.White;
            this.dgvDonations.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDonations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDonations.Location = new System.Drawing.Point(35, 263);
            this.dgvDonations.Name = "dgvDonations";
            this.dgvDonations.ReadOnly = true;
            this.dgvDonations.Size = new System.Drawing.Size(961, 361);
            this.dgvDonations.TabIndex = 1;
            this.dgvDonations.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDonations_CellContentClick_1);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblTotalCount);
            this.panel1.Controls.Add(this.lblTotalAmount);
            this.panel1.Location = new System.Drawing.Point(35, 106);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(299, 139);
            this.panel1.TabIndex = 2;
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCount.Location = new System.Drawing.Point(19, 110);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(71, 17);
            this.lblTotalCount.TabIndex = 0;
            this.lblTotalCount.Text = "6 donation";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.ForeColor = System.Drawing.Color.Red;
            this.lblTotalAmount.Location = new System.Drawing.Point(18, 78);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(37, 21);
            this.lblTotalAmount.TabIndex = 0;
            this.lblTotalAmount.Text = "600";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lblCompletedCount);
            this.panel2.Controls.Add(this.lblCompletedAmount);
            this.panel2.Location = new System.Drawing.Point(367, 106);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(299, 139);
            this.panel2.TabIndex = 3;
            // 
            // lblCompletedCount
            // 
            this.lblCompletedCount.AutoSize = true;
            this.lblCompletedCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompletedCount.Location = new System.Drawing.Point(25, 110);
            this.lblCompletedCount.Name = "lblCompletedCount";
            this.lblCompletedCount.Size = new System.Drawing.Size(77, 17);
            this.lblCompletedCount.TabIndex = 1;
            this.lblCompletedCount.Text = "4 donations";
            this.lblCompletedCount.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblCompletedAmount
            // 
            this.lblCompletedAmount.AutoSize = true;
            this.lblCompletedAmount.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompletedAmount.ForeColor = System.Drawing.Color.Green;
            this.lblCompletedAmount.Location = new System.Drawing.Point(24, 78);
            this.lblCompletedAmount.Name = "lblCompletedAmount";
            this.lblCompletedAmount.Size = new System.Drawing.Size(37, 21);
            this.lblCompletedAmount.TabIndex = 0;
            this.lblCompletedAmount.Text = "290";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.lblProcessingCount);
            this.panel3.Controls.Add(this.lblProcessingAmount);
            this.panel3.Location = new System.Drawing.Point(697, 106);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(299, 139);
            this.panel3.TabIndex = 3;
            // 
            // lblProcessingCount
            // 
            this.lblProcessingCount.AutoSize = true;
            this.lblProcessingCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessingCount.Location = new System.Drawing.Point(24, 110);
            this.lblProcessingCount.Name = "lblProcessingCount";
            this.lblProcessingCount.Size = new System.Drawing.Size(77, 17);
            this.lblProcessingCount.TabIndex = 3;
            this.lblProcessingCount.Text = "2 donations";
            // 
            // lblProcessingAmount
            // 
            this.lblProcessingAmount.AutoSize = true;
            this.lblProcessingAmount.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessingAmount.ForeColor = System.Drawing.Color.Orange;
            this.lblProcessingAmount.Location = new System.Drawing.Point(23, 78);
            this.lblProcessingAmount.Name = "lblProcessingAmount";
            this.lblProcessingAmount.Size = new System.Drawing.Size(43, 21);
            this.lblProcessingAmount.TabIndex = 2;
            this.lblProcessingAmount.Text = "1200";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 37);
            this.label1.TabIndex = 4;
            this.label1.Text = "Donation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label2.Location = new System.Drawing.Point(19, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Total Donations";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label3.Location = new System.Drawing.Point(20, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Completed";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label4.Location = new System.Drawing.Point(23, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Processing";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label8.Location = new System.Drawing.Point(31, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(410, 20);
            this.label8.TabIndex = 27;
            this.label8.Text = "Make monetary donations to support disaster-affected areas\n";
            // 
            // InstitutionalDonationsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvDonations);
            this.Controls.Add(this.btnSubmit);
            this.Name = "InstitutionalDonationsControl";
            this.Size = new System.Drawing.Size(1057, 657);
            this.Load += new System.EventHandler(this.InstitutionalDonationsControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonations)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.DataGridView dgvDonations;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblCompletedAmount;
        private System.Windows.Forms.Label lblCompletedCount;
        private System.Windows.Forms.Label lblProcessingCount;
        private System.Windows.Forms.Label lblProcessingAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
    }
}
