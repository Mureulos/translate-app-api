using Microsoft.AspNetCore.Mvc;
using translate_app.Domain.Abstractions;

namespace translate_app.Api.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this ControllerBase controller, Result<T> result)
    {
        if (result is null)
            return controller.Problem(statusCode: StatusCodes.Status500InternalServerError, title: "Wrong answer from server");

        if (result.TryGetValue(out var value))
            return controller.Ok(value);

        var statusCode = MapStatusCode(result.Error);

        return controller.Problem(
            statusCode: statusCode,
            title: "Failed on requesition",
            detail: result.Error.Message,
            type: result.Error.Code
        );
    }

    private static int MapStatusCode(Error error)
    {
        if (error is null)
            return StatusCodes.Status500InternalServerError;

        return int.TryParse(error.Code, out var status) ? status : StatusCodes.Status400BadRequest;
    }
}