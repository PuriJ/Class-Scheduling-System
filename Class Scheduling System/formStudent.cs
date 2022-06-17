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
    public partial class formStudent : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SchedDB db = new SchedDB();
        formStudentList fls;

        public formStudent(formStudentList fls)
        {
            InitializeComponent();
            con = new SqlConnection();
            con.ConnectionString = db.GetConnection();
            this.fls = fls;
        }

        public void SaveStudent()
        {
            try
            {
                string date = dtBdate.Value.ToShortDateString();
                if(MessageBox.Show("Save this Student's Information?", "Student Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO [dbo].[tableStudent](Stud_ID, YearLevel, Lastname, Firstname, Middlename, Age, Birthdate, BirthPlace, Contact, " +
                        "Gender, Marital, Citizenship, Religion, Address, Father, Mother, FOccupation, MOccupation, PAddress, PContact, HS_Completer, HS_Ave, SHS_Complete, " +
                        "SHS_Ave, Graduation_Completion, School_Name, School_Address) VALUES (@StudNo, @YrLvl, @Lname, @Fname, @Mname, @Age, @Bday, @Bplace, @Contact, " +
                        "@Gender, @Marital, @Citizen, @Religion, @Address, @Father, @Mother, @FOccu, @MOccu, @PAddress, @PContact, @HSCom, @HSAve, @SHSCom, @SHSAve, " +
                        "@Grad, @SchName, @SchAddress)", con);
                    cmd.Parameters.AddWithValue("@StudNo", txtStudNum.Text);
                    cmd.Parameters.AddWithValue("@YrLvl", cbYLevel.Text);
                    cmd.Parameters.AddWithValue("@Lname", txtLname.Text);
                    cmd.Parameters.AddWithValue("@Fname", txtFname.Text);
                    cmd.Parameters.AddWithValue("@Mname", txtMname.Text);
                    cmd.Parameters.AddWithValue("@Age", txtAge.Text);
                    cmd.Parameters.AddWithValue("@Bday", Convert.ToDateTime(date));
                    cmd.Parameters.AddWithValue("@Bplace", txtPOB.Text);
                    cmd.Parameters.AddWithValue("@Contact", txtContact.Text);
                    cmd.Parameters.AddWithValue("@Gender", cbGender.Text);
                    cmd.Parameters.AddWithValue("@Marital", cbStatus.Text);
                    cmd.Parameters.AddWithValue("@Citizen", txtCitizenship.Text);
                    cmd.Parameters.AddWithValue("@Religion", txtReligion.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@Father", txtFather.Text);
                    cmd.Parameters.AddWithValue("@Mother", txtMother.Text);
                    cmd.Parameters.AddWithValue("@FOccu", txtFOccu.Text);
                    cmd.Parameters.AddWithValue("@MOccu", txtMOccu.Text);
                    cmd.Parameters.AddWithValue("@PAddress", txtPAddress.Text);
                    cmd.Parameters.AddWithValue("@PContact", txtPContact.Text);
                    cmd.Parameters.AddWithValue("@HSCom", chkHSC.Text);
                    cmd.Parameters.AddWithValue("@HSAve", txtSH.Text);
                    cmd.Parameters.AddWithValue("@SHSCom", chkSHC.Text);
                    cmd.Parameters.AddWithValue("@SHSAve", txtSHS.Text);
                    cmd.Parameters.AddWithValue("@Grad", Convert.ToDateTime(dtGrad.Text));
                    cmd.Parameters.AddWithValue("@SchName", txtSchName.Text);
                    cmd.Parameters.AddWithValue("@SchAddress", txtSchAdd.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Clear();

                    MessageBox.Show("Student's Information has been Saved.", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fls.loadData();
                }
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void updateStudent()
        {
            try
            {
                if (MessageBox.Show("Update this student information?", "Student Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("UPDATE [dbo].[tableStudent] SET YearLevel = @YrLvl, Lastname = @Lname, Firstname = @Fname, Middlename = @Mname, Age = @Age, " +
                        "Birthdate = @Bday, BirthPlace = @Bplace, Contact = @Contact, Gender = @Gender, Marital = @Marital, Citizenship = @Citizen, Religion = @Religion, " +
                        "Address = @Address, Father = @Father, Mother = @Mother, FOccupation = @FOccu, MOccupation = @MOccu, PAddress = @PAddress, PContact = @PContact, " +
                        "HS_Completer = @HSCom, HS_Ave = @HSAve, SHS_Complete = @SHSCom, SHS_Ave = @SHSAve, Graduation_Completion = @Grad, School_Name = @SchName, " +
                        "School_Address = @SchAddress WHERE Stud_ID = @StudNo", con);

                    cmd.Parameters.AddWithValue("@YrLvl", cbYLevel.Text);
                    cmd.Parameters.AddWithValue("@Lname", txtLname.Text);
                    cmd.Parameters.AddWithValue("@Fname", txtFname.Text);
                    cmd.Parameters.AddWithValue("@Mname", txtMname.Text);
                    cmd.Parameters.AddWithValue("@Age", txtAge.Text);
                    cmd.Parameters.AddWithValue("@Bday", Convert.ToDateTime(dtBdate.Text));
                    cmd.Parameters.AddWithValue("@Bplace", txtPOB.Text);
                    cmd.Parameters.AddWithValue("@Contact", txtContact.Text);
                    cmd.Parameters.AddWithValue("@Gender", cbGender.Text);
                    cmd.Parameters.AddWithValue("@Marital", cbStatus.Text);
                    cmd.Parameters.AddWithValue("@Citizen", txtCitizenship.Text);
                    cmd.Parameters.AddWithValue("@Religion", txtReligion.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@Father", txtFather.Text);
                    cmd.Parameters.AddWithValue("@Mother", txtMother.Text);
                    cmd.Parameters.AddWithValue("@FOccu", txtFOccu.Text);
                    cmd.Parameters.AddWithValue("@MOccu", txtMOccu.Text);
                    cmd.Parameters.AddWithValue("@PAddress", txtPAddress.Text);
                    cmd.Parameters.AddWithValue("@PContact", txtPContact.Text);
                    cmd.Parameters.AddWithValue("@HSCom", chkHSC.Text);
                    cmd.Parameters.AddWithValue("@HSAve", txtSH.Text);
                    cmd.Parameters.AddWithValue("@SHSCom", chkSHC.Text);
                    cmd.Parameters.AddWithValue("@SHSAve", txtSHS.Text);
                    cmd.Parameters.AddWithValue("@Grad", Convert.ToDateTime(dtGrad.Text));
                    cmd.Parameters.AddWithValue("@SchName", txtSchName.Text);
                    cmd.Parameters.AddWithValue("@SchAddress", txtSchAdd.Text);
                    cmd.Parameters.AddWithValue("@StudNo", txtStudNum.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    MessageBox.Show("Student Information has been Updated.", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fls.loadData();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        

        public void Clear()
        {
            txtStudNum.Clear();
            txtStudNum.Focus();
            cbYLevel.SelectedIndex = -1;
            txtLname.Clear();
            txtFname.Clear();
            txtMname.Clear();
            txtAge.Clear();
            dtBdate.Value = DateTime.Now;
            txtPOB.Clear();
            txtContact.Clear();
            cbGender.SelectedIndex = -1;
            cbStatus.SelectedIndex = -1;
            txtCitizenship.Clear();
            txtReligion.Clear();
            txtAddress.Clear();
            txtFather.Clear();
            txtMother.Clear();
            txtFOccu.Clear();
            txtMOccu.Clear();
            txtPAddress.Clear();
            txtPContact.Clear();
            chkHSC.Checked = false;
            txtSH.Clear();
            chkSHC.Checked = false;
            txtSHS.Clear();
            dtGrad.Value = DateTime.Now;
            txtSchName.Clear();
            txtSchAdd.Clear();
        }

        int Years(DateTime start, DateTime end)
        {
            return (end.Year - start.Year - 1) +
                (((end.Month > start.Month) ||
                ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtStudNum.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtStudNum.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtStudNum.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtStudNum.Select();
                return;
            }
            if (string.IsNullOrEmpty(cbYLevel.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbYLevel.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtLname.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLname.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtFname.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFname.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtMname.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMname.Select();
                return;
            }
            if (string.IsNullOrEmpty(dtBdate.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtBdate.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtPOB.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPOB.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtContact.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtContact.Select();
                return;
            }
            if (string.IsNullOrEmpty(cbGender.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbGender.Select();
                return;
            }
            if (string.IsNullOrEmpty(cbStatus.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbStatus.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtCitizenship.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCitizenship.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtReligion.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtReligion.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAddress.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtFather.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFather.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtMother.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMother.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtFOccu.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFOccu.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtMOccu.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMOccu.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtPAddress.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPAddress.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtPContact.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPContact.Select();
                return;
            }
            if (string.IsNullOrEmpty(dtGrad.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtGrad.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtSchName.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSchName.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtSchAdd.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSchAdd.Select();
                return;
            }
            
            SaveStudent();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtStudNum.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtStudNum.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtStudNum.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtStudNum.Select();
                return;
            }
            if (string.IsNullOrEmpty(cbYLevel.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbYLevel.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtLname.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLname.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtFname.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFname.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtMname.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMname.Select();
                return;
            }
            if (string.IsNullOrEmpty(dtBdate.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtBdate.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtPOB.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPOB.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtContact.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtContact.Select();
                return;
            }
            if (string.IsNullOrEmpty(cbGender.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbGender.Select();
                return;
            }
            if (string.IsNullOrEmpty(cbStatus.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbStatus.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtCitizenship.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCitizenship.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtReligion.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtReligion.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAddress.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtFather.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFather.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtMother.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMother.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtFOccu.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFOccu.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtMOccu.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMOccu.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtPAddress.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPAddress.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtPContact.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPContact.Select();
                return;
            }
            if (string.IsNullOrEmpty(dtGrad.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtGrad.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtSchName.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSchName.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtSchAdd.Text))
            {
                MessageBox.Show("Fill up all the information", "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSchAdd.Select();
                return;
            }

            updateStudent();
        }

        private void dtBdate_ValueChanged(object sender, EventArgs e)
        {
            txtAge.Text = Years(dtBdate.Value.Date, DateTime.Now.Date).ToString();
        }

        private void dtBdate_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtStudNum_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void formStudent_Load(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.btnClose, "Close");
        }
    }
}
