using MediatR;

namespace Starter.Application.Todo.Commands.DeleteTodos;

public record DeleteTodo(Guid id) : IRequest<Unit>;