using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Application;
using Microsoft.Extensions.Configuration;

namespace LibraryManagement.ConsoleUI
{
    public class AppConfiguration : IAppConfiguration
    {
        private IConfiguration _configuration;

        public AppConfiguration()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>()
                .Build();
        }

        public string GetConnectionString()
        {
            return _configuration["LibraryDb"] ?? "";
        }

        public DatabaseMode GetDatabaseMode()
        {
            switch(_configuration["DatabaseMode"])
            {
                case "ORM":
                    return DatabaseMode.ORM;
                case "SQL":
                    return DatabaseMode.DirectSQL;
                default:
                    throw new Exception("DatabaseMode configuration key not found!");
            }
        }
    }
}
