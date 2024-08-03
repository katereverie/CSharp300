﻿using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Services;

namespace LibraryManagement.ConsoleUI.IO
{
    public static class MediaWorkflows
    {
        public static void ListMedia(IMediaService service)
        {
            Console.Clear();

            var getTypeResult = service.GetAllMediaTypes();
            if (!getTypeResult.Ok)
            {
                Console.WriteLine(getTypeResult.Message);
                return;
            }

            var typeList = getTypeResult.Data;

            Utilities.PrintMediaTypeList(typeList);

            int typeID = Utilities.GetMediaTypeID(typeList);

            var result = service.GetMediaByType(typeID);

            if (!result.Ok)
            {
                Console.WriteLine(result.Message);
            }
            else
            {
                Utilities.PrintMediaList(result.Data);
            }

            Utilities.AnyKey();
        }

        public static void AddMedia(IMediaService service)
        {
            Console.Clear();

            Media newMedia = new Media();
            int type = 0;

            do
            {
                Menus.DisplayMediaType();

                int userInput = Utilities.GetPositiveInteger("Enter media type ID (1-3) or return (4): ");
                if (userInput == 4)
                {
                    return;
                }
                else if (userInput >= 1 && userInput <= 3)
                {
                    type = userInput;
                    break;
                }

                Console.WriteLine("Invalid media type ID.");
            } while (true);

            newMedia.MediaTypeID = type;
            newMedia.Title = Utilities.GetRequiredString("Enter media title: ");
            newMedia.IsArchived = false;

            var result = service.AddMedia(newMedia);

            if (result.Ok)
            {
                Console.WriteLine($"new Media with ID: {result.Data} added successfully.");
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Utilities.AnyKey();
        }

        public static void EditMedia(IMediaService service)
        {
            Console.Clear();

            int choice = 0;
            do
            {
                Menus.DisplayMediaType();

                int input = Utilities.GetPositiveInteger("Enter the type of media (1-3) to be edited or return (4): ");
                if (input >= 1 && input <= 4)
                {
                    choice = input;
                    break;
                }
                Console.WriteLine("Invalid input");
            } while (true);

            if (choice == 4)
            {
                return;
            }

            var getResult = service.GetUnarchivedMediaByType(choice);

            if (!getResult.Ok)
            {
                Console.WriteLine(getResult.Message);
            }
            else
            {
                Utilities.PrintMediaList(getResult.Data);

                int mediaId = Utilities.GetPositiveInteger("\nEnter the ID of the media you'd like to edit: ");

                var verificationResult = service.GetMediaByID(mediaId);

                if (!verificationResult.Ok)
                {
                    Console.WriteLine(verificationResult.Message);
                }
                else
                {
                    verificationResult.Data.Title = Utilities.GetRequiredString("Enter new Title: ");

                    int newTypeID;
                    do
                    {
                        Menus.DisplayMediaType();
                        int input = Utilities.GetPositiveInteger("Enter new Type ID (1-3): ");
                        if (input >= 1 && input <= 3)
                        {
                            newTypeID = input;
                            break;
                        }
                        Console.WriteLine($"Invalid Type ID: {input}");
                    } while (true);

                    verificationResult.Data.MediaTypeID = newTypeID;

                    var editResult = service.EditMedia(verificationResult.Data);
                    if (editResult.Ok)
                    {
                        Console.WriteLine("Media successfully updated.");
                    }
                    else
                    {
                        Console.WriteLine(editResult.Message);
                    }
                }
            }

            Utilities.AnyKey();
        }

        public static void ArchiveMedia(IMediaService service)
        {
            Console.Clear();

            Menus.DisplayMediaType();
            int type = 0;

            do
            {
                int choice = Utilities.GetPositiveInteger("Enter the type of the media (1-3) to be archived or return (4): ");
                if (choice == 4)
                {
                    return;
                }
                else if (choice >= 1 && choice <= 3)
                {
                    type = choice;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }

            } while (true);

            var getResult = service.GetUnarchivedMediaByType(type);

            if (getResult.Data != null && getResult.Ok)
            {
                Console.WriteLine($"\n{"Media ID",-10} {"Type",-15} {"Title",-35} {"Status",-60}");
                Console.WriteLine(new string('=', 100));

                foreach (var m in getResult.Data)
                {
                    Console.WriteLine($"{m.MediaID,-10} " +
                            $"{m.MediaTypeID,-15} " +
                            $"{m.Title,-35} " +
                            $"{(m.IsArchived ? "Archived" : "Available"),-60}");
                }
            }
            else
            {
                Console.WriteLine(getResult.Message);
            }

            Console.WriteLine();

            int mediaId = 0;
            do
            {
                int userInput = Utilities.GetPositiveInteger("Enter the ID of the media to be archived: ");

                var verificationResult = service.GetMediaByID(userInput);
                if (verificationResult.Ok)
                {
                    mediaId = userInput;
                    break;
                }

                Console.WriteLine(verificationResult.Message);
            } while (true);
            
            var result = service.ArchiveMedia(mediaId);

            if (result.Ok)
            {
                Console.WriteLine("Media successfully archived.");
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Utilities.AnyKey();
        }

        public static void ViewArchive(IMediaService service)
        {
            Console.Clear();

            var result = service.GetAllArchivedMedia();

            if (result.Ok)
            {
                Console.WriteLine($"\n{"Type ID",-10} {"Title",-30}");
                Console.WriteLine(new string('=', 50));

                foreach (var m in result.Data)
                {
                    Console.WriteLine($"{m.MediaTypeID,-10} " +
                                      $"{m.Title,-3} ");
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Utilities.AnyKey();
        }

        public static void GetMostPopularMediaReport(IMediaService service)
        {
            Console.Clear();

            var result = service.GetTop3MostPopularMedia();

            if (result.Ok && result.Data != null)
            {
                Console.WriteLine($"\n{"Media ID",-10} {"Type", -20} {"Title", -35} {"Checkout Count",-15}");
                Console.WriteLine(new string('=',80));

                foreach (var mcc in result.Data)
                {
                    Console.WriteLine($"{mcc.MediaID,-10} " +
                            $"{mcc.MediaTypeName, -20} " +
                            $"{mcc.Title, -35} " +
                            $"{mcc.CheckoutCount,-15}");
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            Utilities.AnyKey();
        }

    }
}
