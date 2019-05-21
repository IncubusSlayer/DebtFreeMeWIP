using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtFreeMe.Model
{
    class Account
    {
          public string CollectorName { get; set; }
          public float Balance { get; set; }
          public DateTime? AccountOpened { get; set; }
          public DateTime? AccountClosed { get; set; }

          public User User { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        //Selecting from DataTable
        public DataTable Selection()
        {
            //Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //Sql query
                string sql = "SELECT * FROM Accounts";
                //Creating cmd using conn and sql
                SqlCommand cmd = new SqlCommand(sql,conn);
                //Creating SQL data adapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);

            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        //Inserting data into Database
        public bool Insert(Account x)
        {
            //Creating default return type and setting its value to false
            bool isSuccess = false;

            //Step 1: Connect to database
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //Step 2: Create a SQL query to insert data
                string sql = "INSERT INTO Accounts(CollectorName, Balance) VALUES(@CollectorName,@Balance)";
                //Create SQL command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create parameters to add data
                cmd.Parameters.AddWithValue("@CollectorName", x.CollectorName);
                cmd.Parameters.AddWithValue("@Balance", x.Balance);

                //Open Connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if(rows>0)
                {

                }
                else
                {

                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
    }
}
