using SystemIOExamples;

//string path1 = @"D:\GitHub\CloneRepository\C#300\text1.txt";
//PathExamples.PrintFileName(path1);

//if (File.Exists(path1))
//{
//    Console.WriteLine("This path exists");
//    string[] lines = File.ReadAllLines(path1);
//    Console.WriteLine(new string('-', 5) + " File Content " + new string('-', 5));
//    foreach (string line in lines)
//    {
//        Console.WriteLine(line);
//    }

//    Console.WriteLine(new string('-', 5) + " End of File " + new string('-', 5));
//}

// Create new folders
Directory.CreateDirectory(@"D:\testFolder1");
Directory.CreateDirectory(@"D:\testFolder1\nestedFolder1");

// Use File.WriteAllLines to create a file in a folder 
string[] lines = { "This is a test file.", "It has several lines of texts.", "This is another line.", "This is the end of the text." };
File.WriteAllLines(@"D:\testFolder1\nestedFolder1\testFile.txt", lines);

//DirectoryExamples.PrintCurrentDirectory();
//DirectoryExamples.PrintLogicalDrives();
//DirectoryExamples.PrintCurrentDirectoryFiles();
//DirectoryExamples.PrintParentDirectory();
DirectoryExamples.PrintDirectories(@"D:\GitHub\CloneRepository");

//DirectoryInfoExamples.GetFileInfoList();
//DirectoryInfoExamples.GetFilePaths();

//FileExamples.IceCreamReadAllLines();
//FileExamples.IceCreamReadAllText();
//FileExamples.IceCreamStreamWithoutUsing();
//FileExamples.IceCreamStreamReader();
