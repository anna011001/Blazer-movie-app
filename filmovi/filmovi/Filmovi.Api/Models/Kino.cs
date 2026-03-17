namespace Filmovi.Api.Models;

public class Kino
{
    public int Id { get; set; }
    public string Naziv { get; set; } = "";
    public List<KinoFilm> KinoFilmovi { get; set; } = new();
}