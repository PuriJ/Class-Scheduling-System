using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Class_Scheduling_System
{
    public partial class formProfessor : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SchedDB db = new SchedDB();
        formProfessorList fpl;

        public formProfessor(formProfessorList fpl)
        {
            InitializeComponent();
            con = new SqlConnection();
            con.ConnectionString = db.GetConnection();
            this.fpl = fpl;
        }

        public void Clear()
        {
            txtProfessorID.Clear();
            txtLastname.Clear();
            txtFirstname.Clear();
            txtMiddlename.Clear();
            txtUnderGradDegree.Clear();
            txtGraduateDegree.Clear();
            txtSpecialization.Clear();
        }

        public void addProfessor()
        {
            try
            {
                if (MessageBox.Show("Save this Professor's Information?", "Professor Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO [dbo].[tableProfessor] (Professor_ID, Lastname, Firstname, Middlename, Under_Graduate_Degree, " +
                                            "Graduate_Degree, Specialization) VALUES (@ProfID, @LName, @Fname, @MName, @UGDegree, @GDegree, @Specialization)", con);
                    cmd.Parameters.AddWithValue("@ProfID", txtProfessorID.Text);
                    cmd.Parameters.AddWithValue("@LName", txtLastname.Text);
                    cmd.Parameters.AddWithValue("@Fname", txtFirstname.Text);
                    cmd.Parameters.AddWithValue("@MName", txtMiddlename.Text);
                    cmd.Parameters.AddWithValue("@UGDegree", txtUnderGradDegree.Text);
                    cmd.Parameters.AddWithValue("@GDegree", txtGraduateDegree.Text);
                    cmd.Parameters.AddWithValue("@Specialization", txtSpecialization.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    MessageBox.Show("Professor's Information has been Saved.", "Professor Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fpl.loadData();
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Professor Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void updateProfessor()
        {
            try
            {
                if (MessageBox.Show("Update this Professor Information?", "Professor Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("UPDATE [dbo].[tableProfessor] SET Lastname = @LName, Firstname = @Fname, Middlename = @MName, " +
                                            "Under_Graduate_Degree = @UGDegree, Graduate_Degree = @GDegree, Specialization = @Specialization " +
                                            "WHERE Professor_ID = @ProfID", con);
                    cmd.Parameters.AddWithValue("@LName", txtLastname.Text);
                    cmd.Parameters.AddWithValue("@Fname", txtFirstname.Text);
                    cmd.Parameters.AddWithValue("@MName", txtMiddlename.Text);
                    cmd.Parameters.AddWithValue("@UGDegree", txtUnderGradDegree.Text);
                    cmd.Parameters.AddWithValue("@GDegree", txtGraduateDegree.Text);
                    cmd.Parameters.AddWithValue("@Specialization", txtSpecialization.Text);
                    cmd.Parameters.AddWithValue("@ProfID", txtProfessorID.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    MessageBox.Show("Professor Information has been Updated.", "Professor Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fpl.loadData();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Professor Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProfessorID.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtProfessorID.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtLastname.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLastname.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtFirstname.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFirstname.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtMiddlename.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMiddlename.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtUnderGradDegree.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUnderGradDegree.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtGraduateDegree.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGraduateDegree.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtSpecialization.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSpecialization.Select();
                return;
            }

            addProfessor();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProfessorID.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtProfessorID.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtLastname.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLastname.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtFirstname.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFirstname.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtMiddlename.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMiddlename.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtUnderGradDegree.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUnderGradDegree.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtGraduateDegree.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGraduateDegree.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtSpecialization.Text))
            {
                MessageBox.Show("Fill up all the information", "Professor Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSpecialization.Select();
                return;
            }

            updateProfessor();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtProfessorID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void formProfessor_Load(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.btnClose, "Close");
        }
    }
}