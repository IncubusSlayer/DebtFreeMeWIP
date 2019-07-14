using DebtFreeMe.Model;
using DebtFreeMeDFMEngine.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFMEngine.Models
{
    [Table("Customer")]
    public class UserModel : INotifyPropertyChanged
    {
        private int _UserID;
        [Key]
        public int UserID
        {
            get {return _UserID; }
            set
            {
                if (_UserID != value)
                    _UserID = value;
                OnPropertyChange(nameof(UserID));
            }
        }

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set
            {
                if (_UserName != value)
                    _UserName = value;
                OnPropertyChange(nameof(UserName));
            }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set
            {
                if (_Password != value)
                    _Password = value;
                OnPropertyChange(nameof(Password));
            }
        }

        private int _TotalAccounts;
        public int TotalAccounts
        {
            get { return _TotalAccounts; }
            set
            {
                if (_TotalAccounts != value)
                    _TotalAccounts = value;
                OnPropertyChange(nameof(TotalAccounts));
            }
        }

        private static string firstName;
        private static string lastName;
        private string _Name = firstName + " " + lastName;
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                    _Name = value;
                OnPropertyChange(nameof(Name));
            }
        }

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set
            {
                if (_Email != value)
                    _Email = value;
                OnPropertyChange(nameof(Email));
            }
        }

        private DateTime _DOB;
        public DateTime DOB
        {
            get { return _DOB; }
            set
            {
                if (_DOB != value)
                    _DOB = value;
                OnPropertyChange(nameof(DOB));
            }
        }

        [ForeignKey("UserID")]
        public virtual ICollection<Account> Accounts { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
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
