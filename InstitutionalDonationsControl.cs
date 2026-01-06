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
    public partial class InstitutionalDonationsControl : UserControl
    {
        string connectionString = "server=localhost;user=root;password=1Alance101;database=disaster_relief_system;";
        public InstitutionalDonationsControl()
        {
            InitializeComponent();
            LoadDonations();
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            SubmitInstitutionalDonationForm form = new SubmitInstitutionalDonationForm();
            form.ShowDialog();
            LoadDonations(); // Refresh after submit
        }

        // Designer might be wired to this method; forward it to the implemented handler.
        private void btnSubmit_Click_1(object sender, EventArgs e)
        {
            btnSubmit_Click(sender, e);
        }

        private void LoadDonations()
        {
            LoadStats();
            LoadGrid();
        }

        private void LoadStats()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Total
                    string qTotal = "SELECT COALESCE(SUM(amount),0), COUNT(*) FROM institutional_donations";
                    using (MySqlCommand cmd = new MySqlCommand(qTotal, conn))
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            decimal totalAmt = r.GetDecimal(0);
                            int totalCount = r.GetInt32(1);
                            lblTotalAmount.Text = $"₱{totalAmt:N0}";
                            lblTotalCount.Text = $"{totalCount} donations";
                        }
                    }

                    // Completed
                    string qComp = "SELECT COALESCE(SUM(amount),0), COUNT(*) FROM institutional_donations WHERE status = 'Completed'";
                    using (MySqlCommand cmd = new MySqlCommand(qComp, conn))
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            decimal amt = r.GetDecimal(0);
                            int count = r.GetInt32(1);
                            lblCompletedAmount.Text = $"₱{amt:N0}";
                            lblCompletedCount.Text = $"{count} donations";
                        }
                    }

                    // Processing
                    string qProc = "SELECT COALESCE(SUM(amount),0), COUNT(*) FROM institutional_donations WHERE status = 'Processing'";
                    using (MySqlCommand cmd = new MySqlCommand(qProc, conn))
                    using (MySqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            decimal amt = r.GetDecimal(0);
                            int count = r.GetInt32(1);
                            lblProcessingAmount.Text = $"₱{amt:N0}";
                            lblProcessingCount.Text = $"{count} donations";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading stats: " + ex.Message);
            }
        }

        private void LoadGrid()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT 
                        organization_name, organization_type, representative_name, position_title,
                        email, amount, transfer_method, status, donation_date
                        FROM institutional_donations
                        ORDER BY donation_date DESC";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvDonations.DataSource = dt;

                    // Customize columns
                    if (dgvDonations.Columns.Count > 0)
                    {
                        dgvDonations.Columns["organization_name"].HeaderText = "Organization";
                        dgvDonations.Columns["organization_type"].HeaderText = "Type";
                        dgvDonations.Columns["amount"].HeaderText = "Amount";
                        dgvDonations.Columns["amount"].DefaultCellStyle.Format = "₱#,##0.00";
                        dgvDonations.Columns["donation_date"].HeaderText = "Date";
                        dgvDonations.Columns["donation_date"].DefaultCellStyle.Format = "MMM d, yyyy";
                        dgvDonations.Columns["status"].HeaderText = "Status";

                        // Hide unnecessary columns
                        dgvDonations.Columns["representative_name"].Visible = false;
                        dgvDonations.Columns["position_title"].Visible = false;
                        dgvDonations.Columns["email"].Visible = false;
                        dgvDonations.Columns["transfer_method"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading donations: " + ex.Message);
            }
        }

        private void dgvDonations_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvDonations.Columns[e.ColumnIndex].Name == "organization_type")
            {
                if (e.Value != null)
                {
                    string type = e.Value.ToString();
                    DataGridViewCell cell = dgvDonations.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.Style.Padding = new Padding(10, 5, 10, 5);
                    cell.Style.ForeColor = Color.White;
                    cell.Style.Font = new Font("Segoe UI", 8F, FontStyle.Bold);

                    switch (type)
                    {
                        case "LGU": cell.Style.BackColor = Color.FromArgb(0, 123, 255); break;
                        case "NGO": cell.Style.BackColor = Color.FromArgb(220, 53, 69); break;
                        case "Barangay": cell.Style.BackColor = Color.FromArgb(255, 193, 7); break;
                        case "School/University": cell.Style.BackColor = Color.FromArgb(40, 167, 69); break;
                        case "National Government Agency": cell.Style.BackColor = Color.FromArgb(108, 117, 125); break;
                        default: cell.Style.BackColor = Color.Gray; break;
                    }
                }
            }

            if (dgvDonations.Columns[e.ColumnIndex].Name == "status")
            {
                if (e.Value != null)
                {
                    string status = e.Value.ToString();
                    DataGridViewCell cell = dgvDonations.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.Style.Padding = new Padding(10, 5, 10, 5);
                    cell.Style.ForeColor = Color.White;
                    cell.Style.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                    cell.Style.BackColor = status == "Completed" ? Color.FromArgb(40, 167, 69) : Color.Orange;
                }
            }
        }

        private void dgvDonations_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: Add "Receipt" button column later
        }

        private void InstitutionalDonationsControl_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgvDonations_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
        // Add this method to InstitutionalDonationsControl to resolve CS1061
        public void RefreshData()
        {
            // Refresh the donations grid and stats
            LoadDonations();
            LoadStats();
        }
    }

}
