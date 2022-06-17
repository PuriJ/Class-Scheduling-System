using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Class_Scheduling_System.Connection
{
    class ServerConnection
    {
        //public static string stringConnection = @"Data Source=mssql-32433-0.cloudclusters.net,32433;Initial Catalog=LMS;User ID=QcuLmsR;Password=Qculms123";

        public static string stringConnection = @"Data Source=mssql-36002-0.cloudclusters.net,36002;Initial Catalog=LMS;User ID=IT-R;Password=Qculms1234";

        public static DataTable executeSQL(string sql)
        {
            SqlConnection con = new SqlConnection();
            SqlDataAdapter sda = default(SqlDataAdapter);
            DataTable dt = new DataTable();

            try
            {
                con.ConnectionString = stringConnection;
                con.Open();

                sda = new SqlDataAdapter(sql, con);
                sda.Fill(dt);

                con.Close();
                con = null;
                return dt;
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show("An error occured: " + e.Message, "Sql Server Connection Failed !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt = null;
            }
            return dt;

        }
    }
}
