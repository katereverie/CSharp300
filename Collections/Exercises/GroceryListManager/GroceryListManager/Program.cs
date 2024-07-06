using GroceryListManager;

ListManager listMgr = new ListManager();

do
{
    ConsoleIO.DisplayMenu();
    Console.Write("\nEnter your choice: ");
    if (int.TryParse(Console.ReadLine(), out int choice))
    {
        switch (choice) {
            case 1:
                listMgr.AddItem(ConsoleIO.GetGroceryItem());
                break;
            case 2:
                listMgr.RemoveItem(ConsoleIO.GetRemovalIndex(listMgr.GetCount()) - 1);
                break;
            case 3:
                listMgr.DisplayItems();
                break;
            case 4:
                return;
            default:
                Console.WriteLine("invalid choice");
                continue;
        }
    }
} while (true);