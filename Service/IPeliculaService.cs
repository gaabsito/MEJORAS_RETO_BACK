namespace CineAPI.Services
{
    public interface IPeliculaService
    {
        Task<List<Pelicula>> GetAllPeliculasAsync();
        Task<Pelicula?> GetPeliculaByIdAsync(int peliculaId);
        Task<List<Pelicula>> GetPeliculasByTitleAsync(string title);
        Task<List<Pelicula>> GetPeliculasByGeneroAsync(string genero);
        Task InicializarDatosAsync();
    }
}