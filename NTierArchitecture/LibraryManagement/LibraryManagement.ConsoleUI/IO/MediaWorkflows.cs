using LibraryManagement.Core.Entities;
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

            var getTypeResult = service.GetAllMediaTypes();
            if (!getTypeResult.Ok)
            {
                Console.WriteLine(getTypeResult.Message);
                return;
            }

            var typeList = getTypeResult.Data;

            Utilities.PrintMediaTypeList(typeList);

            Media newMedia = new Media
            {
                MediaTypeID = Utilities.GetMediaTypeID(typeList),
                Title = Utilities.GetRequiredString("Enter media title: "),
                IsArchived = false
            };

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

            var getTypeResult = service.GetAllMediaTypes();
            if (!getTypeResult.Ok)
            {
                Console.WriteLine(getTypeResult.Message);
                return;
            }
            var typeList = getTypeResult.Data;

            Utilities.PrintMediaTypeList(typeList);
            int typeID = Utilities.GetMediaTypeID(typeList);

            var getResult = service.GetUnarchivedMediaByType(typeID);
            if (!getResult.Ok)
            {
                Console.WriteLine(getResult.Message);
            }
            else
            {
                var mediaList = getResult.Data;

                Utilities.PrintMediaList(mediaList);
                int mediaID = Utilities.GetMediaID(mediaList, "Enter the ID of the media to edit: ");

                var mediaToEdit = mediaList.Find(m => m.MediaID == mediaID);

                mediaToEdit.Title = Utilities.GetRequiredString("Enter new title: ");
                mediaToEdit.MediaTypeID = Utilities.GetMediaTypeID(typeList, "Enter new type ID: ");

                var editResult = service.EditMedia(mediaToEdit);
                if (editResult.Ok)
                {
                    Console.WriteLine("Media successfully updated.");
                }
                else
                {
                    Console.WriteLine(editResult.Message);
                }    
            }

            Utilities.AnyKey();
        }

        public static void ArchiveMedia(IMediaService service)
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

            var getMediaResult = service.GetUnarchivedMediaByType(typeID);

            if (!getMediaResult.Ok)
            {
                Console.WriteLine(getMediaResult.Message);
            }
            else
            {
                var mediaList = getMediaResult.Data;
                Utilities.PrintMediaList(mediaList);
                int mediaID = Utilities.GetMediaID(mediaList, "Enter the ID of the media to be archived: ");

                var archiveResult = service.ArchiveMedia(mediaID);

                if (archiveResult.Ok)
                {
                    Console.WriteLine("Media successfully archived.");
                }
                else
                {
                    Console.WriteLine(archiveResult.Message);
                }
            }

            Utilities.AnyKey();
        }

        public static void ViewArchive(IMediaService service)
        {
            Console.Clear();

            var result = service.GetAllArchivedMedia();

            if (result.Ok)
            {
                Utilities.PrintMediaArchive(result.Data);
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
