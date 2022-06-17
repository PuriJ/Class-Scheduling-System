using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Class_Scheduling_System
{
    class SchedDB
    {
        private string AcadYear;
        private string Semester;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public string GetConnection()
        {
            string cn = @"Data Source=mssql-36002-0.cloudclusters.net,36002;Initial Catalog=LMS;User ID=IT-R;Password=Qculms1234";
            return cn;
        }

        public string getAcadYear()
        {
            con = new SqlConnection();
            con.ConnectionString = GetConnection();
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableAcadYear] WHERE Status LIKE 'OPEN'", con);
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                AcadYear = dr["AYCode"].ToString();
            }
            else
            {
                AcadYear = "";
            }
            dr.Close();
            con.Close();
            return AcadYear;
        }

        public string getSem()
        {
            con = new SqlConnection();
            con.ConnectionString = GetConnection();
            con.Open();
            cmd = new SqlCommand("SELECT * FROM [dbo].[tableAcadYear] WHERE Status LIKE 'OPEN'", con);
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                Semester = dr["Year1st"].ToString() + " " + dr["Year2nd"].ToString() + " " + dr["Semester"].ToString();
            }
            else
            {
                Semester = "";
            }
            dr.Close();
            con.Close();
            return Semester;
        }

        public string getPrimaryKey(string sql)
        {
            string ID = string.Empty;
            con.Open();
            cmd = new SqlCommand(sql, con);
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                ID = dr[0].ToString();
            }
            dr.Close();
            con.Close();
            return ID;
        }

    }
}
