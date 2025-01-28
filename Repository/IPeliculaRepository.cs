namespace CineAPI.Repository
{
    public interface IPeliculaRepository
    {
        Task<List<Pelicula>> GetAllAsync();
        Task<Pelicula?> GetByIdAsync(int peliculaId);
        Task<List<Pelicula>> GetByTitleAsync(string title);
        Task<List<Pelicula>> GetByGeneroAsync(string genero);
        Task InicializarDatosAsync();
    }
}