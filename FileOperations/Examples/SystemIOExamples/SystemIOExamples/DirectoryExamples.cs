namespace SystemIOExamples
{
    public class DirectoryExamples
    {
        public static void PrintCurrentDirectory()
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            Console.WriteLine();
        }

        public static void PrintLogicalDrives()
        {
            string[] drives = Directory.GetLogicalDrives();
            
            foreach (string drive in drives)
            {
                Console.WriteLine(drive);
                Console.WriteLine();
            }
        }

        public static void PrintCurrentDirectoryFiles()
        {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
            foreach (string file in files)
            {
                Console.WriteLine(file);
                Console.WriteLine();
            }
        }

        public static void PrintParentDirectory()
        {
            string currentDir = Directory.GetCurrentDirectory();
            DirectoryInfo? parentDir = Directory.GetParent(currentDir);

            Console.WriteLine(parentDir);
        }

        public static void PrintDirectories(string path)
        {
            string[] foldersInPath = Directory.GetDirectories(path);

            foreach (string folder in foldersInPath)
            {
                Console.WriteLine(folder);
 
            }

            Console.WriteLine();
        }

    }
}
