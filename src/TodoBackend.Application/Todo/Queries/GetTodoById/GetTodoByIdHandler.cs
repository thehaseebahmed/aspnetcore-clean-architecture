using AutoMapper;
using MediatR;
using Starter.Domain.Repositories;

namespace Starter.Application.Todo.Queries.GetTodoById
{
    public class GetTodoHandler : IRequestHandler<GetTodoById, GetTodoByIdViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ITodoRepository _repository;

        public GetTodoHandler(
            IMapper mapper,
            ITodoRepository repository
        )
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<GetTodoByIdViewModel> Handle(
            GetTodoById request,
            CancellationToken cancellationToken
        )
        {
            var entity = await _repository.ReadByIdAsync(request.Id);
            return _mapper.Map<GetTodoByIdViewModel>(entity);
        }
    }
}