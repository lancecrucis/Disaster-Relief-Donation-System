using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Main_Disaster_Relief_Donation_System
{
    public partial class LoginForm : Form
    {
        string connectionString = "server=localhost;user=root;password=1Alance101;database=disaster_relief_system;";
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT id, username, password, role FROM users WHERE username=@username";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string dbPassword = reader.GetString("password");
                                string role = reader.GetString("role");
                                int userId = reader.GetInt32("id");

                                // TODO: Replace with proper hashing later
                                if (password == dbPassword)
                                {
                                    this.Hide(); // Hide login form

                                    // Open the main dashboard and pass user info
                                    MainDashboard mainDash = new MainDashboard(userId, role, username);
                                    mainDash.ShowDialog(); // or just Show() if you want non-modal

                                    this.Close(); // Optional: close login after dashboard closes
                                }
                                else
                                {
                                    MessageBox.Show("Password is Incorrect.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Username or password is Incorrect.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            // Show registration form and hide login while register is open.
            this.Hide();
            using (Register reg = new Register())
            {
                reg.ShowDialog();
            }
            // After registration closes, show login again so user can sign in.
            this.Show();
        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e, object sender2 = null)
        {
            // placeholder to satisfy designer if duplicate event exists
        }
    }
}
