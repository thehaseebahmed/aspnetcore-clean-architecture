using System.Text.Json.Serialization;
using MediatR;
using Starter.Application.ViewModels;

namespace Starter.Application.Todo.Commands.CreateTodo;

public record CreateTodo : IRequest<CreateTodoViewModel>
{
    public int Order { get; set; }
    public string Title { get; set; }
    [JsonIgnore] public bool Completed { get; set; }
}