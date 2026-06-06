using Microsoft.AspNetCore.Mvc;
using translate_app.Domain.Abstractions;

namespace translate_app.Api.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this ControllerBase controller, Result<T> result)
    {
        if (result is null)
            return controller.Problem(statusCode: StatusCodes.Status500InternalServerError, title: "Server Error");

        if (result.IsSuccess)
            return controller.Ok(result.Value);

        var statusCode = MapStatusCode(result.Error.Type);

        return controller.Problem(
            statusCode: statusCode,
            title: "Request Failed",
            detail: result.Error.Message,
            type: result.Error.Code
        );
    }

    private static int MapStatusCode(ErrorType errorType) => errorType switch
    {
        ErrorType.Validation => StatusCodes.Status400BadRequest,
        ErrorType.NotFound => StatusCodes.Status404NotFound,
        ErrorType.Conflict => StatusCodes.Status409Conflict,
        ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
        _ => StatusCodes.Status500InternalServerError
    };
}