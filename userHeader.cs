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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Main_Disaster_Relief_Donation_System
{
    public partial class userHeader : UserControl
    {
        string connectionString = "server=localhost;user=root;password=1Alance101;database=disaster_relief_system;";
        private int currentUserId;

        public userHeader(int userId)
        {
            InitializeComponent();
            this.currentUserId = userId;
            LoadUserName();
        }

        private void LoadUserName()
        {
            string username = "User";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Query to get user's full name or username
                    string query = "SELECT username FROM users WHERE id = @userId";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", currentUserId);

                        object result = cmd.ExecuteScalar();
                        if (result != null && !string.IsNullOrEmpty(result.ToString()))
                        {
                            username = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log silently or handle quietly
                Console.WriteLine("Error loading username: " + ex.Message);
            }

            if (lblWelcome != null)
            {
                lblWelcome.Text = $"Welcome, {username}!";
            }
            else
            {
                // Fallback: search for any label that might be the welcome label
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is Label lbl &&
                        (lbl.Name.ToLower().Contains("welcome") ||
                         lbl.Name.ToLower().Contains("user") ||
                         lbl.Name.ToLower().Contains("lblname")))
                    {
                        lbl.Text = $"Welcome, {username}!";
                        break;
                    }
                }
            }
        }

        private void userDashboard_Load(object sender, EventArgs e)
        {
            LoadUserName();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        // Logout button handler for user header
        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Are you sure you want to logout?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
                return;

            Form mainForm = this.FindForm();
            if (mainForm == null)
                return;

            mainForm.Hide();

            using (LoginForm login = new LoginForm())
            {
                login.ShowDialog();
            }

            mainForm.Close();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = "Emergency Hotline\nCALL 1-800-000 ";
            DialogResult result = MessageBox.Show(
        message,
        "Emergency Hotline",
        MessageBoxButtons.OK,
        MessageBoxIcon.Warning
    );
        }
    }
}