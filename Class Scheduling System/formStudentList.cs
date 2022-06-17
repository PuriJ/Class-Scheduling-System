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
    public partial class formStudentList : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SchedDB db = new SchedDB();
        int ID = 0;

        public formStudentList()
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

        private void delData()
        {
            if (ID != 0)
            {
                cmd = new SqlCommand("DELETE [dbo].[tableStudent] WHERE ID=@ID", con);
                con.Open();
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.ExecuteNonQuery();
                con.Close();
                loadData();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            formStudent fs = new formStudent(this);
            fs.btnUpdate.Enabled = false;
            fs.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void formStudentList_Load(object sender, EventArgs e)
        {
            loadData();
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.btnClose, "Close");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            /**try
            {

            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Verify Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }**/
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if(colName == "colEdit")
            {
                formStudent fs = new formStudent(this);
                con.Open();
                cmd = new SqlCommand("SELECT * FROM [dbo].[tableStudent] WHERE ID LIKE'"+ dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() +"' ", con);
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    fs.txtStudNum.Text = dr["Stud_ID"].ToString();
                    fs.cbYLevel.Text = dr["YearLevel"].ToString();
                    fs.txtLname.Text = dr["Lastname"].ToString();
                    fs.txtFname.Text = dr["Firstname"].ToString();
                    fs.txtMname.Text = dr["Middlename"].ToString();
                    fs.txtAge.Text = dr["Age"].ToString();
                    fs.dtBdate.Value = DateTime.Parse(dr["Birthdate"].ToString());
                    fs.txtPOB.Text = dr["BirthPlace"].ToString();
                    fs.txtContact.Text = dr["Contact"].ToString();
                    fs.cbGender.Text = dr["Gender"].ToString();
                    fs.cbStatus.Text = dr["Marital"].ToString();
                    fs.txtCitizenship.Text = dr["Citizenship"].ToString();
                    fs.txtReligion.Text = dr["Religion"].ToString();
                    fs.txtAddress.Text = dr["Address"].ToString();
                    fs.txtFather.Text = dr["Father"].ToString();
                    fs.txtMother.Text = dr["Mother"].ToString();
                    fs.txtFOccu.Text = dr["FOccupation"].ToString();
                    fs.txtMOccu.Text = dr["MOccupation"].ToString();
                    fs.txtPAddress.Text = dr["PAddress"].ToString();
                    fs.txtPContact.Text = dr["PContact"].ToString();
                    fs.txtSH.Text = dr["HS_Ave"].ToString();
                    fs.txtSHS.Text = dr["SHS_Ave"].ToString();
                    fs.dtGrad.Value = DateTime.Parse(dr["Graduation_Completion"].ToString());
                    fs.txtSchName.Text = dr["School_Name"].ToString();
                    fs.txtSchAdd.Text = dr["School_Address"].ToString();
                    fs.btnSave.Enabled = false;
                    fs.btnUpdate.Enabled = true;
                }
                dr.Close();
                con.Close();
                fs.ShowDialog();
            }
            else if (colName == "colDelete")
                {
                    if (MessageBox.Show("Delete this student information?", "Student Masterlist", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        con.Open();
                        cmd = new SqlCommand("DELETE [dbo].[tableStudent] WHERE ID LIKE '"+ dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Student Information Deleted.", "Student Masterlist", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadData();
                    }
                }

            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Student Masterlist", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableStudent] WHERE Stud_ID LIKE '" + txtSearch.Text + "%' OR Lastname LIKE '" + txtSearch.Text + "%'", con);
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
    }
}
