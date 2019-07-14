using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtFreeMe.Model
{
    public class Context : DbContext
    {
        public Context() : base("name = connstrng"){}

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
        .HasRequired<User>(a => a.user)
        .WithMany(b => b.Accounts)
        .HasForeignKey(c => c.UserID);
    }
}
