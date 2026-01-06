using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Windows.Forms.DataVisualization.Charting;

namespace Main_Disaster_Relief_Donation_System
{
    public partial class adminDashboard : UserControl
    {
        string connectionString = "server=localhost;user=root;password=1Alance101;database=disaster_relief_system;";

        public adminDashboard()
        {
            InitializeComponent();
            LoadDashboardStats();
        }

        private void adminDashboard_Load(object sender, EventArgs e)
        {
            LoadDashboardStats();
        }

        public void RefreshDashboard()
        {
            LoadDashboardStats();
        }

        private void LoadDashboardStats()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    decimal totalInstitutional = 0;
                    int totalAffected = 0;
                    int pendingRequests = 0;
                    decimal totalDonated = 0;
                    int activeAreas = 0;  // New variable for Active Areas

                    // 1. Total Institutional Donations (Completed only)
                    using (MySqlCommand cmd = new MySqlCommand("SELECT COALESCE(SUM(amount), 0) FROM institutional_donations WHERE status = 'Completed'", conn))
                        totalInstitutional = Convert.ToDecimal(cmd.ExecuteScalar());

                    // 2. Total Affected Areas (Approved requests)
                    using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM relief_requests WHERE status = 'Approved'", conn))
                        totalAffected = Convert.ToInt32(cmd.ExecuteScalar());

                    // 3. Pending Requests
                    using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM relief_requests WHERE status = 'Pending'", conn))
                        pendingRequests = Convert.ToInt32(cmd.ExecuteScalar());

                    // 4. Total Donated/Transferred to Affected Areas
                    using (MySqlCommand cmd = new MySqlCommand("SELECT COALESCE(SUM(amount), 0) FROM fund_transfers", conn))
                        totalDonated = Convert.ToDecimal(cmd.ExecuteScalar());

                    // 5. Active Areas — use same logic as affectedArea.CreateAreaCard:
                    //    Approved requests where funds_received < funds_needed (NULLs handled via COALESCE)
                    using (MySqlCommand cmd = new MySqlCommand(
                        "SELECT COUNT(*) FROM relief_requests WHERE status = 'Approved' AND COALESCE(funds_received, 0) < COALESCE(funds_needed, 0)",
                        conn))
                    {
                        activeAreas = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // Update labels
                    lblTotalDonations.Text = $"₱{totalInstitutional:N0}";
                    lblTotalAffected.Text = totalAffected.ToString();
                    lblPendingRequests.Text = pendingRequests.ToString();
                    lblTotalDonated.Text = $"₱{totalDonated:N0}";

                    // === EXISTING BAR CHART: Keep as-is ===
                    chartStats.Series.Clear();
                    chartStats.ChartAreas.Clear();
                    chartStats.Titles.Clear();

                    chartStats.Titles.Add("Admin Dashboard Overview");
                    chartStats.Titles[0].Font = new Font("Segoe UI", 14F, FontStyle.Bold);
                    chartStats.Titles[0].ForeColor = Color.FromArgb(33, 37, 41);

                    ChartArea chartArea = new ChartArea();
                    chartStats.ChartAreas.Add(chartArea);

                    Series series = new Series("Statistics");
                    series.ChartType = SeriesChartType.Column;
                    series.IsValueShownAsLabel = true;

                    series.Points.AddXY("Total Donations", totalInstitutional);
                    series.Points.AddXY("Funds Transferred", totalDonated);
                    series.Points.AddXY("Affected Areas", totalAffected);
                    series.Points.AddXY("Pending Requests", pendingRequests);

                    series.Points[0].Color = Color.FromArgb(0, 122, 37);
                    series.Points[1].Color = Color.FromArgb(0, 205, 78);
                    series.Points[2].Color = Color.FromArgb(219, 0, 7);
                    series.Points[3].Color = Color.FromArgb(255, 200, 55);

                    chartStats.Series.Add(series);

                    chartArea.AxisX.LabelStyle.Font = new Font("Segoe UI", 10F);
                    chartArea.AxisY.LabelStyle.Font = new Font("Segoe UI", 9F);
                    chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
                    chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;

                    // === PIE CHART: Total Areas and Active Areas ===
                    chartAffectedPie.Series.Clear();
                    chartAffectedPie.ChartAreas.Clear();
                    chartAffectedPie.Titles.Clear();

                    chartAffectedPie.Titles.Add("Affected Areas Chart");
                    chartAffectedPie.Titles[0].Font = new Font("Segoe UI", 14F, FontStyle.Bold);
                    chartAffectedPie.Titles[0].ForeColor = Color.FromArgb(33, 37, 41);

                    ChartArea pieArea = new ChartArea();
                    chartAffectedPie.ChartAreas.Add(pieArea);

                    Series pieSeries = new Series("Affected Areas");
                    pieSeries.ChartType = SeriesChartType.Pie;
                    pieSeries.IsValueShownAsLabel = true;
                    pieSeries["PieLabelStyle"] = "Outside";
                    pieSeries["PieLineColor"] = "Black";

                    pieSeries.Points.AddXY("Total Areas", totalAffected);
                    pieSeries.Points.AddXY("Active Areas", activeAreas);

                    // Colors (order: Total, Active)
                    pieSeries.Points[0].Color = Color.FromArgb(135, 15, 15);
                    pieSeries.Points[1].Color = Color.FromArgb(219, 0, 7);

                    chartAffectedPie.Series.Add(pieSeries);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading dashboard: " + ex.Message);
            }
        }

        private void lblIndivTotal_Click(object sender, EventArgs e)
        {
        }

        private void lblTotalDonations_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click_1(object sender, EventArgs e)
        {

        }
    }
}