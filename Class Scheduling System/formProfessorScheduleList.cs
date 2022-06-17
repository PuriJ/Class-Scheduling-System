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
    public partial class formProfessorScheduleList : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SchedDB db = new SchedDB();

        public formProfessorScheduleList()
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
            cmd = new SqlCommand("SELECT * FROM [dbo].[viewProfSchedule]", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["PSchedule_ID"].ToString(), dr["Professor_ID"].ToString(), dr["AYCode"].ToString(), dr["Year"].ToString(),
                                        dr["Course"].ToString(), dr["Section"].ToString(), dr["Subject_Code"].ToString(),
                                        dr["Subject_Name"].ToString(), dr["Units"].ToString(), dr["timeFrom"].ToString() + "-" + dr["timeTo"].ToString(),
                                        dr["Days"].ToString(), dr["Room_Code"].ToString(), dr["Campus"].ToString());
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
            formProfessorSchedule fps = new formProfessorSchedule(this);
            fps.btnUpdate.Enabled = false;
            fps.ShowDialog();
        }

        private void formProfessorScheduleList_Load(object sender, EventArgs e)
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
                    formProfessorSchedule fps = new formProfessorSchedule(this);
                    con.Open();
                    cmd = new SqlCommand("SELECT * FROM [dbo].[viewProfSchedule] WHERE PSchedule_ID LIKE '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        fps.lblProfID.Text = dr["PSchedule_ID"].ToString();
                        fps.cbProfessorNumber.Text = dr["Professor_ID"].ToString();
                        fps.txtLname.Text = dr["Lastname"].ToString();
                        fps.txtFname.Text = dr["Firstname"].ToString();
                        fps.txtMname.Text = dr["Middlename"].ToString();
                        
                        fps.txtYearSem.Text = dr["AYCode"].ToString();
                        fps.cbYearLevel.Text = dr["Year"].ToString();
                        fps.cbCourse.Text = dr["Course"].ToString();
                        fps.cbSection.Text = dr["Section"].ToString();

                        fps.cbSubjCode.Text = dr["Subject_Code"].ToString();
                        fps.txtSubjDesc.Text = dr["Subject_Name"].ToString();
                        fps.txtUnits.Text = dr["Units"].ToString();
                        fps.cbRoom.Text = dr["Room_Code"].ToString();

                        fps.cbDays.Text = dr["Days"].ToString();
                        fps.mtTimeFrom.Text = dr["timeFrom"].ToString();
                        fps.mtTimeTo.Text = dr["timeTo"].ToString();

                        fps.cbProfessorNumber.Enabled = false;
                        fps.btnSave.Enabled = false;
                        fps.btnUpdate.Enabled = true;
                    }
                    dr.Close();
                    con.Close();
                    fps.ShowDialog();
                }
                else if (colName == "colDelete")
                {
                    if (MessageBox.Show("Delete this professor schedule information?", "Professor Schedule List", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        con.Open();
                        cmd = new SqlCommand("DELETE [dbo].[tableProfSchedule] WHERE PSchedule_ID LIKE '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Student Schedule Information Deleted.", "Student Schedule List", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadData();
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Professor Schedule List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[viewProfSchedule] WHERE Professor_ID LIKE '"+ txtSearch.Text +"%'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["PSchedule_ID"].ToString(), dr["Professor_ID"].ToString(), dr["AYCode"].ToString(), dr["Year"].ToString(),
                                        dr["Year"].ToString(), dr["Course"].ToString(), dr["Section"].ToString(), dr["Subject_Code"].ToString(),
                                        dr["Subject_Name"].ToString(), dr["Units"].ToString(), dr["timeFrom"].ToString() + "-" + dr["timeTo"].ToString(),
                                        dr["Days"].ToString(), dr["Room_Code"].ToString(), dr["Campus"].ToString());
            }
            dr.Close();
            con.Close();
        }
    }
}
