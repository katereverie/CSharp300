using LibraryManagement.Application.Services;
using LibraryManagement.Core.Interfaces.Application;
using LibraryManagement.Core.Interfaces.Services;
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
            return new BorrowerService(new EFBorrowerRepository(_config.GetConnectionString()));
        }

        public IMediaService CreateMediaService()
        {
            return new MediaService(new EFMediaRepository(_config.GetConnectionString()));
        }

        public ICheckoutService CreateCheckoutService()
        {
            return new CheckoutService(new EFCheckoutRepository(_config.GetConnectionString()), new EFMediaRepository(_config.GetConnectionString()));
        }
    }
}
