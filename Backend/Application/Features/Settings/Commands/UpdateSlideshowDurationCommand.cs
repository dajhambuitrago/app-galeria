using Backend.Application.Abstractions.Repositories;
using Backend.Application.DTOs;
using Backend.Domain.Common;
using FluentValidation;
using MediatR;

namespace Backend.Application.Features.Settings.Commands;

/// <summary>
/// Comando para actualizar la duración del slideshow.
/// </summary>
public record UpdateSlideshowDurationCommand(int SlideshowDurationSeconds) : IRequest<Result<SettingDto>>;

/// <summary>
/// Valida el comando de actualización de duración.
/// </summary>
public class UpdateSlideshowDurationCommandValidator : AbstractValidator<UpdateSlideshowDurationCommand>
{
    public UpdateSlideshowDurationCommandValidator()
    {
        RuleFor(x => x.SlideshowDurationSeconds)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(120);
    }
}

/// <summary>
/// Maneja la actualización de la duración del slideshow.
/// </summary>
public class UpdateSlideshowDurationCommandHandler(
    ISettingRepository repository,
    IValidator<UpdateSlideshowDurationCommand> validator) : IRequestHandler<UpdateSlideshowDurationCommand, Result<SettingDto>>
{
    /// <summary>
    /// Valida la solicitud y persiste la configuración.
    /// </summary>
    public async Task<Result<SettingDto>> Handle(UpdateSlideshowDurationCommand request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
        {
            return Result<SettingDto>.Failure(validation.Errors.First().ErrorMessage);
        }

        var setting = await repository.UpsertSlideshowDurationAsync(request.SlideshowDurationSeconds, cancellationToken);
        return Result<SettingDto>.Success(new SettingDto(setting.SlideshowDurationSeconds));
    }
}
