using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data
{
    public class LibraryContext : DbContext
    {
        private string _connectionString;

        public DbSet<Borrower> Borrower { get; set; }

        public LibraryContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder); // apply default settings
            optionsBuilder.UseSqlServer(_connectionString); // specify SQL provider
        }
    }
}
