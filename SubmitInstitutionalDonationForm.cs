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
    public partial class SubmitInstitutionalDonationForm : Form
    {
        string connectionString = "server=localhost;user=root;password=1Alance101;database=disaster_relief_system;";

        public SubmitInstitutionalDonationForm()
        {
            InitializeComponent();
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txtOrgName.Text) ||
                cmbOrgType.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtRepName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtContact.Text) ||
                numAmount.Value <= 0 ||
                cmbTransfer.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in all required fields.", "Missing Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int donationId = 0;
                string transferMethod = cmbTransfer.SelectedItem.ToString();

                // Insert donation with "Processing" status
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO institutional_donations 
                        (organization_name, organization_type, representative_name, position_title,
                         email, contact_number, amount, transfer_method, status)
                        VALUES 
                        (@org_name, @org_type, @rep_name, @position,
                         @email, @contact, @amount, @transfer, 'Processing')";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@org_name", txtOrgName.Text.Trim());
                        cmd.Parameters.AddWithValue("@org_type", cmbOrgType.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@rep_name", txtRepName.Text.Trim());
                        cmd.Parameters.AddWithValue("@position", txtPosition.Text.Trim());
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@contact", txtContact.Text.Trim());
                        cmd.Parameters.AddWithValue("@amount", numAmount.Value);
                        cmd.Parameters.AddWithValue("@transfer", transferMethod);
                        cmd.ExecuteNonQuery();

                        // Get the inserted donation ID
                        donationId = (int)cmd.LastInsertedId;
                    }
                }

                // Determine processing time based on transfer method
                int processingSeconds = GetProcessingTime(transferMethod);

                MessageBox.Show($"Institutional donation submitted successfully!\n\n" +
                              $"Transfer Method: {transferMethod}\n" +
                              $"Processing Time: {processingSeconds} seconds\n\n" +
                              $"Your donation is being processed and will be marked as Completed automatically.",
                    "Thank You!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();

                // Process the donation asynchronously in the background
                await ProcessDonationAsync(donationId, processingSeconds);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting donation: " + ex.Message);
            }
        }

        private int GetProcessingTime(string transferMethod)
        {
            switch (transferMethod)
            {
                case "Bank Transfer":
                    return 5;  // 5 seconds
                case "Government Check":
                    return 10; // 10 seconds
                case "Wire Transfer":
                    return 12; // 12 seconds
                case "Cash Deposit":
                    return 3;  // 3 seconds (bonus)
                default:
                    return 5;  // default 5 seconds
            }
        }

        private async Task ProcessDonationAsync(int donationId, int delaySeconds)
        {
            try
            {
                // Wait for the specified processing time
                await Task.Delay(delaySeconds * 1000); // Convert to milliseconds

                // Update status to "Completed"
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    string updateQuery = @"UPDATE institutional_donations 
                                          SET status = 'Completed' 
                                          WHERE id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", donationId);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                // Refresh the dashboard and donations panel
                RefreshAllPanels();
            }
            catch (Exception ex)
            {
                // Log error silently or show a notification
                Console.WriteLine("Error processing donation: " + ex.Message);
            }
        }

        private void RefreshAllPanels()
        {
            try
            {
                // Find the main form
                Form mainForm = Application.OpenForms.Cast<Form>()
                    .FirstOrDefault(f => f.Name == "MainForm" || f.Name == "Form1");

                if (mainForm != null)
                {
                    // Use Invoke to safely update UI from background thread
                    mainForm.Invoke((MethodInvoker)delegate
                    {
                        foreach (Control c in mainForm.Controls)
                        {
                            if (c is adminDashboard dash)
                            {
                                try { dash.RefreshDashboard(); } catch { }
                            }
                            if (c is InstitutionalDonationsControl idc)
                            {
                                try { idc.RefreshData(); } catch { }
                            }
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error refreshing panels: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SubmitInstitutionalDonationForm_Load(object sender, EventArgs e)
        {
        }

        private void btnSubmit_Click_1(object sender, EventArgs e)
        {
            btnSubmit_Click(sender, e);
        }

        private void cmbOrgType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}