using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace CineAPI.Repository
{
    public class SesionRepository : ISesionRepository
    {
        private readonly string _connectionString;

        public SesionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Sesion>> GetAllAsync()
        {
            var sesiones = new List<Sesion>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT SesionId, SalaId, PeliculaId, FechaDeSesion, HoraDeInicio FROM Sesiones";
                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        sesiones.Add(new Sesion
                        {
                            SesionId = reader.GetInt32(0),
                            SalaId = reader.GetInt32(1),
                            PeliculaId = reader.GetInt32(2),
                            FechaDeSesion = reader.GetDateTime(3),
                            HoraDeInicio = reader.GetDateTime(4)
                        });
                    }
                }
            }
            return sesiones;
        }

        public async Task<Sesion?> GetByIdAsync(int sesionId)
        {
            Sesion? sesion = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT SesionId, SalaId, PeliculaId, FechaDeSesion, HoraDeInicio FROM Sesiones WHERE SesionId = @SesionId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SesionId", sesionId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sesion = new Sesion
                            {
                                SesionId = reader.GetInt32(0),
                                SalaId = reader.GetInt32(1),
                                PeliculaId = reader.GetInt32(2),
                                FechaDeSesion = reader.GetDateTime(3),
                                HoraDeInicio = reader.GetDateTime(4)
                            };
                        }
                    }
                }
            }
            return sesion;
        }

        public async Task<List<Sesion>> GetBySalaIdAsync(int salaId)
        {
            var sesiones = new List<Sesion>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT SesionId, SalaId, PeliculaId, FechaDeSesion, HoraDeInicio FROM Sesiones WHERE SalaId = @SalaId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SalaId", salaId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            sesiones.Add(new Sesion
                            {
                                SesionId = reader.GetInt32(0),
                                SalaId = reader.GetInt32(1),
                                PeliculaId = reader.GetInt32(2),
                                FechaDeSesion = reader.GetDateTime(3),
                                HoraDeInicio = reader.GetDateTime(4)
                            });
                        }
                    }
                }
            }
            return sesiones;
        }

        public async Task<int> AddAsync(Sesion sesion)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO Sesiones (SalaId, PeliculaId, FechaDeSesion, HoraDeInicio) VALUES (@SalaId, @PeliculaId, @FechaDeSesion, @HoraDeInicio); SELECT SCOPE_IDENTITY();";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SalaId", sesion.SalaId);
                    command.Parameters.AddWithValue("@PeliculaId", sesion.PeliculaId);
                    command.Parameters.AddWithValue("@FechaDeSesion", sesion.FechaDeSesion);
                    command.Parameters.AddWithValue("@HoraDeInicio", sesion.HoraDeInicio);

                    return Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }

        public async Task<bool> DeleteAsync(int sesionId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Sesiones WHERE SesionId = @SesionId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SesionId", sesionId);
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }
    }
}