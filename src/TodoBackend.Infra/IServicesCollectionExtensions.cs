using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Starter.Domain.Repositories;
using Starter.Infra.Data;
using Starter.Infra.Repositories;

namespace Starter.Infra;

public static class IServicesCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<ApiDbContext>((options) =>
        {
            options.UseInMemoryDatabase("InMemory Database");
        });

        services.AddScoped<ITodoRepository, TodoRepository>();

        return services;
    }
}