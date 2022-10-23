using AutoMapper;
using MediatR;
using Starter.Domain.Repositories;

namespace Starter.Application.Todo.Commands.DeleteAllTodos;

public class DeleteAllTodosHandler : IRequestHandler<DeleteAllTodos, Unit>
{
    private readonly IMapper _mapper;
    private readonly ITodoRepository _repository;

    public DeleteAllTodosHandler(
        IMapper mapper,
        ITodoRepository repository
    )
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<Unit> Handle(
        DeleteAllTodos request,
        CancellationToken cancellationToken
    )
    {
        await _repository.DeleteAllAsync(cancellationToken);
        return new Unit();
    }
}