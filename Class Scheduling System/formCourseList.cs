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
    public partial class formCourseList : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SchedDB db = new SchedDB();

        public formCourseList()
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
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableCourse]", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["Course_ID"].ToString(), dr["Course"].ToString(), dr["Course_Description"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void formCourseList_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if(colName == "colEdit")
            {
                formCourse fc = new formCourse(this);
                con.Open();
                cmd = new SqlCommand("SELECT * FROM [dbo].[tableCourse] WHERE Course_ID LIKE'"+ dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    fc.lblNum.Text = dr["Course_ID"].ToString();
                    fc.txtCourse.Text = dr["Course"].ToString();
                    fc.txtDescription.Text = dr["Course_Description"].ToString();
                    fc.btnSave.Enabled = false;
                    fc.btnUpdate.Enabled = true;
                }
                dr.Close();
                con.Close();
                fc.ShowDialog();
            }
            else if (colName == "colDelete")
            {
                if (MessageBox.Show("Delete this Course Information?", "Course List", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("DELETE [dbo].[tableCourse] WHERE Course_ID LIKE '"+ dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() +"'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    loadData();
                    MessageBox.Show("Course Information Deleted.", "Course List", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Course List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            formCourse fc = new formCourse(this);
            fc.btnUpdate.Enabled = false;
            fc.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
