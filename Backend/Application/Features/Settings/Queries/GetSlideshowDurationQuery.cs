using Backend.Application.Abstractions.Repositories;
using Backend.Application.DTOs;
using Backend.Domain.Common;
using MediatR;

namespace Backend.Application.Features.Settings.Queries;

/// <summary>
/// Consulta para obtener la duración configurada del slideshow.
/// </summary>
public record GetSlideshowDurationQuery : IRequest<Result<SettingDto>>;

/// <summary>
/// Maneja la consulta de configuración del slideshow.
/// </summary>
public class GetSlideshowDurationQueryHandler(ISettingRepository repository) : IRequestHandler<GetSlideshowDurationQuery, Result<SettingDto>>
{
    /// <summary>
    /// Obtiene la configuración y la proyecta a DTO.
    /// </summary>
    public async Task<Result<SettingDto>> Handle(GetSlideshowDurationQuery request, CancellationToken cancellationToken)
    {
        var settings = await repository.GetAsync(cancellationToken);
        if (settings is null)
        {
            return Result<SettingDto>.Failure("Configuración no encontrada.");
        }

        return Result<SettingDto>.Success(new SettingDto(settings.SlideshowDurationSeconds));
    }
}
