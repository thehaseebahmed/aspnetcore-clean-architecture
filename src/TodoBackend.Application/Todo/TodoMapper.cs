using AutoMapper;
using Starter.Application.Todo.Commands.CreateTodo;
using Starter.Application.Todo.Queries.GetTodo;
using Starter.Application.Todo.Queries.GetTodoById;

namespace Starter.Application.Todo;

public class TodoMapper : Profile
{
    public TodoMapper()
    {
        const string baseUrlPattern = "{0}/api/v1/todo/";

        // CreateTodo
        CreateMap<CreateTodo, Domain.Entities.Todo>();
        CreateMap<Domain.Entities.Todo, CreateTodoViewModel>()
            .ForMember(
                dst => dst.Url,
                src => src.MapFrom(opt => baseUrlPattern + opt.Id)
            );

        // GetTodo
        CreateMap<Domain.Entities.Todo, GetTodoViewModel>()
            .ForMember(
                dst => dst.Url,
                src => src.MapFrom(opt => baseUrlPattern + opt.Id)
            );

        // GetTodoById
        CreateMap<Domain.Entities.Todo, GetTodoByIdViewModel>()
            .ForMember(
                dst => dst.Url,
                src => src.MapFrom(opt => baseUrlPattern + opt.Id)
            );
    }
}