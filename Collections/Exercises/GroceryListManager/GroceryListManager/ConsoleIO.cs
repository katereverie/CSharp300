namespace GroceryListManager
{
    public static class ConsoleIO
    {
        public static void DisplayMenu()
        {
            Console.WriteLine("---- Main Menu ----");
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. Remove Item");
            Console.WriteLine("3. Display Items");
            Console.WriteLine("4. Exit\n");
        }

        public static GroceryItem GetGroceryItem()
        {
            GroceryItem item = new GroceryItem();

            Console.WriteLine("\n---- Add Item ----");

            while (true)
            {
                Console.Write("Enter the name of the grocery item: ");
                string itemName = Console.ReadLine().Trim();
                if (itemName == "")
                {
                    Console.WriteLine("item name cannot be empty.");
                    continue;
                }

                Console.Write("Enter the quantity: ");
                if (int.TryParse(Console.ReadLine(), out int quantity))
                {
                    item.Quantity = quantity;
                    break;
                }
                Console.WriteLine("invalid quantity.\n");
            }

            return item;
        }

        public static void DisplayItems(List<GroceryItem> items)
        {

            if (items.Count == 0)
            {
                Console.WriteLine("You haven't added any item to the list.");
                return;
            }

            Console.WriteLine("\n---- Display Items ----");

            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i].ItemName} - Quantity: {items[i].Quantity}");
            }
        }

        public static int GetRemovalIndex(int count)
        {
            while (true)
            {
                Console.WriteLine("\n---- Remove Item ----");
                Console.Write("Enter the index of the item you want to remove: ");
                if (int.TryParse(Console.ReadLine(), out int index))
                {
                    if (index > count + 1)
                    {
                        Console.WriteLine("Invalid index.");
                        continue;
                    }
                    return index;
                }
            }
        }
    }
}
