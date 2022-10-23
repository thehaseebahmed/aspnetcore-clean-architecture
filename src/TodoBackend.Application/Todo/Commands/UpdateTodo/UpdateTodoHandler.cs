using AutoMapper;
using MediatR;
using Starter.Application.ViewModels;
using Starter.Domain.Exceptions;
using Starter.Domain.Repositories;

namespace Starter.Application.Todo.Commands.UpdateTodo;

public class UpdateTodoHandler : IRequestHandler<UpdateTodo, UpdateTodoViewModel>
{
    private readonly IMapper _mapper;
    private readonly ITodoRepository _repository;

    public UpdateTodoHandler(
        IMapper mapper,
        ITodoRepository repository
    )
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<UpdateTodoViewModel> Handle(
        UpdateTodo request,
        CancellationToken cancellationToken
    )
    {
        var entity = await _repository.ReadByIdAsync(request.Id);
        if (entity == null)
        {
            throw new NotFoundException();
        }

        _mapper.Map(request, entity);
        await _repository.UpdateAsync(entity, cancellationToken);

        return _mapper.Map<UpdateTodoViewModel>(entity);
    }
}