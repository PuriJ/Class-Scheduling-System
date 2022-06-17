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
using Class_Scheduling_System.Connection;
using System.Text.RegularExpressions;

namespace Class_Scheduling_System
{
    public partial class Registration : Form
    {
        /**SqlConnection con;
        SqlCommand com;
        string _title = "Scheduling";**/

        public Registration()
        {
            InitializeComponent();
            //con = new SqlConnection();

        }

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

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void btnReg_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLName.Text))
            {
                MessageBox.Show("Please fill up missing form", "Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblLName.Text = "Please Enter Last Name";
                return;
            }
            else
            {
                lblLName.Text = "";
            }

            if (string.IsNullOrEmpty(txtFName.Text))
            {
                MessageBox.Show("Please fill up the form", "Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblFName.Text = "Please Enter First Name";
                return;
            }
            else
            {
                lblFName.Text = "";
            }

            if (string.IsNullOrEmpty(txtMName.Text))
            {
                MessageBox.Show("Please fill up missing form", "Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblMName.Text = "Please Enter Middle Name";
                return;
            }
            else
            {
                lblMName.Text = "";
            }

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Please fill up missing form", "Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblEmail.Text = "Please Enter Email";
                return;
            }
            else
            {
                lblEmail.Text = "";
            }

            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Please fill up missing form", "Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblUser.Text = "Please Enter Username";
                return;
            }
            else
            {
                lblUser.Text = "";
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please fill up missing form", "Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblPass.Text = "Please Enter Password";
                return;
            }

            else
            {
                lblPass.Text = "";
            }

            if (string.IsNullOrEmpty(txtCPass.Text))
            {
                MessageBox.Show("Please fill up missing form", "Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblCPass.Text = "Please Enter Confirm Password";
                return;
            }

            else
            {
                lblCPass.Text = "";
            }

            if (txtPassword.Text.Length < 8)
            {
                MessageBox.Show("Password requires minimum of 8 characters", "Registration", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblPass.Text = "Min of 8 Characters";
                return;
            }
            else
            {
                lblPass.Text = "";
            }

            if (txtPassword.Text != txtCPass.Text)
            {
                MessageBox.Show("Password and Confirmation Password does not match.", "Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblCPassw.Text = "Password does not match";
                return;
            }
            else
            {
                lblCPassw.Text = "";
            }

            if (MessageBox.Show("Create Account?", "Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
            Reg();
            }
            
        }

        public void Reg()
        {
            try
            {
                string selSQL = "SELECT Username FROM [dbo].[VerifyUsers] WHERE Username = '" + txtUsername.Text + "'";
                string selSQLAdmin = "SELECT Username FROM [dbo].[Admin] WHERE Username = '"+ txtUsername.Text +"' ";
                DataTable checkDuplicate = Class_Scheduling_System.Connection.ServerConnection.executeSQL(selSQL);
                DataTable checkDuplicateAdmin = Class_Scheduling_System.Connection.ServerConnection.executeSQL(selSQLAdmin);
                
            
                if (checkDuplicate.Rows.Count > 0 || checkDuplicateAdmin.Rows.Count > 0)
                {
                    MessageBox.Show("Username already exist. Please use different username.", "Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                else if(validEmail.IsMatch(txtEmail.Text) != true)
                {
                    MessageBox.Show("Email Address Invalid.", "Registration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                else
                {
                string mySQL = string.Empty;

                mySQL += "INSERT INTO [dbo].[VerifyUsers] (Lastname, Firstname, Middlename, Email, Username, Password, ConfirmPassword)";
                mySQL += "VALUES ('"+ txtLName.Text +"', '"+ txtFName.Text +"', '"+ txtMName.Text +"', '"+ txtEmail.Text +"',";
                mySQL += "'" + txtUsername.Text +"', '"+ txtPassword.Text +"', '"+ txtCPass.Text +"')";
                Class_Scheduling_System.Connection.ServerConnection.executeSQL(mySQL);
                Clear();

                MessageBox.Show("Registration Successful. Please wait for your email notification before you login.", "Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Verify Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        public void Clear()
        {
            txtLName.Clear();
            txtFName.Clear();
            txtMName.Clear();
            txtEmail.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtCPass.Clear();
        }

        private void chkPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPass.Checked == true)
            {
                txtPassword.UseSystemPasswordChar = false;
                txtCPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                txtCPass.UseSystemPasswordChar = true;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            Hide();
            l.ShowDialog();
        }

        private static Regex emailValidation()
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(pattern, RegexOptions.IgnoreCase);
        }
        static Regex validEmail = emailValidation();

        private void Registration_Load(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.btnMinimize, "Minimize");
            tt.SetToolTip(this.btnClose, "Close");
        }
    }
}
