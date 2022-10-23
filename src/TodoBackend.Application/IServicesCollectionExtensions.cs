using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Starter.Application.Mediatr;
using Starter.Domain.Entities;
using System.Reflection;
using Starter.Application.Todo.Commands.CreateTodo;
using Starter.Application.ViewModels;

namespace Starter.Application;

public static class IServicesCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DummyClass).Assembly);
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssemblyContaining<DummyClass>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        return services;
    }
}