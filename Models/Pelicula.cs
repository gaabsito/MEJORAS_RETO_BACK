public class Pelicula
{
    public int PeliculaId { get; set; }
    public string Title { get; set; }
    public string Descripcion { get; set; }
    public string Director { get; set; }
    public string Actores { get; set; }
    public string Genero { get; set; }
    public string Clasificacion { get; set; }
    public int Duracion { get; set; }
    public string ImageUrl { get; set; }
    public string CartelUrl { get; set; }

    public Pelicula() {} //parameterless constructor

    public Pelicula(string title, string descripcion, string director, string actores, 
                   string genero, string clasificacion, int duracion)
    {
        Title = title;
        Descripcion = descripcion;
        Director = director;
        Actores = actores;
        Genero = genero;
        Clasificacion = clasificacion;
        Duracion = duracion;
    }
}