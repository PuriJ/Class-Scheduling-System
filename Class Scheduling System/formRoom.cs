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
    public partial class formRoom : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SchedDB db = new SchedDB();
        formRoomList frl;

        public formRoom(formRoomList frl)
        {
            InitializeComponent();
            con = new SqlConnection();
            con.ConnectionString = db.GetConnection();
            this.frl = frl;
        }

        public void Clear()
        {
            txtRoomID.Clear();
            txtRoomName.Clear();
        }

        public void addRoom()
        {
            try
            {
                if (MessageBox.Show("Save this Room Information?", "Room Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtRoomID.Focus();
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO [dbo].[tableRoom](Room_Code, Room_Name) VALUES (@RoomID, @RoomName)", con);
                    cmd.Parameters.AddWithValue("@RoomID", txtRoomID.Text);
                    cmd.Parameters.AddWithValue("@RoomName", txtRoomName.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    frl.loadData();
                    MessageBox.Show("Room Information has been Saved.", "Room Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Course Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRoomID.Text))
            {
                MessageBox.Show("Fill up all the information", "Room Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRoomID.Select();
                return;
            }

            if (string.IsNullOrEmpty(txtRoomName.Text))
            {
                MessageBox.Show("Fill up all the information", "Room Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRoomName.Select();
                return;
            }

            addRoom();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
