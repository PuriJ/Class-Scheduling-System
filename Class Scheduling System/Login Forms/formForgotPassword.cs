using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Class_Scheduling_System.Connection;

namespace Class_Scheduling_System
{
    public partial class formForgotPassword : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SchedDB db = new SchedDB();

        public formForgotPassword()
        {
            InitializeComponent();
            con = new SqlConnection();
            con.ConnectionString = db.GetConnection();
        }

        //WILL IDENTIFY IF THAT EMAIL EXIST OR NOT EXIST OR INVALID, BEFORE TO PROCEED ON CHANGING PASSWORD
        public void Submit()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT * FROM [dbo].[Users] WHERE Email = '" + txtEmail.Text + "'", con);
                dr = cmd.ExecuteReader();
                if (validEmail.IsMatch(txtEmail.Text) != true)
                {
                    MessageBox.Show("Invalid Email Address. Please try again.", "Registration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                else if (dr.Read())
                {
                    txtEmail.Text = dr.GetValue(4).ToString();
                    formChangePassword fcp = new formChangePassword();
                    fcp.lblEmail.Text = dr.GetValue(4).ToString();
                    Hide();
                    fcp.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Email doesn't exist.", "Forgot Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Forgot Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Fill up the recovery email.", "Forgot Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Submit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Close this Program?", "Forgot Password", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            Hide();
            login.ShowDialog();
        }

        private static Regex emailValidation()
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(pattern, RegexOptions.IgnoreCase);
        }
        static Regex validEmail = emailValidation();

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        Point lastPoint;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
    }
}
