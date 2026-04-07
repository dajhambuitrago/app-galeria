using Backend.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebAPI.Extensions;

public static class ResultExtensions
{
    /// <summary>
    /// Convierte un resultado con valor en una respuesta HTTP.
    /// </summary>
    public static IActionResult ToActionResult<T>(this ControllerBase controller, Result<T> result)
    {
        if (result.IsSuccess)
        {
            return controller.Ok(result.Value);
        }

        return controller.BadRequest(new { error = result.Error });
    }

    /// <summary>
    /// Convierte un resultado sin valor en una respuesta HTTP.
    /// </summary>
    public static IActionResult ToActionResult(this ControllerBase controller, Result result)
    {
        if (result.IsSuccess)
        {
            return controller.Ok();
        }

        return controller.BadRequest(new { error = result.Error });
    }
}
