namespace LibraryManagement.Core.Entities
{
    public class MediaCheckoutCount
    {
        public int MediaID { get; set; }
        public string MediaTypeName { get; set; }
        public string Title { get; set; }
        public int CheckoutCount { get; set; }
    }
}
