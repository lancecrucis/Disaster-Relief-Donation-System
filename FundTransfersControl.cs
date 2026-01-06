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

namespace Main_Disaster_Relief_Donation_System
{
    public partial class FundTransfersControl : UserControl
    {
        string connectionString = "server=localhost;user=root;password=1Alance101;database=disaster_relief_system;";
        private int currentAdminId;
        private string currentRole;
        private string currentAdminUsername;

        public FundTransfersControl(int adminId, string role, string adminUsername)
        {
            InitializeComponent();
            this.currentAdminId = adminId;
            this.currentAdminUsername = adminUsername;
            this.currentRole = role.ToLower();

            LoadAvailableFunds();
            LoadAreas();
            LoadTransferHistory();
        }

        private void LoadAvailableFunds()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Get total completed donations
                    string queryTotal = @"SELECT COALESCE(SUM(amount), 0) 
                                         FROM institutional_donations 
                                         WHERE status = 'Completed'";
                    decimal totalDonations = 0;
                    using (MySqlCommand cmd = new MySqlCommand(queryTotal, conn))
                    {
                        totalDonations = Convert.ToDecimal(cmd.ExecuteScalar());
                    }

                    // Get total already transferred
                    string queryTransferred = @"SELECT COALESCE(SUM(amount), 0) 
                                               FROM fund_transfers";
                    decimal totalTransferred = 0;
                    using (MySqlCommand cmd = new MySqlCommand(queryTransferred, conn))
                    {
                        totalTransferred = Convert.ToDecimal(cmd.ExecuteScalar());
                    }

                    decimal availableFunds = totalDonations - totalTransferred;

                    // Display available funds (add a label in your form designer)
                    // lblAvailableFunds.Text = $"Available Funds: ₱{availableFunds:N0}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading available funds: " + ex.Message);
            }
        }

        private void LoadAreas()
        {
            cmbSelectArea.Items.Clear();

            try
            {
                cmbSelectArea.DisplayMember = "Display";
                cmbSelectArea.ValueMember = "Id";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT id, affected_location, 
                                           COALESCE(funds_needed, 0) AS funds_needed,
                                           COALESCE(funds_received, 0) AS funds_received
                                    FROM relief_requests
                                    WHERE status = 'Approved'";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            string location = reader["affected_location"] == DBNull.Value
                                ? "(no location)"
                                : reader["affected_location"].ToString();
                            decimal needed = Convert.ToDecimal(reader["funds_needed"]);
                            decimal received = Convert.ToDecimal(reader["funds_received"]);
                            decimal remaining = needed - received;
                            if (remaining < 0) remaining = 0m;

                            string display = $"{location} (Needed: ₱{remaining:N0})";

                            cmbSelectArea.Items.Add(new AreaItem
                            {
                                Id = id,
                                Display = display,
                                Remaining = remaining
                            });
                        }
                    }
                }

                if (cmbSelectArea.Items.Count > 0)
                    cmbSelectArea.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading areas: " + ex.Message);
            }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (cmbSelectArea.SelectedItem == null)
            {
                MessageBox.Show("Please select an affected area.");
                return;
            }

            decimal amount = numAmount.Value;
            if (amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount.");
                return;
            }

            AreaItem selected = (AreaItem)cmbSelectArea.SelectedItem;

            // Check if amount exceeds what's needed for the area
            if (amount > selected.Remaining)
            {
                MessageBox.Show($"Amount exceeds funds needed. Only ₱{selected.Remaining:N0} is needed for this area.",
                    "Amount Too High", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if enough funds are available from donations
            decimal availableFunds = GetAvailableFunds();
            if (amount > availableFunds)
            {
                MessageBox.Show($"Insufficient donation funds. Only ₱{availableFunds:N0} available for transfer.",
                    "Insufficient Funds", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Transfer ₱{amount:N0} to {selected.Display.Split('(')[0].Trim()}?\n\n" +
                $"This will deduct from institutional donations and add to the area's received funds.\n" +
                $"This action cannot be undone.",
                "Confirm Transfer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        using (MySqlTransaction tx = conn.BeginTransaction())
                        {
                            // 1. Record the transfer in fund_transfers table
                            string insertQuery = @"INSERT INTO fund_transfers 
                                                  (request_id, amount, transferred_by) 
                                                  VALUES (@req_id, @amount, @admin_id)";
                            using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn, tx))
                            {
                                cmd.Parameters.AddWithValue("@req_id", selected.Id);
                                cmd.Parameters.AddWithValue("@amount", amount);
                                cmd.Parameters.AddWithValue("@admin_id", currentAdminId);
                                cmd.ExecuteNonQuery();
                            }

                            // 2. Update funds_received in relief_requests (this updates the progress bar)
                            string updateRequestQuery = @"UPDATE relief_requests 
                                                         SET funds_received = COALESCE(funds_received, 0) + @amount 
                                                         WHERE id = @req_id";
                            using (MySqlCommand cmd = new MySqlCommand(updateRequestQuery, conn, tx))
                            {
                                cmd.Parameters.AddWithValue("@amount", amount);
                                cmd.Parameters.AddWithValue("@req_id", selected.Id);
                                cmd.ExecuteNonQuery();
                            }

                            tx.Commit();
                        }
                    }

                    MessageBox.Show($"₱{amount:N0} successfully transferred to {selected.Display.Split('(')[0].Trim()}!",
                        "Transfer Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reset and refresh
                    numAmount.Value = numAmount.Minimum;
                    LoadAvailableFunds();
                    LoadAreas();
                    LoadTransferHistory();

                    // Refresh other panels
                    RefreshParentPanels();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error processing transfer: " + ex.Message);
                }
            }
        }

        private decimal GetAvailableFunds()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Total ALL donations (not just completed - for testing)
                    string queryTotal = @"SELECT COALESCE(SUM(amount), 0) 
                                         FROM institutional_donations";
                    decimal totalDonations = 0;
                    using (MySqlCommand cmd = new MySqlCommand(queryTotal, conn))
                    {
                        totalDonations = Convert.ToDecimal(cmd.ExecuteScalar());
                    }

                    // Total transferred
                    string queryTransferred = @"SELECT COALESCE(SUM(amount), 0) 
                                               FROM fund_transfers";
                    decimal totalTransferred = 0;
                    using (MySqlCommand cmd = new MySqlCommand(queryTransferred, conn))
                    {
                        totalTransferred = Convert.ToDecimal(cmd.ExecuteScalar());
                    }

                    decimal available = totalDonations - totalTransferred;

                    // DEBUG: Show what we found
                    MessageBox.Show($"Total Donations: ₱{totalDonations:N0}\n" +
                                  $"Total Transferred: ₱{totalTransferred:N0}\n" +
                                  $"Available: ₱{available:N0}",
                                  "Debug Info");

                    return available;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in GetAvailableFunds: " + ex.Message);
                return 0;
            }
        }

        private void LoadTransferHistory()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT 
                        ft.transferred_at, 
                        rr.affected_location AS area,
                        ft.amount,
                        u.username AS admin_name
                        FROM fund_transfers ft
                        JOIN relief_requests rr ON ft.request_id = rr.id
                        JOIN users u ON ft.transferred_by = u.id
                        ORDER BY ft.transferred_at DESC";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvHistory.DataSource = dt;

                    // Format columns
                    if (dgvHistory.Columns.Count > 0)
                    {
                        if (dgvHistory.Columns.Contains("transferred_at"))
                        {
                            dgvHistory.Columns["transferred_at"].HeaderText = "Date & Time";
                            dgvHistory.Columns["transferred_at"].DefaultCellStyle.Format = "MMM d, yyyy h:mm tt";
                        }
                        if (dgvHistory.Columns.Contains("area"))
                            dgvHistory.Columns["area"].HeaderText = "Recipient Area";
                        if (dgvHistory.Columns.Contains("amount"))
                        {
                            dgvHistory.Columns["amount"].HeaderText = "Amount";
                            dgvHistory.Columns["amount"].DefaultCellStyle.Format = "₱#,##0";
                        }
                        if (dgvHistory.Columns.Contains("admin_name"))
                            dgvHistory.Columns["admin_name"].HeaderText = "Transferred By";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading history: " + ex.Message);
            }
        }

        private class AreaItem
        {
            public int Id { get; set; }
            public string Display { get; set; }
            public decimal Remaining { get; set; }
            public override string ToString() => Display;
        }

        private void FundTransfersControl_Load(object sender, EventArgs e)
        {
        }

        private void btnTransfer_Click_1(object sender, EventArgs e)
        {
            btnTransfer_Click(sender, e);
        }

        private void RefreshParentPanels()
        {
            Form main = this.FindForm();
            if (main == null) return;

            foreach (Control c in main.Controls)
            {
                if (c is affectedArea aa)
                {
                    try { aa.RefreshData(); } catch { }
                }
                if (c is InstitutionalDonationsControl idc)
                {
                    try { idc.RefreshData(); } catch { }
                }
                if (c is adminDashboard dash)
                {
                    try { dash.RefreshDashboard(); } catch { }
                }
            }
        }
    }
}