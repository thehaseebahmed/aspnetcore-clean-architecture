namespace Starter.Application.Todo.Commands.CreateTodo;

public record CreateTodoViewModel(
    Guid Id,
    bool Completed,
    string Title
)
{
    public string Url { get; set; }
}