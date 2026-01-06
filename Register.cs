using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Main_Disaster_Relief_Donation_System
{
    public partial class Register : Form
    {
        // preserve designer controls and comment blocks
        string connectionString = "server=localhost;user=root;password=1Alance101;database=disaster_relief_system;";

        public Register()
        {
            InitializeComponent();

            // Make password boxes behave like passwords (designer used textBox9/textBox10)
            try
            {
                textBox9.PasswordChar = '*';
                textBox10.PasswordChar = '*';
            }
            catch { }

            // Wire register button
            button1.Click += Button1_Click;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = (textBox9.Text ?? "").Trim();
            string confirm = (textBox10.Text ?? "").Trim();
            bool confirmed = checkBox1.Checked;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirm))
            {
                MessageBox.Show("Please fill Username, Password and Confirm Password.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Passwords do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!confirmed)
            {
                MessageBox.Show("Please confirm that the information provided is accurate.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Only insert username and password (cheat as requested).
                    // Assign default role 'user'.
                    string insert = @"INSERT INTO users (username, password, role) VALUES (@username, @password, @role)";
                    using (MySqlCommand cmd = new MySqlCommand(insert, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password); // replace with hash in production
                        cmd.Parameters.AddWithValue("@role", "user");

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Registration successful. You can now log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // close register and return to login
            }
            catch (MySqlException mex)
            {
                // Duplicate username MySQL error code = 1062
                if (mex.Number == 1062)
                {
                    MessageBox.Show("Username already exists. Choose a different username.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Database error: " + mex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
