using Backend.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Backend.Infrastructure.Identity;

/// <summary>
/// Usuario de la aplicación con relaciones adicionales.
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Roles asignados al usuario.
    /// </summary>
    public ICollection<IdentityUserRole<string>> UserRoles { get; set; } = new List<IdentityUserRole<string>>();
    /// <summary>
    /// Favoritos asociados al usuario.
    /// </summary>
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
}
