public class Sesion
{
    public int SesionId { get; set; }
    public int SalaId { get; set; }
    public int PeliculaId { get; set; }
    public DateTime FechaDeSesion { get; set; }
    public DateTime HoraDeInicio { get; set; }
    public List<Butaca> Butacas { get; set; } = new List<Butaca>();
}