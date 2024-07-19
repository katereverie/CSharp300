using Microsoft.Extensions.Configuration;

// creating an instance of the config builder and loading the appsettings.json file
var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

// now we can get a configuration section by name
var configSection = configBuilder.GetSection("AppSettings");

// get the config values in the section
var apiKey = configSection["API_Key"] ?? null;
var logDirectory = configSection["LogDir"] ?? null;
var db = configSection["ConnectionStrings:4WS"] ?? null;

Console.WriteLine($"key:{apiKey}\nlogdir:{logDirectory}\ndb:{db}\n");
