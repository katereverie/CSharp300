namespace SystemIOExamples
{
    public class DirectoryInfoExamples
    {
        public static void GetFileInfoList()
        {
            DirectoryInfo directory = new DirectoryInfo(Directory.GetCurrentDirectory());

            FileInfo[] files = directory.GetFiles();

            foreach (var file in files)
            {
                Console.WriteLine(file.Name);
                Console.WriteLine(file.FullName);
                Console.WriteLine(file.LinkTarget);
                Console.WriteLine(file.Length + " bytes");
                Console.WriteLine(file.Attributes);
                Console.WriteLine(file.Directory);
                Console.WriteLine(file.DirectoryName);
                Console.WriteLine(file.CreationTime);
                Console.WriteLine(file.LastWriteTime);
                Console.WriteLine(file.Exists);
                Console.WriteLine(file.Extension);
                Console.WriteLine(file.IsReadOnly);
                Console.WriteLine(new string('-', 150));
            }
        }

        public static void GetFilePaths()
        {
            string[] dir = Directory.GetFiles(Directory.GetCurrentDirectory());

            foreach (var file in dir)
            {
                Console.WriteLine(file);
            }
        }
    }
}
