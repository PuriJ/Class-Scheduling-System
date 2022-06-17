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
    public partial class formAcadYear : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SchedDB db = new SchedDB();
        formAcademicYear fay;

        public formAcadYear(formAcademicYear fay)
        {
            InitializeComponent();
            con = new SqlConnection();
            con.ConnectionString = db.GetConnection();
            this.fay = fay;
        }

        public void Clear()
        {
            txtYear1.Clear();
            txtYear2.Clear();
            cbSem.Text = null;
            txtYear1.Focus();
        }


        public void addAY()
        {
            try
            {
                string ayCode = txtYear1.Text + " " + txtYear2.Text + " " + cbSem.Text;
                if (txtYear1.Text == String.Empty || txtYear2.Text == String.Empty || cbSem.Text == "")
                {
                    MessageBox.Show("FILL UP THIS FIELD.", "Academic Year", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Save this Academic Year?", "Academic Year", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("UPDATE [dbo].[tableAcadYear] SET Status = 'CLOSE'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    cmd = new SqlCommand("INSERT INTO [dbo].[tableAcadYear](AYCode, Year1st, Year2nd, Semester) VALUES (@aycode, @y1st, @y2nd, @sem)", con);
                    cmd.Parameters.AddWithValue("@aycode", ayCode);
                    cmd.Parameters.AddWithValue("@y1st", txtYear1.Text);
                    cmd.Parameters.AddWithValue("@y2nd", txtYear2.Text);
                    cmd.Parameters.AddWithValue("@sem", cbSem.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Clear();

                    MessageBox.Show("Academic Year has been Added", "Academic Year", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fay.loadData();
                }
            }
            catch (Exception)
            {
                con.Close();
                MessageBox.Show("This Academic Year is already exist.\nInput different Academic Year.", "Academic Year", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            

        }

        private void txtYear1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtYear2.Text = (long.Parse(txtYear1.Text) + 1).ToString();
            }
            catch (Exception)
            {
                txtYear2.Clear();
            }
        }

        private void txtYear1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            addAY();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void formAcadYear_Load(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.btnClose, "Close");
        }
    }
}
