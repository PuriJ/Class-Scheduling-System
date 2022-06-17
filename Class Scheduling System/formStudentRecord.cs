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
    public partial class formStudentRecord : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SchedDB db = new SchedDB();
        ReportDocument rd;

        public formStudentRecord()
        {
            InitializeComponent();
            con = new SqlConnection();
            con.ConnectionString = db.GetConnection();
            rd = new ReportDocument();
        }

        public void loadData()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableStudent]", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["ID"].ToString(), dr["Stud_ID"].ToString(), dr["Lastname"].ToString(), dr["Firstname"].ToString(), dr["Middlename"].ToString(),
                    DateTime.Parse(dr["Birthdate"].ToString()).ToShortDateString(), dr["Age"].ToString(), dr["BirthPlace"].ToString(), dr["Gender"].ToString(),
                    dr["Citizenship"].ToString(), dr["Marital"].ToString(), dr["Contact"].ToString(), dr["Address"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void formStudentRecord_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableStudent] WHERE Stud_ID LIKE '"+ txtSearch.Text +"%' OR Lastname LIKE '"+ txtSearch.Text +"%'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["ID"].ToString(), dr["Stud_ID"].ToString(), dr["Lastname"].ToString(), dr["Firstname"].ToString(), dr["Middlename"].ToString(),
                    DateTime.Parse(dr["Birthdate"].ToString()).ToShortDateString(), dr["Age"].ToString(), dr["BirthPlace"].ToString(), dr["Gender"].ToString(),
                    dr["Citizenship"].ToString(), dr["Marital"].ToString(), dr["Contact"].ToString(), dr["Address"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void btnGenReport_Click(object sender, EventArgs e)
        {
            formStudentReport fsr = new formStudentReport();
            StudentCRep StudentReport = new StudentCRep();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConnection1"].ToString();

            string sqlString = "SELECT * FROM [dbo].[tableStudent]";

            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlString, conn);

            sda.Fill(ds, "[dbo].[tableStudent]");
            DataTable dt = ds.Tables["[dbo].[tableStudent]"];

            StudentReport.SetDataSource(ds.Tables["[dbo].[tableStudent]"]);
            fsr.crystalReportViewer1.ReportSource = StudentReport;
            fsr.crystalReportViewer1.Refresh();
            StudentReport.ExportToDisk(ExportFormatType.PortableDocFormat, @"C:\Trash\New Trash Files 2020\paper\basta\Delete Files\try\Student Report Records.pdf");
            MessageBox.Show("Student Record Report has been Exported to C:\\Trash\\New Trash Files 2020\\paper\\basta\\Delete Files\\try", "Student Records", MessageBoxButtons.OK, MessageBoxIcon.Information);
            fsr.ShowDialog();

        }
    }
}
