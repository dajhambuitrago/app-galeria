using Backend.Domain.Entities;
using Backend.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Persistence;

/// <summary>
/// Contexto de base de datos principal de la aplicación.
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    /// <summary>
    /// Categorías registradas.
    /// </summary>
    public DbSet<Category> Categories => Set<Category>();
    /// <summary>
    /// Medios registrados.
    /// </summary>
    public DbSet<MediaItem> MediaItems => Set<MediaItem>();
    /// <summary>
    /// Favoritos registrados.
    /// </summary>
    public DbSet<Favorite> Favorites => Set<Favorite>();
    /// <summary>
    /// Configuración de la aplicación.
    /// </summary>
    public DbSet<AppSetting> AppSettings => Set<AppSetting>();

    /// <summary>
    /// Inicializa el contexto con las opciones configuradas.
    /// </summary>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Configura relaciones, índices y restricciones del modelo.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();

        builder.Entity<Favorite>()
            .HasIndex(f => new { f.UserId, f.MediaItemId })
            .IsUnique();

        builder.Entity<Favorite>()
            .HasOne(f => f.MediaItem)
            .WithMany(m => m.Favorites)
            .HasForeignKey(f => f.MediaItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<MediaItem>()
            .HasOne(m => m.Category)
            .WithMany(c => c.MediaItems)
            .HasForeignKey(m => m.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
