using MediatR;
using Starter.Application.Constants;
using Starter.Application.ViewModels;

namespace Starter.Application.Todo.Queries.GetTodo;

public record GetTodo : IRequest<PagedResultViewModel<GetTodoViewModel>>
{
    public bool Count { get; set; } = AppConstants.DEFAULT_QUERY_COUNT;
    public string? Filter { get; set; }
    public int Limit { get; set; } = AppConstants.DEFAULT_QUERY_LIMIT;
    public string? Order { get; set; }
    public int Skip { get; set; }
}