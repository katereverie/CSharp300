namespace GroceryListManager
{
    public class ListManager
    {
        private List<GroceryItem> _items = new List<GroceryItem>();

        public void AddItem(GroceryItem item)
        {
            _items.Add(item);
            Console.WriteLine("Item added successfully.\n");
        }

        public GroceryItem RemoveItem(int itemIndex)
        {
            var itemRemoved = _items[itemIndex];
            _items.RemoveAt(itemIndex);
            Console.WriteLine("Item removed successfully.\n");
            return itemRemoved;
        }

        public void DisplayItems()
        {
            ConsoleIO.DisplayItems(_items);
            Console.WriteLine();
        }

        public int GetCount()
        {
            return _items.Count;
        }
    }
}
