using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Starter.WebAPI.Filters;

public class TransformAttribute : ResultFilterAttribute
{
    public TransformAttribute()
    {
    }

    public override void OnResultExecuting(ResultExecutingContext context) {
        var responseType = context.Filters.OfType<ProducesResponseTypeAttribute>()
            .Where(f => f.StatusCode == StatusCodes.Status200OK)
            .FirstOrDefault()?.Type;

        if (responseType == null) { return; }
        if (!context.ModelState.IsValid) { return; }
        if (!(context.Result is ObjectResult result)) { return; }

        var sourceType = result.Value.GetType();
        var mapper = context.HttpContext.RequestServices.GetService<IMapper>();

        result.Value = mapper.Map(result.Value, sourceType, responseType);
    }

    public override void OnResultExecuted(ResultExecutedContext context)
    {
            
    }
}