namespace Filmovi.Api.Models;

public class KinoFilm
{
    public int Id { get; set; }

    public int KinoId { get; set; }
    public Kino? Kino { get; set; }


    public int FilmId { get; set; }
    public Film? Film { get; set; }


    public DateTime DatumProjekcije { get; set; }
    public decimal CijenaKarte { get; set; }
}