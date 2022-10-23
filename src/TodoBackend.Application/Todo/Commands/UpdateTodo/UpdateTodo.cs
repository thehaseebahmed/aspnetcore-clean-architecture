using System.Text.Json.Serialization;
using MediatR;
using Starter.Application.ViewModels;

namespace Starter.Application.Todo.Commands.UpdateTodo;

public record UpdateTodo(
    bool? Completed,
    int? Order,
    string? Title
) : IRequest<UpdateTodoViewModel>
{
    [JsonIgnore] public Guid Id { get; set; }
}