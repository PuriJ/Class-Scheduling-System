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
    public partial class formUserAccount : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SchedDB db = new SchedDB();

        public formUserAccount()
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
            cmd = new SqlCommand("SELECT * FROM [dbo].[Users]", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["User_ID"].ToString(), dr["Lastname"].ToString(), dr["Firstname"].ToString(), dr["Middlename"].ToString(), dr["Email"].ToString(), dr["Username"].ToString(), dr["Password"].ToString(), dr["ConfirmPassword"].ToString());

            }
            dr.Close();
            con.Close();
        }

        private void formUserAccount_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[Users] WHERE Lastname LIKE '"+ txtSearch.Text +"%'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["User_ID"].ToString(), dr["Lastname"].ToString(), dr["Firstname"].ToString(), dr["Middlename"].ToString(), dr["Email"].ToString(), dr["Username"].ToString(), dr["Password"].ToString(), dr["ConfirmPassword"].ToString());

            }
            dr.Close();
            con.Close();
        }
    }
}
