
namespace RangoAgil.API.EndpointFilters;

public class RangoIsLockedFilter : IEndpointFilter
{
    public readonly int _lockedRangoId;

    public RangoIsLockedFilter(int lockedRangoId)
    {
        _lockedRangoId = lockedRangoId;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        int rangoId;

        if (context.HttpContext.Request.Method == "PUT")
        {
            rangoId = context.GetArgument<int>(2);
        } 
        else if (context.HttpContext.Request.Method == "DELETE")
        {
            rangoId = context.GetArgument<int>(1);
        }
        else 
        {
            throw new NotSupportedException("This filter is not supported for this scenario.");
        }

        if (rangoId == _lockedRangoId)
        {
            return TypedResults.Problem(new()
            {
                Status = 400,
                Title = "Rango já é Perfeito, você não precisa modificar ou deletar esta receita",
                Detail = "Você não pode modificar ou deletar esta Receita"
            });
        }

        var result = await next.Invoke(context);
        return result;
    }
}
