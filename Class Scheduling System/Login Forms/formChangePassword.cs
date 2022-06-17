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

namespace Class_Scheduling_System
{
    public partial class formChangePassword : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SchedDB db = new SchedDB();

        public formChangePassword()
        {
            InitializeComponent();
            con = new SqlConnection();
            con.ConnectionString = db.GetConnection();
        }

        public void Clear()
        {
            txtNewPass.Clear();
            txtReNewPass.Clear();
        }

        //CHANGING THE NEW PASSWORD BY UPDATING PASSWORD AND CONFIRM PASSWORD
        public void Confirm()
        {
            try
            {
                if (MessageBox.Show("Change Password?", "Change Password", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("UPDATE [dbo].[Users] SET Password = @Password, ConfirmPassword = @CPassword WHERE Email = @Email", con);
                    cmd.Parameters.AddWithValue("@Password", txtNewPass.Text);
                    cmd.Parameters.AddWithValue("@CPassword", txtReNewPass.Text);
                    cmd.Parameters.AddWithValue("@Email", lblEmail.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    MessageBox.Show("Password has been changed. Go back to Login.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Login login = new Login();
                    Hide();
                    login.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPass.Text))
                {
                    MessageBox.Show("Please fill up missing form", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (txtNewPass.Text.Length < 8)
                {
                    MessageBox.Show("Password requires minimum of 8 characters", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(txtReNewPass.Text))
                {
                    MessageBox.Show("Please fill up missing form", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (txtNewPass.Text != txtReNewPass.Text)
                {
                    MessageBox.Show("Password and Confirmation Password does not match.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                
            Confirm();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Close this Program?", "Change Password", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Close();
            }
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
    }
}
