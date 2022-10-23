using AutoMapper;
using MediatR;
using Starter.Domain.Repositories;

namespace Starter.Application.Todo.Commands.DeleteTodo;

public class DeleteTodoHandler : IRequestHandler<DeleteTodos.DeleteTodo, Unit>
{
    private readonly ITodoRepository _repository;

    public DeleteTodoHandler(
        ITodoRepository repository
    )
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(
        DeleteTodos.DeleteTodo request,
        CancellationToken cancellationToken
    )
    {
        await _repository.DeleteAsync(request.id, cancellationToken);
        return new Unit();
    }
}