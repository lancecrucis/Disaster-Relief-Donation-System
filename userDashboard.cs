using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Main_Disaster_Relief_Donation_System
{
    public partial class userDashboard : UserControl
    {
        string connectionString = "server=localhost;user=root;password=1Alance101;database=disaster_relief_system;";

        public userDashboard()
        {
            InitializeComponent();
            LoadDashboardStats();
        }

        public void RefreshDashboard()
        {
            LoadDashboardStats();
        }

        // Reduced to only the two values you requested:
        // - Total Donations (Completed institutional donations)
        // - Total People Affected (Approved requests)
        private void LoadDashboardStats()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Total Donations (Completed institutional donations)
                    string queryDonations = @"SELECT COALESCE(SUM(amount), 0) 
                                              FROM institutional_donations 
                                              WHERE status = 'Completed'";
                    decimal totalDonations = 0m;
                    using (MySqlCommand cmd = new MySqlCommand(queryDonations, conn))
                    {
                        totalDonations = Convert.ToDecimal(cmd.ExecuteScalar());
                    }

                    // Total People Affected (Approved requests)
                    string queryPeople = @"SELECT COALESCE(SUM(CAST(people_affected AS UNSIGNED)), 0) 
                                          FROM relief_requests 
                                          WHERE status = 'Approved'";
                    long totalPeople = 0L;
                    using (MySqlCommand cmd = new MySqlCommand(queryPeople, conn))
                    {
                        totalPeople = Convert.ToInt64(cmd.ExecuteScalar());
                    }

                    // Update the two labels. Ensure these names match your Designer labels.
                    label6.Text =$"₱{totalDonations:N0}";
                    label3.Text = totalPeople.ToString("N0");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading dashboard statistics: " + ex.Message,
                    "Dashboard Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Simple helper to find a label by name (searches nested controls)
        

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}