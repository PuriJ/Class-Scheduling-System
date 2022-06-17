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
    public partial class formProfessorScheduleRecord : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SchedDB db = new SchedDB();

        public formProfessorScheduleRecord()
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
                                            dr["Course"].ToString(), dr["Section"].ToString(), dr["Subject_Code"].ToString(),
                                            dr["Subject_Name"].ToString(), dr["Units"].ToString(), dr["timeFrom"].ToString() + "-" + dr["timeTo"].ToString(),
                                            dr["Days"].ToString(), dr["Room_Code"].ToString(), dr["Campus"].ToString());
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
                    MessageBox.Show("Input Professor ID", "Professor Schedule Records", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSearch.Select();
                    return;
                }

                formProfessorScheduleReport fpsr = new formProfessorScheduleReport();
                ProfessorScheduleCRep ProfScheduleReport = new ProfessorScheduleCRep();
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["myConnection1"].ToString();

                string sqlString = "SELECT * FROM [dbo].[viewProfSchedule] WHERE Professor_ID = '" + txtSearch.Text + "'";

                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter(sqlString, conn);

                sda.Fill(ds, "[dbo].[viewProfSchedule]");
                DataTable dt = ds.Tables["[dbo].[viewProfSchedule]"];

                ProfScheduleReport.SetDataSource(ds.Tables["[dbo].[viewProfSchedule]"]);
                fpsr.crystalReportViewer1.ReportSource = ProfScheduleReport;
                fpsr.crystalReportViewer1.Refresh();
                ProfScheduleReport.ExportToDisk(ExportFormatType.PortableDocFormat, @"C:\Trash\New Trash Files 2020\paper\basta\Delete Files\try\Professor Schedule.pdf");
                MessageBox.Show("Professor Schedule Report has been Exported to C:\\Trash\\New Trash Files 2020\\paper\\basta\\Delete Files\\try", "Professor Schedule Records", MessageBoxButtons.OK, MessageBoxIcon.Information);
                fpsr.ShowDialog();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Professor Schedule Records", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void formProfessorScheduleRecord_Load(object sender, EventArgs e)
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
