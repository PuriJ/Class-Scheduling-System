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
    public partial class formProfessorSchedule : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SchedDB db = new SchedDB();
        formProfessorScheduleList fpsl;

        public formProfessorSchedule(formProfessorScheduleList fpsl)
        {
            InitializeComponent();
            con = new SqlConnection();
            con.ConnectionString = db.GetConnection();
            txtYearSem.Text = db.getSem();
            this.fpsl = fpsl;
        }

        public void getSection()
        {
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableSection]", con);
            dr = cmd.ExecuteReader();
            cbCourse.Items.Clear();
            while (dr.Read())
            {
                cbCourse.Items.Add(dr["Course"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void getSubject()
        {
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableSubject]", con);
            dr = cmd.ExecuteReader();
            cbCourse.Items.Clear();
            while (dr.Read())
            {
                cbSubjCode.Items.Add(dr["Subject_Code"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void getProfessor()
        {
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableProfessor]", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cbProfessorNumber.Items.Add(dr["Professor_ID"].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void getRoom()
        {
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableRoom]", con);
            dr = cmd.ExecuteReader();
            cbRoom.Items.Clear();
            while (dr.Read())
            {

                cbRoom.Items.Add(dr["Room_Code"].ToString());

            }
            dr.Close();
            con.Close();
        }

        public void getCampus()
        {
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableCampus]", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cbCampus.Items.Add(dr["Campus_Name"].ToString());
            }
            dr.Close();
            con.Close();
        }

        // CLEAR ALL THE INPUTS AFTER ADDING/UPDATING THE INFORMATION
        public void Clear()
        {
            cbProfessorNumber.Text = null;
            txtLname.Clear();
            txtFname.Clear();
            txtMname.Clear();

            cbYearLevel.Text = null;
            cbCourse.Text = null;
            cbSection.Text = null;

            cbSubjCode.Text = null;
            txtSubjDesc.Clear();
            txtUnits.Clear();
            cbRoom.Text = null;

            cbDays.Text = null;
            mtTimeFrom.Clear();
            mtTimeTo.Clear();
            cbCampus.Text = null;

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        // ADDING PROFESSOR SCHEDULE
        public void addProfSched()
        {
            try
            {
                string SectionID = db.getPrimaryKey("SELECT Section_ID FROM [dbo].[tableSection] WHERE Section LIKE '" + cbSection.Text + "'");
                string SubjectID = db.getPrimaryKey("SELECT Subject_ID FRom [dbo].[tableSubject] WHERE Subject_Code LIKE '" + cbSubjCode.Text + "'");
                if (MessageBox.Show("Save this Professor Schedule Information?", "Professor Schedule Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO [dbo].[tableProfSchedule] (Professor_ID, AYCode, Section_ID, Subject_ID, " +
                        "Room_Code, Days, timeFrom, timeTo, Campus) VALUES (@ProfID, @AYCode, @Section, @Subject, @Room, @Days," +
                        "@timeFrom, @timeTo, @Campus)", con);
                    cmd.Parameters.AddWithValue("@ProfID", cbProfessorNumber.Text);
                    cmd.Parameters.AddWithValue("@AYCode", txtYearSem.Text);
                    cmd.Parameters.AddWithValue("@Section", SectionID);
                    cmd.Parameters.AddWithValue("@Subject", SubjectID);
                    cmd.Parameters.AddWithValue("@Room", cbRoom.Text);
                    cmd.Parameters.AddWithValue("@Days", cbDays.Text);
                    cmd.Parameters.AddWithValue("@timeFrom", mtTimeFrom.Text.ToString());
                    cmd.Parameters.AddWithValue("@timeTo", mtTimeTo.Text.ToString());
                    cmd.Parameters.AddWithValue("@Campus", cbCampus.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    fpsl.loadData();
                    MessageBox.Show("Professor Schedule Information has been Saved.", "Professor Schedule Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Professor Schedule Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // UPDATING CLASS
        public void updateProfSched()
        {
            try
            {
                string SectionID = db.getPrimaryKey("SELECT Section_ID FROM [dbo].[tableSection] WHERE Section LIKE '" + cbSection.Text + "'");
                string SubjectID = db.getPrimaryKey("SELECT Subject_ID FRom [dbo].[tableSubject] WHERE Subject_Code LIKE '" + cbSubjCode.Text + "'");
                if (MessageBox.Show("Update this Professor Schedule Information?", "Professor Schedule Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("UPDATE [dbo].[tableProfSchedule] SET AYCode = @AYCode, Section_ID = @Section, Subject_ID = @Subject, " +
                        "Room_Code = @Room, Days = @Days, timeFrom = @timeFrom, timeTo = @timeTo, Campus = @Campus WHERE PSchedule_ID = @PSchedID", con);
                    cmd.Parameters.AddWithValue("@AYCode", txtYearSem.Text);
                    cmd.Parameters.AddWithValue("@SectionID", SectionID);
                    cmd.Parameters.AddWithValue("@SubjectID", SubjectID);
                    cmd.Parameters.AddWithValue("@Room", cbRoom.Text);
                    cmd.Parameters.AddWithValue("@Days", cbDays.Text);
                    cmd.Parameters.AddWithValue("@timeFrom", mtTimeFrom.Text);
                    cmd.Parameters.AddWithValue("@timeTo", mtTimeTo.Text);
                    cmd.Parameters.AddWithValue("@Campus", cbCampus.Text);
                    cmd.Parameters.AddWithValue("@PSchedID", lblProfID.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    fpsl.loadData();
                    MessageBox.Show("Student Schedule Information has been Updated.", "Student Schedule Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Professor Schedule Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // SAVING INFORMATION
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbProfessorNumber.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Schedule Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbProfessorNumber.Select();
                return;
            }

            if (string.IsNullOrEmpty(cbCourse.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Schedule Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCourse.Select();
                return;
            }

            if (string.IsNullOrEmpty(cbYearLevel.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Schedule Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbYearLevel.Select();
                return;
            }

            if (string.IsNullOrEmpty(cbSection.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Schedule Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbSection.Select();
                return;
            }

            if (string.IsNullOrEmpty(cbSubjCode.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Schedule Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbSubjCode.Select();
                return;
            }

            if (string.IsNullOrEmpty(cbRoom.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Schedule Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbRoom.Select();
                return;
            }

            if (string.IsNullOrEmpty(cbDays.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Schedule Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbDays.Select();
                return;
            }

            if (string.IsNullOrEmpty(cbCampus.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Schedule Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCampus.Select();
                return;
            }

            addProfSched();
        }

        // UPDATING INFORMATION
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbProfessorNumber.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Schedule Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbProfessorNumber.Select();
                return;
            }

            if (string.IsNullOrEmpty(cbCourse.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Schedule Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCourse.Select();
                return;
            }

            if (string.IsNullOrEmpty(cbYearLevel.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Schedule Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbYearLevel.Select();
                return;
            }

            if (string.IsNullOrEmpty(cbSection.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Schedule Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbSection.Select();
                return;
            }

            if (string.IsNullOrEmpty(cbSubjCode.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Schedule Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbSubjCode.Select();
                return;
            }

            if (string.IsNullOrEmpty(cbRoom.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Schedule Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbRoom.Select();
                return;
            }

            if (string.IsNullOrEmpty(cbDays.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Schedule Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbDays.Select();
                return;
            }

            if (string.IsNullOrEmpty(cbCampus.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Schedule Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCampus.Select();
                return;
            }

            updateProfSched();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        // TO CALL ALL THE DATA FROM THE FORM VIEW
        private void formProfessorSchedule_Load(object sender, EventArgs e)
        {
            getSection();
            getSubject();
            getProfessor();
            getRoom();
            getCampus();
        }

        // PROFESSOR LEVEL
        private void cbProfessorNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableProfessor] WHERE Professor_ID = '"+ cbProfessorNumber.Text +"'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtLname.Text = dr[1].ToString();
                txtFname.Text = dr[2].ToString();
                txtMname.Text = dr[3].ToString();
            }
            dr.Close();
            con.Close();
        }

        // SECTION LEVEL
        private void cbYearLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableSection] WHERE Year = '" + cbYearLevel.Text + "'", con);
            dr = cmd.ExecuteReader();
            cbCourse.Items.Clear();
            cbSection.Items.Clear();
            while (dr.Read())
            {
                if (!cbSection.Items.Contains(dr[3].ToString()) || !cbCourse.Items.Contains(dr[2].ToString()))
                {
                    cbCourse.Items.Add(dr[2].ToString());
                    cbCourse.Text = dr[2].ToString();

                    cbSection.Items.Add(dr[3].ToString());
                    cbSection.Text = dr[3].ToString();
                }
            }
            dr.Close();
            con.Close();
        }

        // SUBJECT LEVEL
        private void cbSubjCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableSubject] WHERE Subject_Code = '" + cbSubjCode.Text + "'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtSubjDesc.Text = dr[3].ToString();
                txtUnits.Text = dr[4].ToString();
            }
            dr.Close();
            con.Close();
        }

        //ROOM LEVEL
        private void cbRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableRoom] WHERE Room_Code = '" + cbRoom.Text + "'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (!cbRoom.Items.Contains(dr[0].ToString()))
                {
                    cbRoom.Items.Add(dr[0].ToString());
                    cbRoom.Text = dr[0].ToString();
                }

            }
            dr.Close();
            con.Close();
        }
        
        // CAMPUS LEVEL
        private void cbCampus_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableCampus] WHERE Campus_Name = '" + cbCampus.Text + "'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (!cbCampus.Items.Contains(dr[1].ToString()))
                {
                    cbCampus.Items.Add(dr[1].ToString());
                    cbCampus.Text = dr[1].ToString();
                }
            }
            dr.Close();
            con.Close();
        }
    }
}
