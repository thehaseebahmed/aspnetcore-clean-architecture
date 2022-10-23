namespace Starter.Application.Todo.Queries.GetTodoById
{
    public record GetTodoByIdViewModel(
        Guid Id,
        bool Completed,
        string Title
    )
    {
        public string Url { get; set; }
    };
}