namespace LibraryManagement.Core.Entities
{
    public class CheckoutLog
    {
        public int CheckoutLogID { get; }
        public int BorrowerID { get; }
        public int MediaID { get; }
        public DateTime? CheckoutDate { get; }
        public DateTime? DueDate { get; }
        public DateTime? ReturnDate { get; }
    }
}
