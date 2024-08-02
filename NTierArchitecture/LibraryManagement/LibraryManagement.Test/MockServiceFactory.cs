using LibraryManagement.Application.Services;
using LibraryManagement.Core.Interfaces.Services;
using LibraryManagement.Test.MockRepos;

namespace LibraryManagement.Test
{
    public static class MockServiceFactory
    {
        public static IBorrowerService CreateBorrowerService()
        {
            return new BorrowerService(new MockBorrowerRepo());
        }

        public static IMediaService CreateMediaService()
        {
            return new MediaService(new MockMediaRepo());
        }

        public static ICheckoutService CreateCheckoutService()
        {
            return new CheckoutService(new MockCheckoutRepo(), new MockMediaRepo());
        }
    }
}
