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
    public partial class formStudentScheduleList : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SchedDB db = new SchedDB();

        public formStudentScheduleList()
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
            cmd = new SqlCommand("SELECT * FROM [dbo].[viewStudentSchedule]", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["SSchedule_ID"], dr["Stud_ID"].ToString(), dr["AYCode"].ToString(), dr["Year"].ToString(), 
                                        dr["Course"].ToString(), dr["Section"].ToString(), dr["Subject_Code"].ToString(), dr["Subject_Name"].ToString(),
                                        dr["Units"].ToString(), dr["timeFrom"].ToString()+ "-" + dr["timeTo"].ToString(), dr["Days"].ToString(), 
                                        dr["Room_Code"].ToString(), dr["Campus"].ToString());
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
            formStudentSchedule fss = new formStudentSchedule(this);
            fss.btnUpdate.Enabled = false;
            fss.ShowDialog();
        }

        private void formStudentScheduleList_Load(object sender, EventArgs e)
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
                    formStudentSchedule fss = new formStudentSchedule(this);
                    con.Open();
                    cmd = new SqlCommand("SELECT * FROM [dbo].[viewStudentSchedule] WHERE SSchedule_ID LIKE '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        fss.lblSchedID.Text = dr["SSchedule_ID"].ToString();
                        fss.cbStudentNumber.Text = dr["Stud_ID"].ToString();
                        fss.txtLname.Text = dr["Lastname"].ToString();
                        fss.txtFname.Text = dr["Firstname"].ToString();
                        fss.txtMname.Text = dr["Middlename"].ToString();
                        fss.txtYearSem.Text = dr["AYCode"].ToString();
                        fss.cbYearLevel.Text = dr["Year"].ToString();
                        fss.cbCourse.Text = dr["Course"].ToString();

                        fss.cbSection.Text = dr["Section"].ToString();
                        fss.cbSubjCode.Text = dr["Subject_Code"].ToString();
                        fss.txtSubjDesc.Text = dr["Subject_Name"].ToString();
                        fss.txtUnits.Text = dr["Units"].ToString();
                        fss.cbRoom.Text = dr["Room_Code"].ToString();
                        fss.cbCampus.Text = dr["Campus"].ToString();
                        
                        fss.cbDays.Text = dr["Days"].ToString();
                        fss.mtTimeFrom.Text = dr["timeFrom"].ToString();
                        fss.mtTimeTo.Text = dr["timeTo"].ToString();
                        
                        fss.cbStudentNumber.Enabled = false;
                        fss.btnSave.Enabled = false;
                        fss.btnUpdate.Enabled = true;
                    }
                    dr.Close();
                    con.Close();
                    fss.ShowDialog();
                }
                else if (colName == "colDelete")
                {
                    if (MessageBox.Show("Delete this student schedule information?", "Student Schedule List", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        con.Open();
                        cmd = new SqlCommand("DELETE [dbo].[tableStudentSchedule] WHERE SSchedule_ID LIKE '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Student Schedule Information Deleted.", "Student Schedule List", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadData();
                    }
                }
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Student Schedule List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[viewStudentSchedule] WHERE Stud_ID LIKE '"+ txtSearch.Text +"%'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["SSchedule_ID"], dr["Stud_ID"].ToString(), dr["AYCode"].ToString(), dr["Year"].ToString(),
                                        dr["Course"].ToString(), dr["Section"].ToString(), dr["Subject_Code"].ToString(), dr["Subject_Name"].ToString(),
                                        dr["Units"].ToString(), dr["timeFrom"].ToString() + "-" + dr["timeTo"].ToString(), dr["Days"].ToString(),
                                        dr["Room_Code"].ToString(), dr["Campus"].ToString());
            }
            dr.Close();
            con.Close();
        }
    }
}
