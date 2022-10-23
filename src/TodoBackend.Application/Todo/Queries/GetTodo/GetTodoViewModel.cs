namespace Starter.Application.Todo.Queries.GetTodo
{
    public record GetTodoViewModel(
        Guid Id,
        bool Completed,
        string Title
    )
    {
        public string Url { get; set; }
    }
}