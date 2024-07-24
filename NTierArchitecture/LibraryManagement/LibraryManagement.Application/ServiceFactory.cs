using LibraryManagement.Application.Services;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Application;
using LibraryManagement.Core.Interfaces.Services;
using LibraryManagement.Data.Repositories.Dapper;
using LibraryManagement.Data.Repositories.EF;

namespace LibraryManagement.Application
{
    public class ServiceFactory
    {
        private IAppConfiguration _config;

        public ServiceFactory(IAppConfiguration config)
        {
            _config = config;
        }

        public IBorrowerService CreateBorrowerService()
        {
            // GetDatabaseMode method already handles exceptions to ensure the returned value is either ORM or SQL
            // So, there's no need to handle exceptions in CreateBorrowerService

            if (_config.GetDatabaseMode() == DatabaseMode.ORM)
            {
                return new BorrowerService(new EFBorrowerRepository(_config.GetConnectionString()));
            }
            else
            {
                return new BorrowerService(new DapperBorrowerRepository(_config.GetConnectionString()));
            }
        }
    }
}
