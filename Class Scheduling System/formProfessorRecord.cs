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
    public partial class formProfessorRecord : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SchedDB db = new SchedDB();

        public formProfessorRecord()
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void formProfessorRecord_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableProfessor] WHERE Professor_ID LIKE '"+ txtSearch.Text +"%' OR Lastname LIKE '"+ txtSearch.Text +"%'", con);
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

        private void btnGenReport_Click(object sender, EventArgs e)
        {
            formProfessorReport fpr = new formProfessorReport();
            ProfessorCRep ProfessorReport = new ProfessorCRep();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConnection1"].ToString();

            string sqlString = "SELECT * FROM [dbo].[tableProfessor]";

            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlString, conn);

            sda.Fill(ds, "[dbo].[tableProfessor]");
            DataTable dt = ds.Tables["[dbo].[tableProfessor]"];

            ProfessorReport.SetDataSource(ds.Tables["[dbo].[tableProfessor]"]);
            fpr.crystalReportViewer1.ReportSource = ProfessorReport;
            fpr.crystalReportViewer1.Refresh();
            ProfessorReport.ExportToDisk(ExportFormatType.PortableDocFormat, @"C:\Trash\New Trash Files 2020\paper\basta\Delete Files\try\Professor Report Records.pdf");
            MessageBox.Show("Professor Record Report has been Exported to C:\\Trash\\New Trash Files 2020\\paper\\basta\\Delete Files\\try", "Professor Records", MessageBoxButtons.OK, MessageBoxIcon.Information);
            fpr.ShowDialog();
        }
    }
}
