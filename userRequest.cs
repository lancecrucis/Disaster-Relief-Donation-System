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

namespace Main_Disaster_Relief_Donation_System
{ 
    public partial class userRequest : UserControl
    {
        string connectionString = "server=localhost;user=root;password=1Alance101;database=disaster_relief_system;";
        private int currentUserId;
        private string userRole;  // Add this
        public userRequest(int userId, string role)
        {
            InitializeComponent();
            this.currentUserId = userId;
            this.userRole = role.ToLower();  // "admin" or "user
            LoadUserRequests();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            submitRequestForm form = new submitRequestForm(currentUserId);
            form.ShowDialog(); // Modal - waits until closed

            // After form closes, refresh the list
            LoadUserRequests();
        }
        private void UpdateRequestStatus(int requestId, string newStatus)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"UPDATE relief_requests 
                             SET status = @status, 
                                 approved_at = CASE WHEN @status = 'Approved' THEN NOW() ELSE approved_at END
                             WHERE id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", requestId);
                        cmd.Parameters.AddWithValue("@status", newStatus);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"Request has been {newStatus.ToLower()}!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating request: " + ex.Message);
            }
        }

        private void LoadUserRequests()
        {
            panelRequests.Controls.Clear(); // Clear old cards
            int topPosition = 10; // starting margin

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = userRole == "admin"
                    ? @"SELECT id, full_name, contact_number, affected_location, people_affected,
                        requested_at, priority, status, situation_description, 
                        additional_info, needed_by_date, funds_needed
                        FROM relief_requests
                        ORDER BY requested_at DESC"
                    : @"SELECT id, full_name, contact_number, affected_location, people_affected,
                        requested_at, priority, status, situation_description, 
                        additional_info, needed_by_date, funds_needed
                        FROM relief_requests
                        WHERE user_id = @user_id
                        ORDER BY requested_at DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user_id", currentUserId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Create a card (panel) for each request
                                Panel card = new Panel
                                {
                                    BackColor = Color.White,
                                    Width = panelRequests.ClientSize.Width - 40,
                                    Padding = new Padding(16),
                                    Margin = new Padding(10, 10, 10, 20),  // Extra bottom margin
                                    BorderStyle = BorderStyle.FixedSingle,
                                    AutoSize = false,  // We'll set height manually
                                    Location = new Point(10, topPosition)
                                };
                                card.BorderStyle = BorderStyle.FixedSingle;

                                // Create View Details button (created but not added yet)
                                Button btnDetails = new Button
                                {
                                    Name = "btnDetails",
                                    Text = "View Details",
                                    BackColor = Color.FromArgb(233,233,233),
                                    ForeColor = Color.Black,
                                    FlatStyle = FlatStyle.Flat,
                                    Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                                    Size = new Size(120, 35),
                                    Cursor = Cursors.Hand
                                };
                                btnDetails.FlatAppearance.BorderSize = 0;

                                // Capture request ID and status safely
                                int requestId = reader.GetInt32("id");
                                string currentStatus = reader["status"] == DBNull.Value ? "Pending" : reader["status"].ToString();

                                // Prepare approve button variable (created later if needed)
                                Button btnApprove = null;

                                // Create a copy of the current row's data as an anonymous object (null-safe)
                                var requestData = new
                                {
                                    Id = reader.GetInt32("id"),
                                    FullName = reader["full_name"].ToString(),
                                    ContactNumber = reader["contact_number"].ToString(),
                                    Location = reader["affected_location"].ToString(),
                                    PeopleAffected = reader["people_affected"].ToString(),
                                    RequestedAt = Convert.ToDateTime(reader["requested_at"]),
                                    Priority = reader["priority"].ToString(),
                                    Status = reader["status"].ToString(),
                                    Description = reader["situation_description"].ToString(),
                                    AdditionalInfo = reader["additional_info"] == DBNull.Value ? "" : reader["additional_info"].ToString(),
                                    NeededBy = reader["needed_by_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["needed_by_date"]),
                                    FundsNeeded = Convert.ToDecimal(reader["funds_needed"]).ToString("N2")
                                };

                                // Attach click event using the captured data
                                btnDetails.Click += (s, e) =>
                                {
                                    RequestDetailsForm detailsForm = new RequestDetailsForm(requestData);
                                    detailsForm.ShowDialog();
                                };

                                // Name and Location labels
                                Label lblName = new Label
                                {
                                    Name = "lblName",
                                    Text = requestData.FullName,
                                    Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                                    ForeColor = Color.Black,
                                    AutoSize = true,
                                    Location = new Point(10, 10)
                                };
                                Label lblContact = new Label
                                {
                                    Name = "lblContact",
                                    Text = "+63 " + requestData.ContactNumber,
                                    Font = new Font("Segoe UI", 11F),
                                    ForeColor = Color.FromArgb(64, 64, 64),
                                    AutoSize = true,
                                    Location = new Point(10, lblName.Bottom + 6)
                                };
                                Label lblLocation = new Label
                                {
                                    Name = "lblLocation",
                                    Text = "Location: " + requestData.Location,
                                    Font = new Font("Segoe UI", 11F),
                                    ForeColor = Color.FromArgb(64, 64, 64),
                                    AutoSize = true,
                                    Location = new Point(10, lblContact.Bottom + 6)
                                };

                                // Priority badge
                                string priority = requestData.Priority;
                                Label lblPriority = new Label
                                {
                                    Name = "lblPriority",
                                    Text = string.IsNullOrEmpty(priority) ? "" : priority.ToUpper(),
                                    ForeColor = Color.White,
                                    BackColor = priority == "Critical" ? Color.Red : Color.DarkOrange,
                                    Padding = new Padding(5, 5, 5, 5),
                                    Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                                    AutoSize = true
                                };

                                // Status badge
                                string status = requestData.Status;
                                Label lblStatus = new Label
                                {
                                    Name = "lblStatus",
                                    Text = string.IsNullOrEmpty(status) ? "PENDING" : status.ToUpper(),
                                    ForeColor = Color.White,
                                    BackColor = status.Equals("Pending", StringComparison.OrdinalIgnoreCase) ? Color.Orange :
                                               (status.Equals("Approved", StringComparison.OrdinalIgnoreCase) ? Color.Green : Color.Gray),
                                    Padding = new Padding(5, 5, 5, 5),
                                    Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                                    AutoSize = true
                                };

                                // Details line
                                Label lblDetails = new Label
                                {
                                    Name = "lblDetails",
                                    Text = $"{requestData.PeopleAffected} people affected | Requested: {(requestData.RequestedAt == DateTime.MinValue ? "" : requestData.RequestedAt.ToString("MMM d, yyyy"))}",
                                    ForeColor = Color.FromArgb(64, 64, 64),
                                    Font = new Font("Segoe UI", 11F),
                                    AutoSize = true,
                                    Location = new Point(10, lblLocation.Bottom + 8)
                                };

                                // Description (wrap will be configured after card width is known)
                                Label lblDesc = new Label
                                {
                                    Name = "lblDesc",
                                    Text = $"Description: {requestData.Description}",
                                    Font = new Font("Segoe UI", 10F),
                                    ForeColor = Color.FromArgb(64, 64, 64),
                                    AutoSize = true,
                                    Location = new Point(10, lblDetails.Bottom + 10)
                                };

                                Label lblFunds = new Label
                                {
                                    Name = "lblFunds",
                                    Text = $"Funds Requested: ₱{requestData.FundsNeeded}",
                                    Font = new Font("Segoe UI", 12F, FontStyle.Regular),
                                    ForeColor = Color.Red,
                                    AutoSize = true,
                                    Location = new Point(10, lblDesc.Bottom + 22)
                                };

                                // Add labels to card (do not add buttons yet)
                                card.Controls.AddRange(new Control[] { lblName, lblContact, lblLocation, lblDetails, lblDesc, lblFunds, lblPriority, lblStatus });

                                // FIX TRUNCATE: set wrapping width for description AFTER the card width is finalized
                                int wrapWidth = Math.Max(200, card.Width - 60);
                                lblDesc.MaximumSize = new Size(wrapWidth, 0);
                                lblDesc.AutoSize = true; // ensure resize

                                // Recalculate badge sizes before positioning them
                                lblPriority.Size = lblPriority.PreferredSize;
                                lblStatus.Size = lblStatus.PreferredSize;

                                // Position priority and status badges at top-right corner with small spacing
                                int badgesTop = 12;
                                lblPriority.Location = new Point(card.Width - lblPriority.Width - 16, badgesTop);
                                lblStatus.Location = new Point(lblPriority.Left - lblStatus.Width - 8, badgesTop);

                                // Compute card height based on current (non-button) children
                                int bottom = card.Controls.Cast<Control>().Where(c => !(c is Button)).Max(c => c.Bottom);
                                // Leave space for buttons (if present) and padding
                                int footerSpace = 15 + 15; // gap + button height
                                card.Height = bottom + 5 + footerSpace;

                                // Now position the Details button anchored to bottom-right
                                btnDetails.Location = new Point(card.Width - btnDetails.Width - 20, card.Height - btnDetails.Height - 20);

                                // If admin and pending, create approve button and place it to the left of Details
                                if (userRole == "admin" && currentStatus.Equals("Pending", StringComparison.OrdinalIgnoreCase))
                                {
                                    btnApprove = new Button
                                    {
                                        Name = "btnApprove",
                                        Text = "Approve",
                                        BackColor = Color.FromArgb(40, 167, 69),  // Green
                                        ForeColor = Color.White,
                                        FlatStyle = FlatStyle.Flat,
                                        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                                        Size = new Size(100, 35),
                                        Cursor = Cursors.Hand
                                    };
                                    btnApprove.FlatAppearance.BorderSize = 0;

                                    btnApprove.Click += (s, e) =>
                                    {
                                        if (MessageBox.Show("Approve this request?\nIt will appear in Affected Areas.",
                                            "Confirm Approval", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            UpdateRequestStatus(requestId, "Approved");
                                            // Update status badge only and disable buttons
                                            var statusLabel = card.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblStatus");
                                            if (statusLabel != null)
                                            {
                                                statusLabel.Text = "APPROVED";
                                                statusLabel.BackColor = Color.Green;
                                                statusLabel.ForeColor = Color.White;
                                            }
                                            btnApprove.Enabled = false;
                                            btnDetails.Enabled = true;
                                        }
                                    };

                                    // Position approve to the left of details with 12px gap and same vertical alignment
                                    btnApprove.Location = new Point(btnDetails.Left - btnApprove.Width - 12, btnDetails.Top);

                                    // Add buttons to card
                                    card.Controls.Add(btnApprove);
                                    button1.Hide();
                                }

                                // Finally add the details button
                                card.Controls.Add(btnDetails);

                                // Recompute card.Height in case buttons extend it
                                int newBottom = card.Controls.Cast<Control>().Max(c => c.Bottom);
                                card.Height = newBottom + 20;

                                // Add card to panel and increment top position
                                panelRequests.Controls.Add(card);
                                topPosition += card.Height + 20; // space between cards

                            }
                        }
                    }
                }

                if (panelRequests.Controls.Count == 0)
                {
                    Label lblNoRequests = new Label
                    {
                        Text = "No requests submitted yet.",
                        ForeColor = Color.Gray,
                        Font = new Font("Segoe UI", 12),
                        AutoSize = true,
                        Location = new Point(20, 20)
                    };
                    panelRequests.Controls.Add(lblNoRequests);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading requests: " + ex.Message);
            }
        }

        private void panelRequests_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
