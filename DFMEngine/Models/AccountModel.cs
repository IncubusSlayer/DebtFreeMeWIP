using DebtFreeMe.Model;
using DebtFreeMeDFMEngine.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFMEngine.Models
{
    [Table("Accounts")]
    public class AccountModel : INotifyPropertyChanged
     {
        private string _CollectorName;
        public string CollectorName
        {
            get { return _CollectorName; }
            set
            {
                if (_CollectorName != value)
                    _CollectorName = value;
            }
        }

        private float _Balance;
        public float Balance
        {
            get { return _Balance; }
            set
            {
                if (_Balance != value)
                    _Balance = value;
                OnPropertyChange(nameof(Balance));
            }
        }

        private DateTime _AccountOpened;
        public DateTime AccountOpened
        {
            get { return _AccountOpened; }
            set
            {
                if (_AccountOpened != value)
                    _AccountOpened = value;
            }
        }

        private DateTime _AccountClosed;
        public DateTime AccountClosed
        {
            get { return _AccountClosed; }
            set
            {
                if (_AccountClosed != value)
                    _AccountClosed = value;
            }
        }

        private int _CollectionID;
        public int CollectionID
        {
            get { return _CollectionID; }
            set
            {
                if (_CollectionID != value)
                    _CollectionID = value;
            }
        }

        private int _UserID;
        [Key]
        public int UserID
        {
            get { return _UserID; }
            set
            {
                if (_UserID != value)
                    _UserID = value;
            }
        }
        public virtual User user { get; set; }

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


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
            .HasRequired<User>(a => a.user)
            .WithMany(b => b.Accounts)
            .HasForeignKey(c => c.UserID);
        }
    }
}
