using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Class_Scheduling_System.Connection;
using System.Data.SqlClient;

namespace Class_Scheduling_System
{
    public partial class Login : Form
    {
        int count, attempt;

        public Login()
        {
            InitializeComponent();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
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

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Close this Program?", "Login Form", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void lnkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registration r = new Registration();
            Hide();
            r.ShowDialog();
        }

        private void chkPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPassword.Checked == true)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Loginn();
        }

        //DISABLE TO USE LOGIN FORM UNTIL IT REACH 30 SECONDS
        public void Disable()
        {
            if (attempt == 3)
            {
                MessageBox.Show("You have reached the maximum 3 attempts.\n Please try again.", "", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                attempt = 0;
                count = 30;
                timerLogin.Enabled = true;
                btnLogin.Enabled = false;
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
                chkPassword.Enabled = false;
            }
        }

        //LOGIN FUNCTION
        public void Loginn()
        {
            if (!string.IsNullOrEmpty(txtUsername.Text) &&
                !string.IsNullOrEmpty(txtPassword.Text))
            {
                string mySQL = string.Empty;

                mySQL += "SELECT * FROM [dbo].[Users]";
                mySQL += "WHERE Username = '" + txtUsername.Text + "' COLLATE SQL_Latin1_General_CP1_CS_AS AND Password = '" + txtPassword.Text + "' COLLATE SQL_Latin1_General_CP1_CS_AS ";

                string mySQLAdmin = string.Empty;

                mySQLAdmin += "SELECT * FROM [dbo].[Admin]";
                mySQLAdmin += "WHERE Username = '" + txtUsername.Text + "' AND Password = '" + txtPassword.Text + "' ";

                DataTable userData = ServerConnection.executeSQL(mySQL);
                DataTable adminData = ServerConnection.executeSQL(mySQLAdmin);

                //USER ACCOUNT WILL ACCESS TO THE SYSTEM
                if (userData.Rows.Count > 0)
                {

                    MessageBox.Show("Welcome, User " + userData.Rows[0].ItemArray[1].ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUsername.Clear();
                    txtPassword.Clear();
                    chkPassword.Checked = false;

                    this.Hide();

                    Userform uf = new Userform();
                    uf.mnLogout.Text = "Logout as " + userData.Rows[0].ItemArray[1].ToString() + ", " + userData.Rows[0].ItemArray[2].ToString();
                    uf.ShowDialog();
                    
                    uf = null;

                    this.Dispose();
                    this.txtUsername.Select();
                }

                else if (adminData.Rows.Count > 0)
                {
                    MessageBox.Show("Welcome, Admin "+ adminData.Rows[0].ItemArray[1].ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUsername.Clear();
                    txtPassword.Clear();
                    chkPassword.Checked = false;

                    this.Hide();

                    Adminform af = new Adminform();
                    af.mnLogout.Text = "Logout as " + adminData.Rows[0].ItemArray[1].ToString() + ", " + adminData.Rows[0].ItemArray[2].ToString();
                    af.ShowDialog();
                    af = null;

                    this.Dispose();
                    this.txtUsername.Select();
                }

                //INCORRECT USER ACCOUNT
                else
                {
                    MessageBox.Show("The username or password is incorrect.\n Please Try again.", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtUsername.Focus();
                    txtUsername.SelectAll();
                    attempt = attempt + 1;
                    Disable();
                }
            }
            //TEXTBOX IS NOT INPUT
            else
            {
                MessageBox.Show("Please enter username and password.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUsername.Select();
            }
        }

        private void timerLogin_Tick(object sender, EventArgs e)
        {
            //WILL STOP THE COOLDOWN
            if(count == 0)
            {
                timerLogin.Enabled = false;
                btnLogin.Enabled = true;
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
                chkPassword.Enabled = true;
                btnLogin.Text = "LOGIN";
                txtUsername.Focus();
            }
            //COUNTING THE COOLDOWN THROUGH LOGIN BUTTON UNTIL IT REACH 30 SECONDS
            else
            {
                btnLogin.Text = "" + count;
                count = count - 1;
            }
        }

        private void linkForgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            formForgotPassword ffp = new formForgotPassword();
            Hide();
            ffp.ShowDialog();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            attempt = 0;
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.btnMinimize, "Minimize");
            tt.SetToolTip(this.btnClose, "Close");
        }
    }
}
