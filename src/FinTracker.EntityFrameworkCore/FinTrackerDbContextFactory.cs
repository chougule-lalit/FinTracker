using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace FinTracker.EntityFrameworkCore
{
    // FinTracker.EntityFrameworkCore/EntityFrameworkCore/FinTrackerDbContextFactory.cs
    public class FinTrackerDbContextFactory : IDesignTimeDbContextFactory<FinTrackerDbContext>
    {
        public FinTrackerDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();
            var connectionString = configuration.GetConnectionString("Default");

            var builder = new DbContextOptionsBuilder<FinTrackerDbContext>()
                .UseNpgsql(connectionString);

            return new FinTrackerDbContext(builder.Options, null!);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../FinTracker.HttpApi.Host"))
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true);

            return builder.Build();
        }
    }
}
