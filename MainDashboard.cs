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
    public partial class MainDashboard : Form
    {
        private int currentUserId;
        private string currentRole;
        private string currentUsername;
        public MainDashboard(int userId, string role, string username)
        {
            InitializeComponent();
            this.currentUserId = userId;
            this.currentRole = role;
            this.currentUsername = username;
        }

        private void MainDashboard_Load(object sender, EventArgs e)
        {
            this.Text = $"Disaster Relief System - Welcome, {currentUsername} ({currentRole})";

            // Clear any existing controls in the panel
            panelContent.Controls.Clear();
            dashboardPanel.Controls.Clear();

            button5.Visible = currentRole.ToLower() == "admin";
            button4.Visible = currentRole.ToLower() == "admin";


            if (currentRole.ToLower() == "admin")
            {
                adminHeader admindash = new adminHeader(currentUserId);
                admindash.Dock = DockStyle.Fill;
                panelContent.Controls.Add(admindash);

                adminDashboard adminDash = new adminDashboard();
                adminDash.Dock = DockStyle.Fill;
                dashboardPanel.Controls.Add(adminDash);
            }
            else // user or donor
            {
                userHeader userdash = new userHeader(currentUserId);
                userdash.Dock = DockStyle.Fill;
                panelContent.Controls.Add(userdash);

                // Load User view - sees only THEIR requests
                userDashboard userDash = new userDashboard();
                userDash.Dock = DockStyle.Fill;
                dashboardPanel.Controls.Add(userDash);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dashboardPanel.Controls.Clear();

            if (currentRole.ToLower() == "admin")
            {
                // Load Admin view - sees ALL requests
                userRequest adminReq = new userRequest(currentUserId, "admin");
                dashboardPanel.Controls.Add(adminReq);
            }
            else
            {
                // Load User view - sees only THEIR requests
                userRequest userReq = new userRequest(currentUserId, currentRole);
                userReq.Dock = DockStyle.Fill;
                dashboardPanel.Controls.Add(userReq);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dashboardPanel.Controls.Clear();

            if (currentRole.ToLower() == "admin")
            {
                // Load Admin view - sees ALL requests
                adminDashboard adminDash = new adminDashboard();
                adminDash.Dock = DockStyle.Fill;
                dashboardPanel.Controls.Add(adminDash);
            }
            else
            {
                // Load User view - sees only THEIR requests
                userDashboard userDash = new userDashboard();
                userDash.Dock = DockStyle.Fill;
                dashboardPanel.Controls.Add(userDash);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dashboardPanel.Controls.Clear();

            if (currentRole.ToLower() == "admin")
            {
                // Load Admin view - sees ALL requests
                affectedArea affectedArea = new affectedArea();
                affectedArea.Dock = DockStyle.Fill;
                dashboardPanel.Controls.Add(affectedArea);
            }
            else
            {
                // Load User view - sees only THEIR requests
                userDashboard userDash = new userDashboard();
                userDash.Dock = DockStyle.Fill;
                dashboardPanel.Controls.Add(userDash);
            }
        }

        public void LoadControl(UserControl control)
        {
            dashboardPanel.Controls.Clear();
            control.Dock = DockStyle.Fill;
            dashboardPanel.Controls.Add(control);
        }

        // New: central navigation helper to show FundTransfersControl using current user/context
        public void ShowFundTransfers()
        {
            dashboardPanel.Controls.Clear();

            if (currentRole.ToLower() == "admin")
            {
                FundTransfersControl transferControl = new FundTransfersControl(currentUserId, currentRole, currentUsername);
                transferControl.Dock = DockStyle.Fill;
                dashboardPanel.Controls.Add(transferControl);
            }
            else
            {
                MessageBox.Show("Only administrators can access Fund Transfers.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dashboardPanel.Controls.Clear();
            InstitutionalDonationsControl donationsControl = new InstitutionalDonationsControl();
            donationsControl.Dock = DockStyle.Fill;
            dashboardPanel.Controls.Add(donationsControl);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dashboardPanel.Controls.Clear();

            if (currentRole.ToLower() == "admin")
            {
                // pass the already-available currentUsername to FundTransfersControl
                FundTransfersControl transferControl = new FundTransfersControl(currentUserId, currentRole, currentUsername);
                transferControl.Dock = DockStyle.Fill;
                dashboardPanel.Controls.Add(transferControl);
            }
            else
            {
                MessageBox.Show("Only administrators can access Fund Transfers.");
            }
        }
    }
}

