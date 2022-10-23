using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Starter.Infra.Data;
using System.IO;

namespace Starter.Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApiDbContext>
{
    // This class' single purpose is to keep migrations out of the startup project
    // This is made to avoid coupling between API and specific ORM (EF Core)
    public ApiDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<ApiDbContext>();
        var connectionString = configuration.GetConnectionString("Main");
        builder.UseSqlServer(connectionString);

        return new ApiDbContext(builder.Options);
    }
}