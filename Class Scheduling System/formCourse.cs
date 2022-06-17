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
    public partial class formCourse : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SchedDB db = new SchedDB();
        formCourseList fcl;

        public formCourse(formCourseList fcl)
        {
            InitializeComponent();
            con = new SqlConnection();
            con.ConnectionString = db.GetConnection();
            this.fcl = fcl;
        }

        public void Clear()
        {
            txtCourse.Clear();
            txtDescription.Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            txtCourse.Focus();
        }

        public void addCourse()
        {
            try
            {
                if (MessageBox.Show("Save this Course Information?", "Course Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtCourse.Focus();
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO [dbo].[tableCourse](Course, Course_Description) VALUES (@Course, @Description)", con);
                    cmd.Parameters.AddWithValue("@Course", txtCourse.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    fcl.loadData();
                    MessageBox.Show("Course Information has been Saved.", "Course Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
            }
            catch(Exception)
            {
                con.Close();
                MessageBox.Show("Course Acronym already exist.\nInput different Course Acronym", "Course Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        public void updateCourse()
        {
            try
            {
                if (MessageBox.Show("Update this Course Information?", "Course Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtCourse.Focus();
                    con.Open();
                    cmd = new SqlCommand("UPDATE [dbo].[tableCourse] SET Course = @Course, Course_Description = @Description WHERE Course_ID = @CourseID", con);
                    cmd.Parameters.AddWithValue("@Course", txtCourse.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("CourseID", lblNum.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    fcl.loadData();
                    MessageBox.Show("Course Information has been Updated.", "Course Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
            }
            catch (Exception)
            {
                con.Close();
                MessageBox.Show("Course Acronym already exist.\nInput different Course Acronym", "Course Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCourse.Text))
            {
                MessageBox.Show("Fill up all the information", "Course Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCourse.Select();
                return;
            }

            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                MessageBox.Show("Fill up all the information", "Course Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDescription.Select();
                return;
            }

            addCourse();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCourse.Text))
            {
                MessageBox.Show("Fill up all the information", "Course Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCourse.Select();
                return;
            }

            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                MessageBox.Show("Fill up all the information", "Course Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDescription.Select();
                return;
            }

            updateCourse();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
