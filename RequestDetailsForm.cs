using Org.BouncyCastle.Asn1.Cmp;
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
    public partial class RequestDetailsForm : Form
    {
        public RequestDetailsForm(object data)
        {
            InitializeComponent();
            dynamic d = data;  

            lblName.Text = d.FullName;
            lblContact.Text = d.ContactNumber;
            lblLocation.Text = d.Location;
            lblPriority.Text = d.Priority.ToUpper();
            lblPeopleAffected.Text = d.PeopleAffected;
            lblStatus.Text = d.Status.ToUpper();

            

            txtDesc.Text = $"{d.PeopleAffected} people affected • Requested: {d.RequestedAt:MMM d, yyyy}";

            lblFundsNeeded.Text = "₱" + d.FundsNeeded;

            DateTime? neededBy = d.NeededBy as DateTime?;
            lblNeededBy.Text = neededBy.HasValue
                ? neededBy.Value.ToString("MMM d, yyyy")
                : "Not specified";

            txtDesc.Text = d.Description;

     
            txtAdditional.Text = string.IsNullOrEmpty(d.AdditionalInfo) ? "None" : d.AdditionalInfo;

            // Badge colors (keep your label/panel names)
            lblPriority.BackColor = d.Priority == "Critical" ? Color.Red : Color.DarkOrange;
            lblPriority.ForeColor = Color.White;

            lblStatus.BackColor = d.Status == "Approved" ? Color.Green :
                                 d.Status == "Pending" ? Color.Orange : Color.Gray;
            lblStatus.ForeColor = Color.White;
        }
        private void RequestDetailsForm_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDescription_Click(object sender, EventArgs e)
        {

        }

        private void lblPeopleAffected_Click(object sender, EventArgs e)
        {

        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }
    }
}
