namespace Starter.Application.Todo.Queries.GetTodoById
{
    public record GetTodoByIdViewModel(
        Guid Id,
        bool Completed,
        int Order,
        string Title
    )
    {
        public string Url { get; set; }
    };
}