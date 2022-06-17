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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Class_Scheduling_System.Connection;

namespace Class_Scheduling_System
{
    public partial class formStudentScheduleRecord : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SchedDB db = new SchedDB();

        public formStudentScheduleRecord()
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
                                        dr["Units"].ToString(), dr["timeFrom"].ToString() + "-" + dr["timeTo"].ToString(), dr["Days"].ToString(),
                                        dr["Room_Code"].ToString(), dr["Campus"].ToString());
            }
            dr.Close();
            con.Close();
        }
        
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[viewStudentSchedule] WHERE Stud_ID LIKE '" + txtSearch.Text + "%'", con);
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

        private void btnGenReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    MessageBox.Show("Input Student ID", "Student Schedule Records", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSearch.Select();
                    return;
                }

                formStudentScheduleReport fssr = new formStudentScheduleReport();
                StudentScheduleCRep SScheduleReport = new StudentScheduleCRep();
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConnection1"].ToString();

                string sqlString = "SELECT * FROM [dbo].[viewStudentSchedule] WHERE Stud_ID = '"+ txtSearch.Text +"'";

                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter(sqlString, conn);

                sda.Fill(ds, "[dbo].[viewStudentSchedule]");
                DataTable dt = ds.Tables["[dbo].[viewStudentSchedule]"];

                SScheduleReport.SetDataSource(ds.Tables["[dbo].[viewStudentSchedule]"]);
                fssr.crystalReportViewer1.ReportSource = SScheduleReport;
                fssr.crystalReportViewer1.Refresh();
                SScheduleReport.ExportToDisk(ExportFormatType.PortableDocFormat, @"C:\Trash\New Trash Files 2020\paper\basta\Delete Files\try\Student Schedule.pdf");
                MessageBox.Show("Student Schedule Report has been Exported to C:\\Trash\\New Trash Files 2020\\paper\\basta\\Delete Files\\try", "Student Schedule Records", MessageBoxButtons.OK, MessageBoxIcon.Information);
                fssr.ShowDialog();
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Student Schedule Records", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void formStudentScheduleRecord_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '-' && (sender as TextBox).Text.IndexOf('-') > -1)
            {
                e.Handled = true;
            }
        }
    }
}
