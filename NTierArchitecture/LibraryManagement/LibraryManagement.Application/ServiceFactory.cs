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
            switch (_config.GetDatabaseMode())
            {
                case DatabaseMode.ORM:
                    return new BorrowerService(new EFBorrowerRepository(_config.GetConnectionString()));
                case DatabaseMode.DirectSQL:
                    return new BorrowerService(new DBorrowerRepository(_config.GetConnectionString()));
                default:
                    throw new Exception("DatabaseMode configuration is off.");
            }
        }

        public IMediaService CreateMediaService()
        {
            switch (_config.GetDatabaseMode())
            {
                case DatabaseMode.ORM:
                    return new MediaService(new EFMediaRepository(_config.GetConnectionString()));
                case DatabaseMode.DirectSQL:
                    return new MediaService(new DMediaRepository(_config.GetConnectionString()));
                default:
                    throw new Exception("DatabaseMode configuration is off.");
            }  
        }

        public ICheckoutService CreateCheckoutService()
        {
            switch (_config.GetDatabaseMode())
            {
                case DatabaseMode.ORM:
                    return new CheckoutService(new EFCheckoutRepository(_config.GetConnectionString()), new EFMediaRepository(_config.GetConnectionString()));
                case DatabaseMode.DirectSQL:
                    return new CheckoutService(new DCheckoutRepository(_config.GetConnectionString()), new DMediaRepository(_config.GetConnectionString()));
                default:
                    throw new Exception("DatabaseMode configuration is off.");
            }
        }
    }
}
