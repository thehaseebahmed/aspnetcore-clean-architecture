using FluentValidation;

namespace Starter.Application.Todo.Commands.UpdateTodo;

public class UpdateTodoValidator : AbstractValidator<UpdateTodo>
{
    public UpdateTodoValidator()
    {
    }
}