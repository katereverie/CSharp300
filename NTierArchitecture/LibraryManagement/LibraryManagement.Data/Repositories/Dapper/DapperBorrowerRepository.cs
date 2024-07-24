using Dapper;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using Microsoft.Data.SqlClient;

namespace LibraryManagement.Data.Repositories.Dapper
{
    public class DapperBorrowerRepository : IBorrowerRepository
    {
        private string _connectionString;

        public DapperBorrowerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Borrower b)
        {
            throw new NotImplementedException();
        }

        public List<Borrower> GetAll()
        {
            throw new NotImplementedException();
        }

        public Borrower? GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Borrower? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Borrower b)
        {
            throw new NotImplementedException();
        }
    }
}
