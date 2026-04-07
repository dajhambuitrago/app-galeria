using Backend.Application.Abstractions.Repositories;
using Backend.Domain.Common;
using FluentValidation;
using MediatR;

namespace Backend.Application.Features.Favorites.Commands;

/// <summary>
/// Comando para quitar un medio de favoritos.
/// </summary>
public record RemoveFavoriteCommand(string UserId, Guid MediaId) : IRequest<Result>;

/// <summary>
/// Valida el comando de quitar favorito.
/// </summary>
public class RemoveFavoriteCommandValidator : AbstractValidator<RemoveFavoriteCommand>
{
    public RemoveFavoriteCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.MediaId).NotEmpty();
    }
}

/// <summary>
/// Maneja la lógica para quitar un medio de favoritos.
/// </summary>
public class RemoveFavoriteCommandHandler(IFavoriteRepository repository, IValidator<RemoveFavoriteCommand> validator) : IRequestHandler<RemoveFavoriteCommand, Result>
{
    /// <summary>
    /// Ejecuta la validación y elimina el favorito si existe.
    /// </summary>
    public async Task<Result> Handle(RemoveFavoriteCommand request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            return Result.Failure(validation.Errors.First().ErrorMessage);
        }

        var favorite = await repository.GetByUserIdAndMediaIdAsync(request.UserId, request.MediaId, cancellationToken);
        if (favorite is null)
        {
            return Result.Success();
        }

        await repository.DeleteAsync(favorite, cancellationToken);
        return Result.Success();
    }
}
