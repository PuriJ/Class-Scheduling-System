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
    public partial class formSubjectList : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SchedDB db = new SchedDB();

        public formSubjectList()
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
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableSubject]", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["Subject_ID"].ToString(), dr["Year_Level"].ToString(), dr["Subject_Code"].ToString(),
                                        dr["Subject_Name"].ToString(), dr["Units"].ToString(), dr["Type"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void formSubjectList_Load(object sender, EventArgs e)
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
                    formSubject fs = new formSubject(this);
                    con.Open();
                    cmd = new SqlCommand("SELECT * FROM [dbo].[tableSubject] WHERE Subject_ID LIKE'" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        fs.lblSubjID.Text = dr["Subject_ID"].ToString();
                        fs.cbYLevel.Text = dr["Year_Level"].ToString();
                        fs.txtSubjCode.Text = dr["Subject_Code"].ToString();
                        fs.txtSubjName.Text = dr["Subject_Name"].ToString();
                        fs.txtUnits.Text = dr["Units"].ToString();
                        fs.cbType.Text = dr["Type"].ToString();
                        fs.btnSave.Enabled = false;
                        fs.btnUpdate.Enabled = true;
                    }
                    dr.Close();
                    con.Close();
                    fs.ShowDialog();
                }
                else if (colName == "colDelete")
                {
                    if (MessageBox.Show("Delete this Subject Information?", "Subject List", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        con.Open();
                        cmd = new SqlCommand("DELETE [dbo].[tableSubject] WHERE Subject_ID LIKE '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        loadData();
                        MessageBox.Show("Subject Information Deleted.", "Subject List", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Subject List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            formSubject fs = new formSubject(this);
            fs.btnUpdate.Enabled = false;
            fs.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableSubject] WHERE Year_Level LIKE '"+ txtSearch.Text +"%' OR Subject_Code LIKE '" + txtSearch.Text + "%'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["Subject_ID"].ToString(), dr["Year_Level"].ToString(), dr["Subject_Code"].ToString(),
                                        dr["Subject_Name"].ToString(), dr["Units"].ToString(), dr["Type"].ToString());
            }
            dr.Close();
            con.Close();
        }
    }
}
