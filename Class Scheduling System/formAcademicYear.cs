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
    public partial class formAcademicYear : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SchedDB db = new SchedDB();

        public formAcademicYear()
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
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableAcadYear]", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["AYCode"].ToString(), dr["Year1st"].ToString() + "-" + dr["Year2nd"].ToString(), 
                                        dr["Semester"].ToString(), dr["Status"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if(colName == "colOpen")
            {
                if (MessageBox.Show("OPEN THIS ACADEMIC YEAR/SEMESTER?", "Academic Year", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("UPDATE [dbo].[tableAcadYear] SET Status = 'CLOSE'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    cmd = new SqlCommand("UPDATE [dbo].[tableAcadYear] SET Status = 'OPEN' WHERE AYCode LIKE '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "' ", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    loadData();
                    MessageBox.Show("ACADEMIC/SEMESTER has been Opened", "Academic Year", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
            }
            else if (colName == "colClose")
            {
                if (MessageBox.Show("OPEN THIS ACADEMIC YEAR/SEMESTER?", "Academic Year", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("UPDATE [dbo].[tableAcadYear] SET Status = 'CLOSE' WHERE AYCode LIKE '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "' ", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    loadData();
                    MessageBox.Show("ACADEMIC/SEMESTER has been Closed", "Academic Year", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Academic Year", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void formAcademicYear_Load(object sender, EventArgs e)
        {
            loadData();
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.btnClose, "Close");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            formAcadYear fay = new formAcadYear(this);
            fay.ShowDialog();
        }
    }
}
