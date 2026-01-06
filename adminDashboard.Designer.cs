namespace Main_Disaster_Relief_Donation_System
{
    partial class adminDashboard
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblTotalDonated = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblPendingRequests = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTotalAffected = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTotalDonations = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chartStats = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartAffectedPie = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartStats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAffectedPie)).BeginInit();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lblTotalDonated);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Location = new System.Drawing.Point(782, 37);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(233, 135);
            this.panel4.TabIndex = 9;
            // 
            // lblTotalDonated
            // 
            this.lblTotalDonated.AutoSize = true;
            this.lblTotalDonated.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalDonated.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(205)))), ((int)(((byte)(78)))));
            this.lblTotalDonated.Location = new System.Drawing.Point(25, 87);
            this.lblTotalDonated.Name = "lblTotalDonated";
            this.lblTotalDonated.Size = new System.Drawing.Size(117, 32);
            this.lblTotalDonated.TabIndex = 1;
            this.lblTotalDonated.Text = "₱ 129,200";
            this.lblTotalDonated.Click += new System.EventHandler(this.lblIndivTotal_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(13, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 21);
            this.label9.TabIndex = 0;
            this.label9.Text = "Total Donated";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lblPendingRequests);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(529, 37);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(233, 135);
            this.panel3.TabIndex = 10;
            // 
            // lblPendingRequests
            // 
            this.lblPendingRequests.AutoSize = true;
            this.lblPendingRequests.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendingRequests.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(200)))), ((int)(((byte)(55)))));
            this.lblPendingRequests.Location = new System.Drawing.Point(25, 87);
            this.lblPendingRequests.Name = "lblPendingRequests";
            this.lblPendingRequests.Size = new System.Drawing.Size(126, 32);
            this.lblPendingRequests.TabIndex = 1;
            this.lblPendingRequests.Text = "12 request";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(13, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 21);
            this.label7.TabIndex = 0;
            this.label7.Text = "Pending Requests";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblTotalAffected);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(275, 37);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(233, 135);
            this.panel2.TabIndex = 11;
            // 
            // lblTotalAffected
            // 
            this.lblTotalAffected.AutoSize = true;
            this.lblTotalAffected.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAffected.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(0)))), ((int)(((byte)(7)))));
            this.lblTotalAffected.Location = new System.Drawing.Point(25, 87);
            this.lblTotalAffected.Name = "lblTotalAffected";
            this.lblTotalAffected.Size = new System.Drawing.Size(93, 32);
            this.lblTotalAffected.TabIndex = 1;
            this.lblTotalAffected.Text = "4 areas";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "Total Affected";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblTotalDonations);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(22, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(233, 135);
            this.panel1.TabIndex = 8;
            // 
            // lblTotalDonations
            // 
            this.lblTotalDonations.AutoSize = true;
            this.lblTotalDonations.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalDonations.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(37)))));
            this.lblTotalDonations.Location = new System.Drawing.Point(25, 87);
            this.lblTotalDonations.Name = "lblTotalDonations";
            this.lblTotalDonations.Size = new System.Drawing.Size(136, 32);
            this.lblTotalDonations.TabIndex = 1;
            this.lblTotalDonations.Text = "₱ 1,000,000";
            this.lblTotalDonations.Click += new System.EventHandler(this.lblTotalDonations_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Total Donations";
            // 
            // chartStats
            // 
            this.chartStats.BorderlineColor = System.Drawing.Color.Black;
            this.chartStats.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.Name = "ChartArea1";
            this.chartStats.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartStats.Legends.Add(legend1);
            this.chartStats.Location = new System.Drawing.Point(529, 221);
            this.chartStats.Name = "chartStats";
            this.chartStats.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Grayscale;
            this.chartStats.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartStats.Series.Add(series1);
            this.chartStats.Size = new System.Drawing.Size(486, 377);
            this.chartStats.TabIndex = 12;
            this.chartStats.Text = "chartStats";
            this.chartStats.Click += new System.EventHandler(this.chart1_Click);
            // 
            // chartAffectedPie
            // 
            this.chartAffectedPie.BorderlineColor = System.Drawing.Color.Black;
            this.chartAffectedPie.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea2.Name = "ChartArea1";
            this.chartAffectedPie.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartAffectedPie.Legends.Add(legend2);
            this.chartAffectedPie.Location = new System.Drawing.Point(22, 221);
            this.chartAffectedPie.Name = "chartAffectedPie";
            this.chartAffectedPie.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Grayscale;
            this.chartAffectedPie.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartAffectedPie.Series.Add(series2);
            this.chartAffectedPie.Size = new System.Drawing.Size(486, 377);
            this.chartAffectedPie.TabIndex = 13;
            this.chartAffectedPie.Text = "chart1";
            this.chartAffectedPie.Click += new System.EventHandler(this.chart1_Click_1);
            // 
            // adminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.Controls.Add(this.chartAffectedPie);
            this.Controls.Add(this.chartStats);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "adminDashboard";
            this.Size = new System.Drawing.Size(1057, 657);
            this.Load += new System.EventHandler(this.adminDashboard_Load);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartStats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAffectedPie)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblTotalDonated;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblPendingRequests;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTotalAffected;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTotalDonations;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartStats;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAffectedPie;
    }
}
