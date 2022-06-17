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
    public partial class formRoomList : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SchedDB db = new SchedDB();

        public formRoomList()
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
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableRoom]", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["Room_Code"].ToString(), dr["Room_Name"].ToString());
            }
            dr.Close();
            con.Close();
        }

        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colName = dataGridView1.Columns[e.ColumnIndex].Name;
                if (colName == "colDelete")
                {
                    if (MessageBox.Show("Delete this Room Information?", "Room List", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        con.Open();
                        cmd = new SqlCommand("DELETE [dbo].[tableRoom] WHERE Room_Code LIKE '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        loadData();
                        MessageBox.Show("Room Information Deleted.", "Room List", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Room List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            formRoom fr = new formRoom(this);
            fr.ShowDialog();
        }

        private void formRoomList_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        
    }
}
