namespace Filmovi.Api.Models;

public class Film
{
    public int Id { get; set; }
    public string Naziv { get; set; } = "";
    public string SifraFilma { get; set; } = "";
    public List<KinoFilm> KinoFilmovi { get; set; } = new();
}