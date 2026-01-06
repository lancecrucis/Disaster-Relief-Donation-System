using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Main_Disaster_Relief_Donation_System
{
    public partial class submitRequestForm : Form
    {
        string connectionString = "server=localhost;user=root;password=1Alance101;database=disaster_relief_system;";
        private int currentUserId;
        public submitRequestForm(int userId)
        {
            InitializeComponent();
            this.currentUserId = userId;
            

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(txtContactPerson.Text) ||
                string.IsNullOrWhiteSpace(txtContactNumber.Text) ||
                string.IsNullOrWhiteSpace(txtLocation.Text) ||
                string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please fill all required fields.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (numFundsNeeded.Value <= 0) { 
                MessageBox.Show("Please enter funds needed."); return; 
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO relief_requests 
                        (user_id, full_name, contact_number, affected_location, priority, 
                         people_affected, needed_by_date, situation_description, additional_info, funds_needed)
                        VALUES 
                        (@user_id, @full_name, @contact_number, @location, @priority, 
                         @people, @needed_by, @description, @additional, @funds_needed)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user_id", currentUserId);
                        cmd.Parameters.AddWithValue("@full_name", txtContactPerson.Text.Trim());
                        cmd.Parameters.AddWithValue("@contact_number", txtContactNumber.Text.Trim());
                        cmd.Parameters.AddWithValue("@location", txtLocation.Text.Trim());
                        cmd.Parameters.AddWithValue("@priority", cmbPriority.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@people", numPeopleAffected.Value);
                        cmd.Parameters.AddWithValue("@needed_by", dtpNeededBy.Value.Date);
                        cmd.Parameters.AddWithValue("@description", txtDescription.Text.Trim());
                        cmd.Parameters.AddWithValue("@additional", txtAdditional.Text.Trim());
                        cmd.Parameters.AddWithValue("@funds_needed", numFundsNeeded.Value);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Request submitted successfully! Our team will review it soon.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting request: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numFundsNeeded_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
