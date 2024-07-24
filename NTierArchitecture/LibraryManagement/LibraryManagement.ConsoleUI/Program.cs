using LibraryManagement.ConsoleUI;

var app = new App();
app.Run();

//Console.WriteLine(config.GetConnectionString());

//ServiceFactory factory = new ServiceFactory(config);

//var borrowerService = factory.CreateBorrowerService();

//var result = borrowerService.GetAllBorrowers();

//if (result.Ok)
//{
//    foreach (var b in result.Data)
//    {
//        Console.WriteLine(b.Email);
//    }
//}
//else
//{
//    Console.WriteLine(result.Message);
//}

//var lookup1 = borrowerService.GetBorrower(1);
//if (lookup1.Ok)
//{
//    Console.WriteLine($"Borrower {lookup1.Data.FirstName} found!");
//}
//else
//{
//    Console.WriteLine(lookup1.Message);
//}

//var lookup2 = borrowerService.GetBorrower(1000);
//if (lookup2.Ok)
//{
//    Console.WriteLine($"Borrower {lookup2.Data.LastName} found!");
//}
//else
//{
//    Console.WriteLine(lookup2.Message);
//}