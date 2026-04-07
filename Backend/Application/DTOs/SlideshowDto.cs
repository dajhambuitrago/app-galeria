namespace Backend.Application.DTOs;

/// <summary>
/// Representa la configuración y listado de un slideshow.
/// </summary>
public record SlideshowDto(int SlideshowDurationSeconds, int TotalItems, string EffectiveOrder, List<MediaDto> Items);
