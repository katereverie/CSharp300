﻿using LibraryManagement.Application;
using LibraryManagement.ConsoleUI.IO;
using LibraryManagement.Core.Interfaces.Application;

namespace LibraryManagement.ConsoleUI
{
    public class App
    {
        IAppConfiguration _config;
        ServiceFactory _serviceFactory;

        public App()
        {
            _config = new AppConfiguration();
            _serviceFactory = new ServiceFactory(_config);
        }

        public void Run()
        {
            do
            {
                Console.Clear();
                Menus.DisplayMainMenu();

                int choice = Utilities.GetPositiveInteger("Enter Menu Option (1-4): ");
                switch (choice)
                {
                    case 1:
                        ManageBorrower();
                        break;
                    case 2:
                        ManageMedia();
                        break;
                    case 3:
                        ManageCheckout();
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        continue;
                }

            } while (true);
        }

        private void ManageBorrower ()
        {
            do
            {
                Menus.DisplayBorrowerManagementMenu();

                int choice = Utilities.GetPositiveInteger("Enter Management Option (1-6): ");
                switch (choice)
                {
                    case 1:
                        BorrowerWorkflows.ListAllBorrowers(_serviceFactory.CreateBorrowerService());
                        break;
                    case 2:
                        BorrowerWorkflows.ViewBorrower(_serviceFactory.CreateBorrowerService());
                        break;
                    case 3:
                        BorrowerWorkflows.EditBorrower(_serviceFactory.CreateBorrowerService());
                        break;
                    case 4:
                        BorrowerWorkflows.AddBorrower(_serviceFactory.CreateBorrowerService());
                        break;
                    case 5:
                        BorrowerWorkflows.DeleteBorrower(_serviceFactory.CreateBorrowerService());
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        continue;
                }

            } while (true);
        }

        private void ManageMedia()
        {
            do
            {
                Menus.DisplayMediaManagementMenu();

                int choice = Utilities.GetPositiveInteger("Enter Management Option (1-7): ");
                switch (choice)
                {
                    case 1:
                        MediaWorkflows.ListMedia(_serviceFactory.CreateMediaService());
                        break;
                    case 2:
                        MediaWorkflows.AddMedia(_serviceFactory.CreateMediaService());
                        break;
                    case 3:
                        MediaWorkflows.EditMedia(_serviceFactory.CreateMediaService());
                        break;
                    case 4:
                        MediaWorkflows.ArchiveMedia(_serviceFactory.CreateMediaService());
                        break;
                    case 5:
                        MediaWorkflows.ViewArchive(_serviceFactory.CreateMediaService());
                        break;
                    case 6:
                        MediaWorkflows.GetMostPopularMediaReport(_serviceFactory.CreateMediaService());
                        break;
                    case 7:
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        continue;
                }

            } while (true);
        }

        private void ManageCheckout()
        {
            do
            {
                Menus.DisplayCheckoutManagementMenu();

                int choice = Utilities.GetPositiveInteger("Enter Management Option (1-4): ");
                switch (choice)
                {
                    case 1:
                        CheckoutWorkflows.Checkout(_serviceFactory.CreateCheckoutService());
                        break;
                    case 2:
                        CheckoutWorkflows.Return(_serviceFactory.CreateCheckoutService());
                        break;
                    case 3:
                        CheckoutWorkflows.CheckoutLog(_serviceFactory.CreateCheckoutService());
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        continue;
                }

            } while (true);
        }
    }
}
