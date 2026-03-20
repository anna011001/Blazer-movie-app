using Filmovi.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Filmovi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KinaController : ControllerBase
{
    private readonly AppDbContext _db;

    public KinaController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var kina = await _db.Kina
            .Include(k => k.KinoFilmovi)
            .ThenInclude(kf => kf.Film)
            .Select(k => new
            {
                k.Id,
                k.Naziv,
                Filmovi = k.KinoFilmovi.Select(kf => new
                {
                    kf.Id,
                    FilmId = kf.FilmId,
                    NazivFilma = kf.Film!.Naziv,
                    SifraFilma = kf.Film!.SifraFilma,
                    kf.DatumProjekcije,
                    kf.CijenaKarte
                })
            })
            .ToListAsync();

        return Ok(kina);
    }
}