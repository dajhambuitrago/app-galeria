using Backend.Application.Abstractions.Repositories;
using Backend.Domain.Common;
using MediatR;

namespace Backend.Application.Features.Media.Commands;

/// <summary>
/// Comando para eliminar un medio por id.
/// </summary>
public record DeleteMediaCommand(Guid Id) : IRequest<Result>;

/// <summary>
/// Maneja la lógica de eliminación de medios.
/// </summary>
public class DeleteMediaCommandHandler(IMediaRepository repository) : IRequestHandler<DeleteMediaCommand, Result>
{
    /// <summary>
    /// Elimina el medio si existe.
    /// </summary>
    public async Task<Result> Handle(DeleteMediaCommand request, CancellationToken cancellationToken)
    {
        var media = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (media is null)
        {
            return Result.Failure("Archivo multimedia no encontrado.");
        }

        await repository.DeleteAsync(media, cancellationToken);
        return Result.Success();
    }
}
