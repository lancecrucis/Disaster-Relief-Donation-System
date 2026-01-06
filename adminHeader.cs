using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main_Disaster_Relief_Donation_System
{
    public partial class adminHeader : UserControl
    {
        public adminHeader(int userId)
        {
            InitializeComponent();
        }

        private void adminDashboard_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
        "Are you sure you want to logout?",
        "Confirm Logout",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question) == DialogResult.No)
                return;

            // Find the parent form (MainDashboard)
            Form mainForm = this.FindForm();
            if (mainForm == null)
                return;

            // Hide the dashboard, show login modally, then close dashboard when login dialog closes.
            mainForm.Hide();
            using (LoginForm login = new LoginForm())
            {
                login.ShowDialog();
            }
            mainForm.Close();
        }
    }
}
