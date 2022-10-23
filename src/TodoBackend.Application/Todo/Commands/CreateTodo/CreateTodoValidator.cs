using FluentValidation;

namespace Starter.Application.Todo.Commands.CreateTodo;

public class CreateTodoValidator : AbstractValidator<CreateTodo>
{
    public CreateTodoValidator()
    {
    }
}