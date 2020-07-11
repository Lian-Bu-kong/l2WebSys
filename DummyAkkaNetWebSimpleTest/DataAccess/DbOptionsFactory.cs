using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DataAccess
{
    public class DbOptionsFactory
    {
        public static DbContextOptions<ApplicationDbContext> DbContextOptions { get; }
        public static string ConnectionString { get; }
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        static DbOptionsFactory()
        {
            var configuration = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .Build();
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("DataAccess", LogLevel.Debug)
                    .AddConsole()
                    ;
            });
            DbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                              .UseSqlServer(ConnectionString)
                              .UseLoggerFactory(loggerFactory)
                              .Options;
        }
    }
}
