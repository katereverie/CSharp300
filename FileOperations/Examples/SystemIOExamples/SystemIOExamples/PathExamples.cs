namespace SystemIOExamples
{
    public static class PathExamples
    {
        public static void PrintFileName(string path)
        {
            string fileName = Path.GetFileName(path);
            Console.WriteLine("This is the file name: " + fileName);
        }

        public static void PrintFileNameWithoutExtension(string path)
        {
            string fileNameWOExtension = Path.GetFileNameWithoutExtension(path);
            Console.WriteLine("This is the file name w/o extension: " + fileNameWOExtension);
        }

        public static void PrintExtension(string path)
        {
            string extension = Path.GetExtension(path);
            Console.WriteLine("This is the file extension: " + extension);
        }

        public static void PrintPathRoot(string path)
        {
            string? root = Path.GetPathRoot(path);
            Console.WriteLine("This is the root of the path: " + root);
        }
        
        public static void PrintDirectoryName(string path)
        {
            string? dirName = Path.GetDirectoryName(path);
            Console.WriteLine("This is the directory name: " + dirName);
        }

        public static void PrintCombinedPath(string str1, string str2)
        {
            string path = Path.Combine(str1, str2);
            Console.WriteLine("This is the combined path: " + path);
        } 
    }
}
