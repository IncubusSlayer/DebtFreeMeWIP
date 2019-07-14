using DebtFreeMe.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DebtFreeMe.Views
{
    /// <summary>
    /// Interaction logic for SignUpView.xaml
    /// </summary>
    public partial class SignUpView : UserControl
    {
        public SignUpView()
        {
            InitializeComponent();
        }

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
        public bool Insert(UserModel user)
        {
            const string sql = "INSERT INTO Customer(UserName, Passwrd, UserID, TotalAccounts, Name, Email, DOB) VALUES(@UserName,@Passwrd,@UserID,@TotalAccounts,@Name,@Email,@DOB)";

            int rows = DataManipulation(sql,
                new string[] { "@UserName", "@Passwrd", "@UserID", "@TotalAccounts", "@Name", "@Email", "@DOB" },
                new object[] { user.UserName, user.Password, user.UserID, user.TotalAccounts, user.Name, user.Email, user.DOB });

            return rows > 0;
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            /// Call 'ifUserNameVerification' function
            /// if it doesnt already exist then send data to SQL
            /// then Navigate back to Login window for sign in
            /// close this window and page

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            /// Navigate bact to Login page 
        }

    }
}
