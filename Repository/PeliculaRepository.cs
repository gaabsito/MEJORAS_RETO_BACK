using Microsoft.Data.SqlClient;

namespace CineAPI.Repository
{
    public class PeliculaRepository : IPeliculaRepository
    {
        private readonly string _connectionString;

        public PeliculaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Pelicula>> GetAllAsync()
        {
            var peliculas = new List<Pelicula>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT PeliculaId, Title, Description, Director, Actores, Genero, Clasificacion, Duration, ImageUrl, CartelUrl FROM Peliculas";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var pelicula = new Pelicula
                            {
                                PeliculaId = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Director = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Actores = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Genero = reader.IsDBNull(5) ? null : reader.GetString(5),
                                Clasificacion = reader.IsDBNull(6) ? null : reader.GetString(6),
                                Duracion = reader.IsDBNull(7) ? 0 : reader.GetInt32(7),
                                ImageUrl = reader.IsDBNull(8) ? null : reader.GetString(8),
                                CartelUrl = reader.IsDBNull(9) ? null : reader.GetString(9)
                            };

                            peliculas.Add(pelicula);
                        }
                    }
                }
            }
            return peliculas;
        }

        public async Task<Pelicula?> GetByIdAsync(int peliculaId)
        {
            Pelicula? pelicula = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT PeliculaId, Title, Description, Director, Actores, Genero, Clasificacion, Duration, ImageUrl, CartelUrl FROM Peliculas WHERE PeliculaId = @PeliculaId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PeliculaId", peliculaId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            pelicula = new Pelicula
                            {
                                PeliculaId = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Director = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Actores = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Genero = reader.IsDBNull(5) ? null : reader.GetString(5),
                                Clasificacion = reader.IsDBNull(6) ? null : reader.GetString(6),
                                Duracion = reader.IsDBNull(7) ? 0 : reader.GetInt32(7),
                                ImageUrl = reader.IsDBNull(8) ? null : reader.GetString(8),
                                CartelUrl = reader.IsDBNull(9) ? null : reader.GetString(9)
                            };
                        }
                    }
                }
            }
            return pelicula;
        }

        public async Task<List<Pelicula>> GetByTitleAsync(string title)
        {
            var peliculas = new List<Pelicula>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT PeliculaId, Title, Description, Director, Actores, Genero, Clasificacion, Duration, ImageUrl, CartelUrl FROM Peliculas WHERE Title LIKE @Title";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", $"%{title}%");

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var pelicula = new Pelicula
                            {
                                PeliculaId = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Director = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Actores = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Genero = reader.IsDBNull(5) ? null : reader.GetString(5),
                                Clasificacion = reader.IsDBNull(6) ? null : reader.GetString(6),
                                Duracion = reader.IsDBNull(7) ? 0 : reader.GetInt32(7),
                                ImageUrl = reader.IsDBNull(8) ? null : reader.GetString(8),
                                CartelUrl = reader.IsDBNull(9) ? null : reader.GetString(9)
                            };

                            peliculas.Add(pelicula);
                        }
                    }
                }
            }
            return peliculas;
        }

        public async Task<List<Pelicula>> GetByGeneroAsync(string genero)
        {
            var peliculas = new List<Pelicula>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT PeliculaId, Title, Description, Director, Actores, Genero, Clasificacion, Duration, ImageUrl, CartelUrl FROM Peliculas WHERE Genero LIKE @Genero";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Genero", $"%{genero}%");

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var pelicula = new Pelicula
                            {
                                PeliculaId = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Director = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Actores = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Genero = reader.IsDBNull(5) ? null : reader.GetString(5),
                                Clasificacion = reader.IsDBNull(6) ? null : reader.GetString(6),
                                Duracion = reader.IsDBNull(7) ? 0 : reader.GetInt32(7),
                                ImageUrl = reader.IsDBNull(8) ? null : reader.GetString(8),
                                CartelUrl = reader.IsDBNull(9) ? null : reader.GetString(9)
                            };

                            peliculas.Add(pelicula);
                        }
                    }
                }
            }
            return peliculas;
        }

        public async Task InicializarDatosAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = @"
                    INSERT INTO Peliculas (Title, Description, Director, Actores, Genero, Clasificacion, Duration)
                    VALUES 
                    (@Title1, @Description1, @Director1, @Actores1, @Genero1, @Clasificacion1, @Duration1),
                    (@Title2, @Description2, @Director2, @Actores2, @Genero2, @Clasificacion2, @Duration2)";

                using (var command = new SqlCommand(query, connection))
                {
                    // Primera película
                    command.Parameters.AddWithValue("@Title1", "Inception");
                    command.Parameters.AddWithValue("@Description1", "Un ladrón que roba secretos corporativos a través del uso de la tecnología para compartir sueños.");
                    command.Parameters.AddWithValue("@Director1", "Christopher Nolan");
                    command.Parameters.AddWithValue("@Actores1", "Leonardo DiCaprio, Joseph Gordon-Levitt, Ellen Page");
                    command.Parameters.AddWithValue("@Genero1", "Ciencia ficción");
                    command.Parameters.AddWithValue("@Clasificacion1", "PG-13");
                    command.Parameters.AddWithValue("@Duration1", 148);

                    // Segunda película
                    command.Parameters.AddWithValue("@Title2", "The Dark Knight");
                    command.Parameters.AddWithValue("@Description2", "Batman se enfrenta al Joker en una batalla por el alma de Ciudad Gótica.");
                    command.Parameters.AddWithValue("@Director2", "Christopher Nolan");
                    command.Parameters.AddWithValue("@Actores2", "Christian Bale, Heath Ledger, Aaron Eckhart");
                    command.Parameters.AddWithValue("@Genero2", "Acción");
                    command.Parameters.AddWithValue("@Clasificacion2", "PG-13");
                    command.Parameters.AddWithValue("@Duration2", 152);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}