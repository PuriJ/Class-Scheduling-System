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
using System.Net;
using System.Net.Mail;

namespace Class_Scheduling_System
{
    public partial class VerifyAccount : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SchedDB db = new SchedDB();
        int ID = 0;

        public VerifyAccount()
        {
            InitializeComponent();
            con = new SqlConnection();
            con.ConnectionString = db.GetConnection();
        }

        public void loadData()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[VerifyUsers]", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["User_ID"].ToString(), dr["Lastname"].ToString(), dr["Firstname"].ToString(), dr["Middlename"].ToString(), dr["Email"].ToString(), dr["Username"].ToString(), dr["Password"].ToString(), dr["ConfirmPassword"].ToString());

            }
            dr.Close();
            con.Close();
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

        private void delData()
        {
            if(ID != 0)
            {
                cmd = new SqlCommand("DELETE [dbo].[VerifyUsers] WHERE User_ID=@ID", con);
                con.Open();
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.ExecuteNonQuery();
                con.Close();
                loadData();
            }
        }

        //Sending Email Notification if the user's account was approve by the admin
        private void sendMail()
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("moxford42021@gmail.com");
                msg.To.Add(txtEmail.Text);
                msg.Subject = "QCU Class Scheduling Registration";
                msg.Body = "Your Account has been registered. You are now able to login.";


                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                NetworkCredential cred = new NetworkCredential();
                cred.UserName = "moxford42021@gmail.com";
                cred.Password = "givemebebetime21";
                client.Credentials = cred;
                client.EnableSsl = true;
                client.Port = 587;
                client.Send(msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Verify Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Sending Email Notification if the user's account was decline by the admin
        private void sendMail1()
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("moxford42021@gmail.com");
                msg.To.Add(txtEmail.Text);
                msg.Subject = "QCU Class Scheduling Registration";
                msg.Body = "Your Account has been declined. Please try again to register.";


                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                NetworkCredential cred = new NetworkCredential();
                cred.UserName = "moxford42021@gmail.com";
                cred.Password = "givemebebetime21";
                client.Credentials = cred;
                client.EnableSsl = true;
                client.Port = 587;
                client.Send(msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Verify Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void VerifyAccount_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            txtLName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtFName.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtMName.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtUsername.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtPassword.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtCPass.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (txtLName.Text == "" && txtFName.Text == "" && txtMName.Text == "" && txtEmail.Text == "" && txtUsername.Text == "")
                {
                    MessageBox.Show("Select Record to Verify", "Verify Account", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                else if (MessageBox.Show("Approve this User?", "Verify Account", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO [dbo].[Users] (Lastname, Firstname, Middlename, Email, Username, Password, ConfirmPassword) VALUES (@Lname, @Fname, @Mname, @Email, @Uname, @Pass, @CPass)", con);
                    con.Open();

                    //cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@Lname", txtLName.Text);
                    cmd.Parameters.AddWithValue("@Fname", txtFName.Text);
                    cmd.Parameters.AddWithValue("@Mname", txtMName.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Uname", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@Pass", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@CPass", txtCPass.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    sendMail();
                    Clear();
                    delData();
                    loadData();
                    MessageBox.Show("User Account Approved!", "Verify Account", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Verify Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtLName.Text == "" && txtFName.Text == "" && txtMName.Text == "" && txtEmail.Text == "" && txtUsername.Text == "")
                {
                    MessageBox.Show("Select Record to Verify", "Verify Account", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                    else if (MessageBox.Show("Decline this User?", "Verify Account", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        delData();
                        //sendMail1();
                        Clear();
                        loadData();
                        MessageBox.Show("User Account Declined!", "Verify Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Verify Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
