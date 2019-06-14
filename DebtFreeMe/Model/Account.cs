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
    public class Account
     {
        public string CollectorName { get; set; }
        public float Balance { get; set; }
        public DateTime AccountOpened { get; set; }
        public DateTime AccountClosed { get; set; }
        public int CollectionID { get; set; }
        public int UserID { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        int DataManipulation(string sql, string[] paramList, object[] paramValues)
        {
            if (string.IsNullOrEmpty(sql))
                throw new ArgumentException("sql");

            if (paramList == null)
                throw new ArgumentNullException("paramList");

            if (paramValues == null)
                throw new ArgumentNullException("paramValues");

            if (paramList.Length != paramValues.Length)
                throw new ArgumentException("paramValues and paramList have different lengths");

            using (var conn = new SqlConnection(myconnstrng))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    for (int i = 0; i < paramList.Length; i++)
                    {
                        cmd.Parameters.AddWithValue(paramList[i], paramValues[i]);
                    }

                    return cmd.ExecuteNonQuery();
                }
            }
        }
        // Selecting from database
        public DataTable Selection()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM Accounts";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        //Inserting data into Database
        public bool Insert(Account account)
        {
            const string sql = "INSERT INTO Accounts(CollectorName, Balance) VALUES(@CollectorName,@Balance)";

            int rows = DataManipulation(sql,
                new string[] { "@CollectorName", "@Balance" },
                new object[] { account.CollectorName, account.Balance });
               
            return rows > 0;
        }

        //Method updates data in database from application
        public bool Update(Account account)
        {
            const string sql = "UPDATE Accounts SET CollectorName=@CollectorName, Balance=@Balance WHERE CollectionID=@CollectionID";

            int rows = DataManipulation(sql,
                new string[] { "@CollectorName", "@Balance" },
                new object[] { account.CollectorName, account.Balance });

            return rows > 0;
        }
    }
}
