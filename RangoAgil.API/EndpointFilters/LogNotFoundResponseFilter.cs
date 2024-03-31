
using System.Net;

namespace RangoAgil.API.EndpointFilters;

public class LogNotFoundResponseFilter(ILogger<LogNotFoundResponseFilter> logger) : IEndpointFilter
{
    public readonly ILogger<LogNotFoundResponseFilter> _logger = logger;

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var result = await next(context);
        var actualResults = (result is INestedHttpResult result1) ? result1.Result : (IResult)result;

        if (actualResults is IStatusCodeHttpResult { StatusCode: (int)HttpStatusCode.NotFound })
        {
            _logger.LogInformation($"Resource {context.HttpContext.Request.Path} was not found.");
        }

        return result;
    }
}
