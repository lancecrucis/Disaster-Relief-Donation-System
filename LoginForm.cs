using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Disaster_Relief_Donation_System
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            label5.MouseEnter += label5_mousehover;
            label5.MouseLeave += label5_mouseleave;

            registerbutton.MouseEnter += registerbutton_mousehover;
            registerbutton.MouseLeave += registerbutton_mouseleave;




        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }


        private void registerbutton_mousehover(Object sender, EventArgs e) {
            registerbutton.Font = new Font(registerbutton.Font, FontStyle.Bold | FontStyle.Underline);
        }
        private void registerbutton_mouseleave(Object sender, EventArgs e)
        {
            registerbutton.Font = new Font(registerbutton.Font, FontStyle.Bold);
        }


        private void label5_mousehover(object sender, EventArgs e) {
            label5.Font = new Font(label5.Font, FontStyle.Bold | FontStyle.Underline);
        }
        private void label5_mouseleave(object sender, EventArgs e)
        {
            label5.Font = new Font(label5.Font, FontStyle.Bold);
        }
        private void label5_Click(object sender, EventArgs e)
        {
            forgotPassword forgotpass = new forgotPassword();
            forgotpass.Show();
            this.Hide();
        }
    }
}
