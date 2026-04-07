using AutoMapper;
using Backend.Application.Abstractions.Repositories;
using Backend.Application.DTOs;
using Backend.Domain.Common;
using Backend.Domain.Enums;
using MediatR;

namespace Backend.Application.Features.Media.Queries;

/// <summary>
/// Consulta para filtrar medios por categoría y tipo.
/// </summary>
public record GetMediaByFilterQuery(string? Category, MediaType? Type) : IRequest<Result<List<MediaDto>>>;

/// <summary>
/// Maneja la consulta de filtrado de medios.
/// </summary>
public class GetMediaByFilterQueryHandler(IMediaRepository repository, IMapper mapper) : IRequestHandler<GetMediaByFilterQuery, Result<List<MediaDto>>>
{
    /// <summary>
    /// Obtiene y mapea los medios filtrados.
    /// </summary>
    public async Task<Result<List<MediaDto>>> Handle(GetMediaByFilterQuery request, CancellationToken cancellationToken)
    {
        var items = await repository.FilterAsync(request.Category, request.Type, cancellationToken);
        return Result<List<MediaDto>>.Success(mapper.Map<List<MediaDto>>(items));
    }
}
