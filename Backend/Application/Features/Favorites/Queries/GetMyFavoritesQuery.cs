using AutoMapper;
using Backend.Application.Abstractions.Repositories;
using Backend.Application.DTOs;
using Backend.Domain.Common;
using MediatR;

namespace Backend.Application.Features.Favorites.Queries;

/// <summary>
/// Consulta para obtener los favoritos del usuario.
/// </summary>
public record GetMyFavoritesQuery(string UserId) : IRequest<Result<List<FavoriteDto>>>;

/// <summary>
/// Maneja la consulta de favoritos del usuario.
/// </summary>
public class GetMyFavoritesQueryHandler(IFavoriteRepository repository, IMapper mapper) : IRequestHandler<GetMyFavoritesQuery, Result<List<FavoriteDto>>>
{
    /// <summary>
    /// Obtiene favoritos y los mapea a DTOs.
    /// </summary>
    public async Task<Result<List<FavoriteDto>>> Handle(GetMyFavoritesQuery request, CancellationToken cancellationToken)
    {
        var favorites = await repository.GetByUserIdAsync(request.UserId, cancellationToken);
        return Result<List<FavoriteDto>>.Success(mapper.Map<List<FavoriteDto>>(favorites));
    }
}
