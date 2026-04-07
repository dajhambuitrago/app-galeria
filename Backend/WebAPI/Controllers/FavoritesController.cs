using System.Security.Claims;
using Backend.Application.Features.Favorites.Commands;
using Backend.Application.Features.Favorites.Queries;
using Backend.WebAPI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebAPI.Controllers;

[ApiController]
[Route("api/favorites")]
[Authorize(Roles = "User")]
public class FavoritesController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Obtiene los favoritos del usuario autenticado.
    /// </summary>
    [HttpGet("me")]
    public async Task<IActionResult> GetMyFavorites(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await mediator.Send(new GetMyFavoritesQuery(userId), cancellationToken);
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Agrega un medio a favoritos del usuario autenticado.
    /// </summary>
    [HttpPost("{mediaId:guid}")]
    public async Task<IActionResult> Add(Guid mediaId, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await mediator.Send(new AddFavoriteCommand(userId, mediaId), cancellationToken);
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Quita un medio de favoritos del usuario autenticado.
    /// </summary>
    [HttpDelete("{mediaId:guid}")]
    public async Task<IActionResult> Remove(Guid mediaId, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var result = await mediator.Send(new RemoveFavoriteCommand(userId, mediaId), cancellationToken);
        return this.ToActionResult(result);
    }
}
