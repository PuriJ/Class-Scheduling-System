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
    public partial class formProfessorList : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SchedDB db = new SchedDB();

        public formProfessorList()
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
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableProfessor]", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["Professor_ID"].ToString(), dr["Lastname"].ToString(), dr["Firstname"].ToString(), dr["Middlename"].ToString(),
                                        dr["Under_Graduate_Degree"].ToString(), dr["Graduate_Degree"].ToString(), dr["Specialization"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void formProfessorList_Load(object sender, EventArgs e)
        {
            loadData();
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.btnClose, "Close");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colName = dataGridView1.Columns[e.ColumnIndex].Name;
                if (colName == "colEdit")
                {
                    formProfessor fp = new formProfessor(this);
                    fp.txtProfessorID.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    fp.txtLastname.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    fp.txtFirstname.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    fp.txtMiddlename.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    fp.txtUnderGradDegree.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    fp.txtGraduateDegree.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    fp.txtSpecialization.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    fp.btnSave.Enabled = false;
                    fp.txtProfessorID.Enabled = false;
                    fp.ShowDialog();
                }
                else if (colName == "colDelete")
                {
                    if (MessageBox.Show("Delete this Professor Information?", "Professor Masterlist", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        con.Open();
                        cmd = new SqlCommand("DELETE FROM [dbo].[tableProfessor] WHERE Professor_ID LIKE '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        loadData();
                        MessageBox.Show("Professor Information Deleted.", "Professor Masterlist", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Professor Masterlist", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableProfessor] WHERE Professor_ID LIKE '" + txtSearch.Text + "%' OR Lastname LIKE '" + txtSearch.Text + "%'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["Professor_ID"].ToString(), dr["Lastname"].ToString(), dr["Firstname"].ToString(), dr["Middlename"].ToString(),
                                        dr["Under_Graduate_Degree"].ToString(), dr["Graduate_Degree"].ToString(), dr["Specialization"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            formProfessor fp = new formProfessor(this);
            fp.btnUpdate.Enabled = false;
            fp.ShowDialog();
        }

        
    }
}
