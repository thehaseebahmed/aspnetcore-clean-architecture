using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Starter.Application.Todo.Commands.CreateTodo;
using Starter.Application.Todo.Commands.DeleteAllTodos;
using Starter.Application.Todo.Commands.DeleteTodos;
using Starter.Application.Todo.Commands.UpdateTodo;
using Starter.Application.Todo.Queries.GetTodo;
using Starter.Application.Todo.Queries.GetTodoById;
using Starter.Application.ViewModels;

namespace Starter.WebAPI.Controllers;

[ApiController]
[Route("/api/v1/todo")]
[ProducesResponseType(typeof(ValidationErrorsViewModel), StatusCodes.Status422UnprocessableEntity)]
[ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status500InternalServerError)]
public class TodosController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ILogger<TodosController> _logger;

    // TODO: Fetch this dynamically from HttpContext
    private string BaseUrl => "https://localhost:5001";

    public TodosController(
        ISender sender,
        ILogger<TodosController> logger
    )
    {
        _sender = sender;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateTodoViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateNewTodo(
        [FromBody] CreateTodo command
    )
    {
        command.Completed = false;
        var response = await _sender.Send(command);
        response.Url = ReplaceBaseUrlInUrl(response.Url, BaseUrl);

        return Ok(response);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(CommandResultViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAllTodos()
    {
        await _sender.Send(new DeleteAllTodos());
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(CommandResultViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteExistingTodo(
        [FromRoute] Guid id
    )
    {
        await _sender.Send(new DeleteTodo(id));
        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResultViewModel<GetTodoViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTodo(
        [FromQuery] GetTodo query
    )
    {
        var response = await _sender.Send(query);
        foreach (var todo in response.Data)
        {
            todo.Url = ReplaceBaseUrlInUrl(todo.Url, BaseUrl);
        }

        // NOTE: Returning response.Data to satisfy todobackend.com test specs but real-life projects
        // should return the actual response with count + data.
        return Ok(response.Data);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetTodoByIdViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTodoById(
        Guid id
    )
    {
        var response = await _sender.Send(new GetTodoById(id));
        response.Url = ReplaceBaseUrlInUrl(response.Url, BaseUrl);

        return Ok(response);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(UpdateTodoViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateExistingTodo(
        [FromRoute] Guid id,
        [FromBody] UpdateTodo command
    )
    {
        command.Id = id;
        var response = await _sender.Send(command);
        response.Url = ReplaceBaseUrlInUrl(response.Url, BaseUrl);

        return Ok(response);
    }

    private static string ReplaceBaseUrlInUrl(string url, string baseUrl)
    {
        return string.Format(url, baseUrl);
    }
}