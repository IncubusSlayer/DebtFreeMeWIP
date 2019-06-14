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

namespace DebtFreeMe.Model
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;


        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            conn.Open();



        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
