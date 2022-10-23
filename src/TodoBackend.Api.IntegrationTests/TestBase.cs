using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Starter.Infra.Data;
using Starter.WebAPI;

namespace Starter.Api.IntegrationTests;

public class TestBase : IClassFixture<TestBase>, IDisposable
{
    private readonly TestServer _server;

    protected HttpClient Client { get; }
    protected IServiceScope TestScope { get; }

    public TestBase()
    {
        var configuration = GetIConfigurationRoot();
        _server = RunServer(configuration);

        Client = _server.CreateClient();
        TestScope = _server.Services.CreateScope();
    }

    private void AddTestDependencies(IServiceCollection services)
    {
        services.AddDbContext<ApiDbContext>(b =>
        {
            b.UseInMemoryDatabase("TestDatabase");
            b.UseLoggerFactory(LoggerFactory.Create(c => c.AddConsole().AddDebug()));
            b.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        });
    }

    private IConfigurationRoot GetIConfigurationRoot()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            .AddJsonFile("appsettings.json", optional: true)
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "RedisConnection", "" },
                { "ServiceBusConnectionString", "" },
            })
            .AddEnvironmentVariables()
            .Build();
    }

    private TestServer RunServer(IConfigurationRoot config)
    {
        var webHostBuilder = new WebHostBuilder()
            .ConfigureServices(AddTestDependencies)
            .UseConfiguration(config)
            .UseEnvironment("Test")
            .UseStartup(typeof(Startup));

        var server = new TestServer(webHostBuilder);
        return server;
    }

    public void Dispose()
    {
        _server.Dispose();
    }
}