using AutoMapper;
using MediatR;
using Starter.Application.ViewModels;
using Starter.Domain.Repositories;

namespace Starter.Application.Todo.Queries.GetTodo
{
    public class GetTodoHandler : IRequestHandler<GetTodo, PagedResultViewModel<GetTodoViewModel>>
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

        public async Task<PagedResultViewModel<GetTodoViewModel>> Handle(
            GetTodo request,
            CancellationToken cancellationToken
        )
        {
            var result = _repository.Read(
                request.Filter,
                request.Count,
                request.Limit,
                request.Skip,
                request.Order
            );

            return new PagedResultViewModel<GetTodoViewModel>
            {
                Count = result.count,
                Data = _mapper.Map<IEnumerable<GetTodoViewModel>>(result.data)
            };
        }
    }
}