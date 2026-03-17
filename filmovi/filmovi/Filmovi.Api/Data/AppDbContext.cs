using Microsoft.EntityFrameworkCore;
using Filmovi.Api.Models;

namespace Filmovi.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Kino> Kina => Set<Kino>();
    public DbSet<Film> Filmovi => Set<Film>();
    public DbSet<KinoFilm> KinoFilmovi => Set<KinoFilm>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KinoFilm>()
            .Property(x => x.CijenaKarte)
            .HasPrecision(18, 2);
    }
}