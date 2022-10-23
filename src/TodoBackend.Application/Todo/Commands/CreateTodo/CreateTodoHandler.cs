using AutoMapper;
using MediatR;
using Starter.Application.ViewModels;
using Starter.Domain.Repositories;

namespace Starter.Application.Todo.Commands.CreateTodo;

public class CreateTodoHandler : IRequestHandler<CreateTodo, CreateTodoViewModel>
{
    private readonly IMapper _mapper;
    private readonly ITodoRepository _repository;

    public CreateTodoHandler(
        IMapper mapper,
        ITodoRepository repository
    )
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<CreateTodoViewModel> Handle(
        CreateTodo request,
        CancellationToken cancellationToken
    )
    {
        var model = _mapper.Map<Domain.Entities.Todo>(request);

        await _repository.CreateAsync(model, cancellationToken);

        return _mapper.Map<CreateTodoViewModel>(model);
    }
}