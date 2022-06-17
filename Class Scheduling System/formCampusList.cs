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
    public partial class formCampusList : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SchedDB db = new SchedDB();

        public formCampusList()
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
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableCampus]", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["Campus_ID"].ToString(), dr["Campus_Name"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void addCampus()
        {
            try
            {
                if (MessageBox.Show("Save this Campus Information?", "Campus Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtCampus.Focus();
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO [dbo].[tableCampus](Campus_Name) VALUES (@Campus)", con);
                    cmd.Parameters.AddWithValue("@Campus", txtCampus.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    txtCampus.Clear();
                    loadData();
                    MessageBox.Show("Campus Information has been Saved.", "Campus Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                con.Close();
                MessageBox.Show("Campus already exist.\nInput different Campus", "Campus Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void formCampusList_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colName = dataGridView1.Columns[e.ColumnIndex].Name;
                if (colName == "colDelete")
                {
                    if (MessageBox.Show("Delete this Campus Information?", "Campus List", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        con.Open();
                        cmd = new SqlCommand("DELETE [dbo].[tableCampus] WHERE Campus_ID LIKE '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        loadData();
                        MessageBox.Show("Campus Information Deleted.", "Course List", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Campus List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addCampus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
