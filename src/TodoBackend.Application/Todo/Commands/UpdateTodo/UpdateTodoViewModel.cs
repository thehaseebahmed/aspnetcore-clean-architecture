namespace Starter.Application.Todo.Commands.UpdateTodo;

public record UpdateTodoViewModel(
    Guid Id,
    bool Completed,
    int Order,
    string Title
)
{
    public string Url { get; set; }
}