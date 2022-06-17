using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Class_Scheduling_System
{
    public partial class Adminform : Form
    {
        public Adminform()
        {
            InitializeComponent();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Close this Form?", "Admin Form", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
        Point lastPoint;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void mnVerifyAccount_Click(object sender, EventArgs e)
        {
            VerifyAccount VA = new VerifyAccount();
            VA.TopLevel = false;
            panel3.Controls.Add(VA);
            VA.loadData();
            VA.BringToFront();
            VA.Show();
        }

        private void mnUserAccount_Click(object sender, EventArgs e)
        {
            formUserAccount fua = new formUserAccount();
            fua.TopLevel = false;
            panel3.Controls.Add(fua);
            fua.loadData();
            fua.BringToFront();
            fua.Show();
        }

        private void mnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to logout?", "Admin Form", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
            this.Hide();

            Login lg = new Login();
            lg.ShowDialog();
            lg = null;

            this.Dispose();
            }
            
        }

        private void menuStudent_Click(object sender, EventArgs e)
        {
            formStudentList fsl = new formStudentList();
            fsl.TopLevel = false;
            panel3.Controls.Add(fsl);
            fsl.BringToFront();
            fsl.Show();
        }

        private void menuProfessor_Click(object sender, EventArgs e)
        {
            formProfessorList fpl = new formProfessorList();
            fpl.TopLevel = false;
            panel3.Controls.Add(fpl);
            fpl.BringToFront();
            fpl.Show();
        }

        private void menuStudSched_Click(object sender, EventArgs e)
        {
            formStudentScheduleList fssl = new formStudentScheduleList();
            fssl.TopLevel = false;
            panel3.Controls.Add(fssl);
            fssl.BringToFront();
            fssl.Show();
        }

        private void menuProfSched_Click(object sender, EventArgs e)
        {
            formProfessorScheduleList fpsl = new formProfessorScheduleList();
            fpsl.TopLevel = false;
            panel3.Controls.Add(fpsl);
            fpsl.BringToFront();
            fpsl.Show();
        }

        private void menuSubject_Click(object sender, EventArgs e)
        {
            formSubjectList fsl = new formSubjectList();
            fsl.TopLevel = false;
            panel3.Controls.Add(fsl);
            fsl.BringToFront();
            fsl.Show();
        }

        private void recordStudent_Click(object sender, EventArgs e)
        {
            formStudentRecord fsr = new formStudentRecord();
            fsr.TopLevel = false;
            panel3.Controls.Add(fsr);
            fsr.BringToFront();
            fsr.Show();
        }

        private void recordStudSched_Click(object sender, EventArgs e)
        {
            formStudentScheduleRecord fssr = new formStudentScheduleRecord();
            fssr.TopLevel = false;
            panel3.Controls.Add(fssr);
            fssr.BringToFront();
            fssr.Show();
        }

        private void recordProf_Click(object sender, EventArgs e)
        {
            formProfessorRecord fpr = new formProfessorRecord();
            fpr.TopLevel = false;
            panel3.Controls.Add(fpr);
            fpr.BringToFront();
            fpr.Show();
        }

        private void recordProfSched_Click(object sender, EventArgs e)
        {
            formProfessorScheduleRecord fpsr = new formProfessorScheduleRecord();
            fpsr.TopLevel = false;
            panel3.Controls.Add(fpsr);
            fpsr.BringToFront();
            fpsr.Show();
        }

        private void mtAcadYear_Click(object sender, EventArgs e)
        {
            formAcademicYear fay = new formAcademicYear();
            fay.TopLevel = false;
            panel3.Controls.Add(fay);
            fay.BringToFront();
            fay.Show();
        }

        private void mtCourse_Click(object sender, EventArgs e)
        {
            formCourseList fcl = new formCourseList();
            fcl.TopLevel = false;
            panel3.Controls.Add(fcl);
            fcl.BringToFront();
            fcl.Show();
        }

        private void mtSection_Click(object sender, EventArgs e)
        {
            formSectionList fsl = new formSectionList();
            fsl.TopLevel = false;
            panel3.Controls.Add(fsl);
            fsl.BringToFront();
            fsl.Show();
        }

        private void mtRoom_Click(object sender, EventArgs e)
        {
            formRoomList frl = new formRoomList();
            frl.TopLevel = false;
            panel3.Controls.Add(frl);
            frl.BringToFront();
            frl.Show();
        }

        private void mtCampus_Click(object sender, EventArgs e)
        {
            formCampusList fcl = new formCampusList();
            fcl.TopLevel = false;
            panel3.Controls.Add(fcl);
            fcl.BringToFront();
            fcl.Show();
        }
    }
}
