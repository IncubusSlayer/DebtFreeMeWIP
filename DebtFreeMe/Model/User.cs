﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtFreeMe.Model
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; private set; }
        public int TotalAccounts { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public ICollection<Account> Accounts { get; set; }

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

        //Inserting data into Database
        public bool Insert(User user)
        {
            const string sql = "INSERT INTO Customer(UserName, Passwrd, UserID, TotalAccounts, Name, Email,DOB) VALUES(@UserName,@Passwrd,@UserID,@TotalAccounts,@Name,@Email,@DOB)";

            int rows = DataManipulation(sql,
                new string[] { "@UserName", "@Passwrd","@UserID", "@TotalAccounts", "@Name", "@Email", "@DOB" },
                new object[] { user.UserName, user.Password, user.UserID, user.TotalAccounts, user.Name, user.Email, user.DOB });

            return rows > 0;
        }

        // Selecting from database
        public DataTable Selection()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM Customer";
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
    }
}
