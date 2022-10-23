namespace Starter.Application.Todo.Queries.GetTodo
{
    public record GetTodoViewModel(
        Guid Id,
        bool Completed,
        int Order,
        string Title
    )
    {
        public string Url { get; set; }
    }
}