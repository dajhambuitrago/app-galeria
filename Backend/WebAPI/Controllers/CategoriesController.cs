using Backend.Application.Features.Categories.Commands;
using Backend.Application.Features.Categories.Queries;
using Backend.WebAPI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebAPI.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Obtiene todas las categorías disponibles.
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCategoriesQuery(), cancellationToken);
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Crea una nueva categoría.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Actualiza una categoría existente.
    /// </summary>
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command with { Id = id }, cancellationToken);
        return this.ToActionResult(result);
    }

    /// <summary>
    /// Elimina una categoría por id.
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteCategoryCommand(id), cancellationToken);
        return this.ToActionResult(result);
    }
}
