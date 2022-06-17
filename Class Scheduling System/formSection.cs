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
    public partial class formSection : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SchedDB db = new SchedDB();
        formSectionList fsl;

        public formSection(formSectionList fsl)
        {
            InitializeComponent();
            con = new SqlConnection();
            con.ConnectionString = db.GetConnection();
            this.fsl = fsl;
        }

        public void getCourse()
        {
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableCourse]", con);
            dr = cmd.ExecuteReader();
            cbCourse.Items.Clear();
            while (dr.Read())
            {
                cbCourse.Items.Add(dr["Course"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void Clear()
        {
            cbYLevel.Text = null;
            cbCourse.Text = null;
            txtSection.Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            cbYLevel.Focus();
        }

        public void addSection()
        {
            try
            {
                if (MessageBox.Show("Save this Section Information?", "Section Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO [dbo].[tableSection](Year, Course, Section) VALUES (@Year, @Course, @Section)", con);
                    cmd.Parameters.AddWithValue("@Year", cbYLevel.Text);
                    cmd.Parameters.AddWithValue("@Course", cbCourse.Text);
                    cmd.Parameters.AddWithValue("@Section", txtSection.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    fsl.loadData();
                    MessageBox.Show("Section Information has been Saved.", "Section Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Section Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void updateSection()
        {
            try
            {
                if (MessageBox.Show("Update this Section Information?", "Section Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cbYLevel.Focus();
                    con.Open();
                    cmd = new SqlCommand("UPDATE [dbo].[tableSection] SET Year = @Year, Course = @Course, Section = @Section WHERE Section_ID = @Section_ID", con);
                    cmd.Parameters.AddWithValue("@Year", cbYLevel.Text);
                    cmd.Parameters.AddWithValue("@Course", cbCourse.Text);
                    cmd.Parameters.AddWithValue("@Section", txtSection.Text);
                    cmd.Parameters.AddWithValue("@Section_ID", lblSecID.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    MessageBox.Show("Section Information has been Updated.", "Section Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fsl.loadData();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Section Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void formSection_Load(object sender, EventArgs e)
        {
            getCourse();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbYLevel.Text))
            {
                MessageBox.Show("Fill up all the information", "Course Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbYLevel.Select();
                return;
            }
            if (string.IsNullOrEmpty(cbCourse.Text))
            {
                MessageBox.Show("Fill up all the information", "Course Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCourse.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtSection.Text))
            {
                MessageBox.Show("Fill up all the information", "Course Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSection.Select();
                return;
            }

            addSection();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbYLevel.Text))
            {
                MessageBox.Show("Fill up all the information", "Course Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbYLevel.Select();
                return;
            }
            if (string.IsNullOrEmpty(cbCourse.Text))
            {
                MessageBox.Show("Fill up all the information", "Course Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCourse.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtSection.Text))
            {
                MessageBox.Show("Fill up all the information", "Course Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSection.Select();
                return;
            }

            updateSection();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
