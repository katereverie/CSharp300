using LibraryManagement.Core.Entities;

namespace LibraryManagement.ConsoleUI.IO
{
    public class Utilities
    {
        public static void AnyKey()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static int GetPositiveInteger(string prompt)
        {
            int result;

            do
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out result))
                {
                    if (result > 0)
                    {
                        return result;
                    }
                }

                Console.WriteLine("Invalid input, must be a positive integer!");
                AnyKey();
            } while (true);
        }

        public static string GetRequiredString(string prompt)
        {
            string? input;

            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();

                if(!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }

                Console.WriteLine("Input is required.");
                AnyKey();
            } while (true);
        }

        public static int GetMediaTypeID(List<MediaType> typeList)
        {
            do
            {
                int input = GetPositiveInteger("Enter media type ID: ");
                if (typeList.Any(mt => mt.MediaTypeID == input))
                {
                    return input;
                }
                Console.WriteLine("Invalid media type ID.");
            } while (true);
        }

        public static void PrintBorrowerList(List<Borrower> list)
        {
            PrintHeader(" Borrower List ");
            Console.WriteLine($"{"ID",-5} {"Name",-32} Email");
            Console.WriteLine(new string('=', 100));
            foreach (var b in list)
            {
                Console.WriteLine($"{b.BorrowerID,-5} {b.LastName + ", " + b.FirstName,-32} {b.Email}");
            }
            Console.WriteLine();
        }

        public static void PrintBorrowerInformation(Borrower borrower)
        {
            PrintHeader(" Borrower Information ");
            Console.WriteLine($"Id: {borrower.BorrowerID}");
            Console.WriteLine($"Name: {borrower.LastName}, {borrower.FirstName}");
            Console.WriteLine($"Email: {borrower.Email}");
            Console.WriteLine();
        }

        public static void PrintBorrowerCheckoutLog(List<CheckoutLog> logs)
        {
            PrintHeader(" Checkout Record ");
            Console.WriteLine($"{"Media ID",-10} {"Title",-40} {"Checkout Date",-20} {"Return Date",-20}");
            Console.WriteLine(new string('=', 100));
            foreach (var cl in logs)
            {
                Console.WriteLine($"{cl.MediaID,-10} " +
                    $"{cl.Media.Title,-40} " +
                    $"{cl.CheckoutDate,-20:MM/dd/yyyy} " +
                    $"{(cl.ReturnDate == null ? "Unreturned" : cl.ReturnDate),-20:MM/dd/yyyy}");
            }
            Console.WriteLine();
        }

        public static void PrintMediaList(List<Media> list)
        {
            PrintHeader($" {list[0].MediaType.MediaTypeName} List ");
            Console.WriteLine($"{"Media ID", -10} {"Type ID", -10} {"Title", -35} {"Status", -15}");
            Console.WriteLine(new string('=', 100));
            foreach (var m in list)
            {
                Console.WriteLine($"{m.MediaID,-10} " +
                                  $"{m.MediaTypeID, -10} " +
                                  $"{m.Title,-35} " +
                                  $"{(m.IsArchived ? "Archived" : "Available"),-15}");
            }
            Console.WriteLine();
        }

        public static void PrintMediaTypeList(List<MediaType> list)
        {
            PrintHeader($" Media Type List ");
            Console.WriteLine($"{"Type ID", -10} {"Type Name", -20}");
            Console.WriteLine(new string('=', 100));
            foreach (var mt in list)
            {
                Console.WriteLine($"{mt.MediaTypeID, -10} " +
                                  $"{mt.MediaTypeName, -20} ");
            }
            Console.WriteLine();
        }

        public static void PrintHeader(string header)
        {
            string headerSpace = new string('-', (100 - header.Length) / 2);
            Console.WriteLine("\n" + headerSpace + header + headerSpace + "\n");
        }
    }
}
