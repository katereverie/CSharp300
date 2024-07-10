using AirportLockerRental.UI.DTOs;
using System.Text.Json;

namespace AirportLockerRental.UI.Actions
{
    public class RepositoryIO
    {
        private const string _filePath = "lockers.json";
        private static JsonSerializerOptions _options = new JsonSerializerOptions { WriteIndented = true }; // what does this do?

        public RepositoryIO()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "{}");
            }
        }

        public Dictionary<int, LockerContents> ReadDataFromFile()
        {
            string fileContent = File.ReadAllText(_filePath);

            return JsonSerializer.Deserialize<Dictionary<int, LockerContents>>(fileContent) ?? new Dictionary<int, LockerContents>();
        }

        public void AddContentsToFile(LockerContents contents)
        {
            var repos = ReadDataFromFile();
            repos.Add(contents.LockerNumber, contents);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(repos, _options)); // what does this do? the second param specifies how the first param is supposed to be written?
        }

        public bool RemoveContentsFromFile(LockerContents ContentsToRemove)
        {
            var repos = ReadDataFromFile();
            if (repos.ContainsKey(ContentsToRemove.LockerNumber))
            {
                repos.Remove(ContentsToRemove.LockerNumber);
                File.WriteAllText(_filePath, JsonSerializer.Serialize(repos, _options));
                return true;
            }

            return false;
        }
    }
}
