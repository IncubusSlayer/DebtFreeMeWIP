using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        private readonly object txtboxUsername;
        private readonly object pboxPasswrd;

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                string sql = "SELECT COUNT(1) FROM Customer WHERE UserName=@UserName AND Passwrd=@Passwrd";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@UserName", txtboxUsername.ToString());
                cmd.Parameters.AddWithValue("@Passwrd", pboxPasswrd.GetHashCode());
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 1)
                {
                    /// Need to retrieve userID for User
                    MainWindow homewindow = new MainWindow();
                    homewindow.Show();
                    this.Close();
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

            /// OR Open a new window displaying signup page

            /// OR OR have it display signup page NOT switch Windows
            /// 



        }
    }
}
