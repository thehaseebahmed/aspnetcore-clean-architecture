using MediatR;
using Starter.Application.Constants;
using Starter.Application.ViewModels;

namespace Starter.Application.Todo.Queries.GetTodoById;

public record GetTodoById(
    Guid Id
) : IRequest<GetTodoByIdViewModel>;