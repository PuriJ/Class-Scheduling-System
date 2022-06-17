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
    public partial class formSubject : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SchedDB db = new SchedDB();
        formSubjectList fsl;

        public formSubject(formSubjectList fsl)
        {
            InitializeComponent();
            con = new SqlConnection();
            con.ConnectionString = db.GetConnection();
            this.fsl = fsl;
        }

        public void Clear()
        {
            cbYLevel.Text = null;
            txtSubjCode.Clear();
            txtSubjName.Clear();
            txtUnits.Clear();
            cbType.Text = null;
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            cbYLevel.Focus();
        }

        public void addSuject()
        {
            try
            {
                if (MessageBox.Show("Save this Subject Information?", "Subject Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cbYLevel.Focus();
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO [dbo].[tableSubject](Year_Level, Subject_Code, Subject_Name, Units, Type) VALUES (@YearLevel, @SubCode, @SubName, @Units, @Type)", con);
                    cmd.Parameters.AddWithValue("@YearLevel", cbYLevel.Text);
                    cmd.Parameters.AddWithValue("@SubCode", txtSubjCode.Text);
                    cmd.Parameters.AddWithValue("@SubName", txtSubjName.Text);
                    cmd.Parameters.AddWithValue("@Units", txtUnits.Text);
                    cmd.Parameters.AddWithValue("@Type", cbType.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    fsl.loadData();
                    MessageBox.Show("Subject Information has been Saved.", "Subject Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Subject Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        public void updateSubject()
        {
            try
            {
                if (MessageBox.Show("Update this Subject Information?", "Subject Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("UPDATE [dbo].[tableSubject] SET Year_Level = @YearLevel, Subject_Code = @SubCode, " +
                                        "Subject_Name = @SubName, Units = @Units, Type = @Type WHERE Subject_ID = @SubjectID", con);
                    cmd.Parameters.AddWithValue("@YearLevel", cbYLevel.Text);
                    cmd.Parameters.AddWithValue("@SubCode", txtSubjCode.Text);
                    cmd.Parameters.AddWithValue("@SubName", txtSubjName.Text);
                    cmd.Parameters.AddWithValue("@Units", txtUnits.Text);
                    cmd.Parameters.AddWithValue("@Type", cbType.Text);
                    cmd.Parameters.AddWithValue("@SubjectID", lblSubjID.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    fsl.loadData();
                    MessageBox.Show("Subject Information has been Updated.", "Subject Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Subject Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbYLevel.Text))
            {
                MessageBox.Show("Fill up all the information", "Subject Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbYLevel.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtSubjCode.Text))
            {
                MessageBox.Show("Fill up all the information", "Subject Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSubjCode.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtSubjName.Text))
            {
                MessageBox.Show("Fill up all the information", "Subject Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSubjName.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtUnits.Text))
            {
                MessageBox.Show("Fill up all the information", "Subject Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUnits.Select();
                return;
            }
            if (string.IsNullOrEmpty(cbType.Text))
            {
                MessageBox.Show("Fill up all the information", "Subject Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbType.Select();
                return;
            }

            addSuject();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbYLevel.Text))
            {
                MessageBox.Show("Fill up all the information", "Subject Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbYLevel.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtSubjCode.Text))
            {
                MessageBox.Show("Fill up all the information", "Subject Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSubjCode.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtSubjName.Text))
            {
                MessageBox.Show("Fill up all the information", "Subject Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSubjName.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtUnits.Text))
            {
                MessageBox.Show("Fill up all the information", "Subject Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUnits.Select();
                return;
            }
            if (string.IsNullOrEmpty(cbType.Text))
            {
                MessageBox.Show("Fill up all the information", "Subject Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbType.Select();
                return;
            }

            updateSubject();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
