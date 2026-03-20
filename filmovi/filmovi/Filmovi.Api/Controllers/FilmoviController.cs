using Filmovi.Api.Data;
using Filmovi.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Filmovi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilmoviController : ControllerBase
{
    private readonly AppDbContext _db;

    public FilmoviController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateFilmRequest request)
    {
        var film = new Film
        {
            Naziv = request.Naziv,
            SifraFilma = request.SifraFilma
        };

        _db.Filmovi.Add(film);
        await _db.SaveChangesAsync();

        var kinoFilm = new KinoFilm
        {
            FilmId = film.Id,
            KinoId = request.KinoId,
            DatumProjekcije = request.DatumProjekcije,
            CijenaKarte = request.CijenaKarte
        };

        _db.KinoFilmovi.Add(kinoFilm);
        await _db.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateFilmRequest request)
    {
        var film = await _db.Filmovi
            .Include(f => f.KinoFilmovi)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (film == null)
            return NotFound();

        film.Naziv = request.Naziv;
        film.SifraFilma = request.SifraFilma;

        var kinoFilm = film.KinoFilmovi.FirstOrDefault();
        if (kinoFilm != null)
        {
            kinoFilm.DatumProjekcije = request.DatumProjekcije;
            kinoFilm.CijenaKarte = request.CijenaKarte;
        }

        await _db.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var film = await _db.Filmovi
            .Include(f => f.KinoFilmovi)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (film == null)
            return NotFound();

        _db.KinoFilmovi.RemoveRange(film.KinoFilmovi);
        _db.Filmovi.Remove(film);

        await _db.SaveChangesAsync();

        return Ok();
    }
}

public class CreateFilmRequest
{
    public string Naziv { get; set; } = "";
    public string SifraFilma { get; set; } = "";
    public int KinoId { get; set; }
    public DateTime DatumProjekcije { get; set; }
    public decimal CijenaKarte { get; set; }
}

public class UpdateFilmRequest
{
    public string Naziv { get; set; } = "";
    public string SifraFilma { get; set; } = "";
    public DateTime DatumProjekcije { get; set; }
    public decimal CijenaKarte { get; set; }
}