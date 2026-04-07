using AutoMapper;
using Backend.Application.Abstractions.Repositories;
using Backend.Application.DTOs;
using Backend.Domain.Common;
using MediatR;

namespace Backend.Application.Features.Categories.Queries;

/// <summary>
/// Consulta para obtener todas las categorías.
/// </summary>
public record GetCategoriesQuery() : IRequest<Result<List<CategoryDto>>>;

/// <summary>
/// Maneja la consulta de categorías.
/// </summary>
public class GetCategoriesQueryHandler(ICategoryRepository repository, IMapper mapper) : IRequestHandler<GetCategoriesQuery, Result<List<CategoryDto>>>
{
    /// <summary>
    /// Obtiene categorías y las mapea a DTOs.
    /// </summary>
    public async Task<Result<List<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await repository.GetAllAsync(cancellationToken);
        return Result<List<CategoryDto>>.Success(mapper.Map<List<CategoryDto>>(categories));
    }
}
