using Backend.Domain.Enums;

namespace Backend.Application.DTOs;

/// <summary>
/// Representa un medio para consumo externo.
/// </summary>
public record MediaDto(Guid Id, string Title, string Url, string Description, MediaType Type, string CategoryName);
