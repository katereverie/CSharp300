namespace LibraryManagement.Core.Entities
{
    public class Media
    {
        public int MediaID { get; }
        public int MediaTypeID { get; }
        public string Title { get; }
        public bool IsArchived { get; }
    }
}
