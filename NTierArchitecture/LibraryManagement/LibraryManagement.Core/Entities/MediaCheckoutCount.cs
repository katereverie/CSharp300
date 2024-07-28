namespace LibraryManagement.Core.Entities
{
    public class MediaCheckoutCount
    {
        public int MediaID { get; set; }
        public string MediaType { get; set; }
        public string MediaTitle { get; set; }
        public int CheckoutCount { get; set; }
    }
}
