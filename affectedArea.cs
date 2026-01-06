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
    public partial class affectedArea : UserControl
    {
        string connectionString = "server=localhost;user=root;password=1Alance101;database=disaster_relief_system;";

        public affectedArea()
        {
            InitializeComponent();
            LoadAffectedAreas();
        }

        private void LoadAffectedAreas()
        {
            LoadStats();
            LoadApprovedRequests();
        }

        private void LoadStats()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Total Approved Areas
                    string queryTotal = "SELECT COUNT(*) FROM relief_requests WHERE status = 'Approved'";
                    using (MySqlCommand cmd = new MySqlCommand(queryTotal, conn))
                    {
                        int total = Convert.ToInt32(cmd.ExecuteScalar());
                        lblTotalLocations.Text = total.ToString() + (total == 1 ? " location" : " locations");
                    }

                    // Active Areas (needed_by_date >= today AND not fully funded)
                    string queryActive = @"SELECT COUNT(*) FROM relief_requests 
                                          WHERE status = 'Approved' 
                                          AND needed_by_date >= CURDATE()
                                          AND COALESCE(funds_received, 0) < funds_needed";
                    using (MySqlCommand cmd = new MySqlCommand(queryActive, conn))
                    {
                        int active = Convert.ToInt32(cmd.ExecuteScalar());
                        lblActiveAreas.Text = active.ToString();
                    }

                    // Total Funding Needed
                    string queryNeeded = "SELECT COALESCE(SUM(funds_needed), 0) FROM relief_requests WHERE status = 'Approved'";
                    using (MySqlCommand cmd = new MySqlCommand(queryNeeded, conn))
                    {
                        decimal needed = Convert.ToDecimal(cmd.ExecuteScalar());
                        lblFundingNeeded.Text = $"₱{needed:N0}";
                    }

                    // Total Funding Received
                    string queryReceived = "SELECT COALESCE(SUM(funds_received), 0) FROM relief_requests WHERE status = 'Approved'";
                    using (MySqlCommand cmd = new MySqlCommand(queryReceived, conn))
                    {
                        decimal received = Convert.ToDecimal(cmd.ExecuteScalar());
                        lblFundingReceived.Text = $"₱{received:N0}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading stats: " + ex.Message);
            }
        }

        private void LoadApprovedRequests()
        {
            flowAreas.Controls.Clear();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT 
                        affected_location, full_name, contact_number, people_affected,
                        needed_by_date, situation_description, funds_needed, 
                        COALESCE(funds_received, 0) as funds_received,
                        approved_at, priority
                        FROM relief_requests 
                        WHERE status = 'Approved' 
                        ORDER BY 
                            CASE WHEN COALESCE(funds_received, 0) >= funds_needed THEN 1 ELSE 0 END,
                            approved_at DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CreateAreaCard(reader);
                        }
                    }
                }

                if (flowAreas.Controls.Count == 0)
                {
                    Label lblNoAreas = new Label
                    {
                        Text = "No approved affected areas yet.",
                        ForeColor = Color.Gray,
                        Font = new Font("Segoe UI", 12),
                        AutoSize = true,
                        Location = new Point(20, 20)
                    };
                    flowAreas.Controls.Add(lblNoAreas);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading areas: " + ex.Message);
            }
        }

        private void CreateAreaCard(MySqlDataReader reader)
        {
            // Calculate funding status
            decimal needed = Convert.ToDecimal(reader["funds_needed"]);
            decimal received = Convert.ToDecimal(reader["funds_received"]);
            decimal percentage = needed > 0 ? (received / needed) * 100 : 0;
            bool isFullyFunded = received >= needed;

            // Layout constants (spacing/layout only)
            const int horizontalPadding = 16;
            const int verticalPadding = 12;
            const int betweenControls = 8;
            int cardWidth = Math.Max(360, flowAreas.ClientSize.Width - 40);

            Panel card = new Panel
            {
                BackColor = Color.White,
                Width = cardWidth,
                Padding = new Padding(horizontalPadding),
                Margin = new Padding(10, 10, 10, 30),
                BorderStyle = BorderStyle.FixedSingle
            };

            // running y position inside card
            int y = verticalPadding;

            // Title (location)
            Label lblLocation = new Label
            {
                Text = reader["affected_location"].ToString(),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.Black,
                Location = new Point(horizontalPadding, y)
            };
            card.Controls.Add(lblLocation);

            // Badges - add early to measure, will reposition after sizes are known
            string priority = reader["priority"].ToString();
            Label lblPriorityBadge = new Label
            {
                Text = string.IsNullOrEmpty(priority) ? "" : priority.ToUpper(),
                ForeColor = Color.White,
                BackColor = priority == "Critical" ? Color.Red : Color.DarkOrange,
                Padding = new Padding(10, 5, 10, 5),
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                AutoSize = true
            };
            Label lblStatusBadge = new Label
            {
                Text = isFullyFunded ? "COMPLETED" : "ACTIVE",
                ForeColor = Color.White,
                BackColor = isFullyFunded ? Color.FromArgb(40, 167, 69) : Color.FromArgb(219,0,7),
                Padding = new Padding(10, 5, 10, 5),
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                AutoSize = true
            };
            card.Controls.Add(lblPriorityBadge);
            card.Controls.Add(lblStatusBadge);

            // move y under title
            y = lblLocation.Bottom + betweenControls;

            // Requested by / contact / deadline
            string deadline = reader["needed_by_date"] == DBNull.Value
                ? "Not specified"
                : Convert.ToDateTime(reader["needed_by_date"]).ToString("MMM d, yyyy");

            Label lblRequestedBy = new Label
            {
                Text = $"Requested by: {reader["full_name"]}  |  {reader["contact_number"]}  | Deadline: {deadline}",
                ForeColor = Color.FromArgb(64, 64, 64),
                Font = new Font("Segoe UI", 10F),
                AutoSize = true,
                Location = new Point(horizontalPadding, y)
            };
            card.Controls.Add(lblRequestedBy);

            y = lblRequestedBy.Bottom + (betweenControls / 2);

            // People affected
            Label lblPeople = new Label
            {
                Text = $"{reader["people_affected"]} people affected",
                ForeColor = Color.FromArgb(64, 64, 64),
                Font = new Font("Segoe UI", 10F),
                AutoSize = true,
                Location = new Point(horizontalPadding, y)
            };
            card.Controls.Add(lblPeople);

            y = lblPeople.Bottom + betweenControls;

            // Description (wrap)
            Label lblDesc = new Label
            {
                Text = reader["situation_description"].ToString(),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(64, 64, 64),
                AutoSize = true,
                MaximumSize = new Size(card.Width - (horizontalPadding * 2) - 10, 0),
                Location = new Point(horizontalPadding, y)
            };
            card.Controls.Add(lblDesc);

            y = lblDesc.Bottom + betweenControls;

            // Progress bar (use inner width)
            int innerWidth = card.Width - (card.Padding.Left + card.Padding.Right);
            ProgressBar pb = new ProgressBar
            {
                Value = Math.Min((int)percentage, 100),
                Maximum = 100,
                Height = 18,
                Width = Math.Max(200, innerWidth),
                Location = new Point(horizontalPadding, y),
                Style = ProgressBarStyle.Continuous
            };
            card.Controls.Add(pb);

            y = pb.Bottom + betweenControls;

            // Progress text left and percentage right
            Label lblProgressText = new Label
            {
                Text = $"₱{received:N0} / ₱{needed:N0}",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(horizontalPadding, y)
            };
            card.Controls.Add(lblProgressText);

            Label lblPercentage = new Label
            {
                Text = $"{Math.Min(percentage, 100):F0}%",
                Font = new Font("Segoe UI", 9F),
                ForeColor = isFullyFunded ? Color.FromArgb(40, 167, 69) : Color.Gray,
                AutoSize = true
            };
            // position percentage on the right with same top
            lblPercentage.Location = new Point(card.Width - card.Padding.Right - lblPercentage.PreferredWidth - horizontalPadding, lblProgressText.Top);
            card.Controls.Add(lblPercentage);

            y = Math.Max(lblProgressText.Bottom, lblPercentage.Bottom) + betweenControls;

            // Remaining / approved
            Label lblRemaining = new Label
            {
                Text = isFullyFunded ? "Fully Funded!" : $"Remaining: ₱{(needed - received):N0}",
                ForeColor = isFullyFunded ? Color.FromArgb(40, 167, 69) : Color.Red,
                Font = new Font("Segoe UI",11F, isFullyFunded ? FontStyle.Bold : FontStyle.Regular),
                AutoSize = true,
                Location = new Point(horizontalPadding, y)
            };
            card.Controls.Add(lblRemaining);

            y = lblRemaining.Bottom + betweenControls;

            Label lblApproved = new Label
            {
                Text = reader["approved_at"] == DBNull.Value ? "Approved: N/A" : $"Approved: {Convert.ToDateTime(reader["approved_at"]):MMM d, yyyy}",
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                AutoSize = true,
                Location = new Point(horizontalPadding, y)
            };
            card.Controls.Add(lblApproved);

            // Footer buttons
            Button btnDonate = new Button
            {
                Text = isFullyFunded ? "Completed" : "Donate Now",
                BackColor = isFullyFunded ? Color.Gray : Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(140, 40),
                Enabled = !isFullyFunded
            };
            btnDonate.FlatAppearance.BorderSize = 0;
            if (!isFullyFunded)
            {
                btnDonate.Click += (s, e) =>
                {
                    var main = this.FindForm() as MainDashboard;
                    if (main != null)
                    {
                        main.ShowFundTransfers();
                    }
                };
            }

            

                // place footer buttons with consistent footer spacing
            int footerY = lblApproved.Bottom + (verticalPadding * 2);
            btnDonate.Location = new Point(horizontalPadding, footerY);
            

            // Reposition badges top-right now that sizes are known
            lblPriorityBadge.Size = lblPriorityBadge.PreferredSize;
            lblStatusBadge.Size = lblStatusBadge.PreferredSize;
            int badgeTop = verticalPadding;
            lblPriorityBadge.Location = new Point(card.Width - card.Padding.Right - lblPriorityBadge.Width - horizontalPadding / 2, badgeTop);
            lblStatusBadge.Location = new Point(lblPriorityBadge.Left - lblStatusBadge.Width - 8, badgeTop);

            // Add footer buttons to card
            card.Controls.Add(btnDonate);
            

            // Final card height
            int bottom = card.Controls.Cast<Control>().Max(c => c.Bottom);
            card.Height = bottom + verticalPadding;

            flowAreas.Controls.Add(card);
        }

        private void affectedArea_Load(object sender, EventArgs e)
        {
        }

        public void RefreshData()
        {
            LoadAffectedAreas();
        }

        private void flowAreas_Paint(object sender, PaintEventArgs e)
        {
        }

        private void lblTotalLocations_Click(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}