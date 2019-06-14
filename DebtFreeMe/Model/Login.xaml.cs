using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
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

namespace DebtFreeMe.Model
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        private object txtboxUsername;
        private object pboxPasswrd;

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                string sql = "SELECT COUNT(1) FROM Customer WHERE UserName=@UserName AND Passwrd=@Passwrd";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UserName", txtboxUsername.ToString());
                cmd.Parameters.AddWithValue("@Passwrd", pboxPasswrd.GetHashCode());
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 1)
                {
                    ///retrieve userID for User
                    /// MainWindow home = new MainWindow();
                    /// HomePage.Show();
                    /// this.Close();
                }
                else
                {
                    MessageBox.Show("Username or Password invalid");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            /// If Login is a page this should navigate to SignUp on click event
            /// this.NavigationService.Navigate(new SignUp());

            /// Open a new window displaying signup page
            /// MainWindow signup = new MainWindow();
            /// SignUp.show();
            this.Close();
        }
    }
}
