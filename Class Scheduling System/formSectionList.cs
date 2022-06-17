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
    public partial class formSectionList : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SchedDB db = new SchedDB();

        public formSectionList()
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
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableSection]", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["Section_ID"].ToString(), dr["Year"].ToString(), dr["Course"].ToString(), dr["Section"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void formSectionList_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colName = dataGridView1.Columns[e.ColumnIndex].Name;
                if (colName == "colEdit")
                {
                    formSection fs = new formSection(this);
                    con.Open();
                    cmd = new SqlCommand("SELECT * FROM [dbo].[tableSection] WHERE Section_ID LIKE'" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        fs.getCourse();
                        fs.lblSecID.Text = dr["Section_ID"].ToString();
                        fs.cbYLevel.Text = dr["Year"].ToString();
                        fs.cbCourse.Text = dr["Course"].ToString();
                        fs.txtSection.Text = dr["Section"].ToString();
                        fs.btnSave.Enabled = false;
                        fs.btnUpdate.Enabled = true;
                    }
                    dr.Close();
                    con.Close();
                    fs.ShowDialog();
                }

                else if (colName == "colDelete")
                {
                    if (MessageBox.Show("Delete this Section Information?", "Section List", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        con.Open();
                        cmd = new SqlCommand("DELETE [dbo].[tableSection] WHERE Section_ID LIKE '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        loadData();
                        MessageBox.Show("Section Information Deleted.", "Section List", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Section List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableSection] WHERE Year LIKE '"+ txtSearch.Text +"%' OR Section LIKE '"+ txtSearch.Text +"%'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["Section_ID"].ToString(), dr["Year"].ToString(), dr["Course"].ToString(), dr["Section"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            formSection fs = new formSection(this);
            fs.btnUpdate.Enabled = false;
            fs.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        
    }
}
