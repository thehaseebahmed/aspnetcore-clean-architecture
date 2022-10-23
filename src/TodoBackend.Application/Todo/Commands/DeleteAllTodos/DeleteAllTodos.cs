using MediatR;

namespace Starter.Application.Todo.Commands.DeleteAllTodos;

public record DeleteAllTodos : IRequest<Unit>
{
}