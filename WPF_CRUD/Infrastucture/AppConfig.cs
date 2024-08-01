using System.IO;
using Microsoft.Extensions.Configuration;

namespace WPF_CRUD.Infrastucture;

internal static class AppConfig
{
    public static string GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();

        string connectionString = configuration.GetConnectionString("DefaultConnection");

        return connectionString;
    }
}
