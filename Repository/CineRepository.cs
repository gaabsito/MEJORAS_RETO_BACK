using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace CineAPI.Repository
{
    public class CineRepository : ICineRepository
    {
        private readonly string _connectionString;

        public CineRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Cine>> GetAllAsync()
        {
            var cines = new List<Cine>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT CineId, Nombre, Ubicacion FROM Cines";
                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        cines.Add(new Cine
                        {
                            CineId = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Ubicacion = reader.GetString(2)
                        });
                    }
                }
            }
            return cines;
        }

        public async Task<Cine?> GetByIdAsync(int cineId)
        {
            Cine? cine = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT CineId, Nombre, Ubicacion FROM Cines WHERE CineId = @CineId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CineId", cineId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            cine = new Cine
                            {
                                CineId = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Ubicacion = reader.GetString(2)
                            };
                        }
                    }
                }
            }
            return cine;
        }

        public async Task<List<Sala>> GetSalasByCineIdAsync(int cineId)
        {
            var salas = new List<Sala>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT SalaId, Nombre FROM Salas WHERE CineId = @CineId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CineId", cineId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            salas.Add(new Sala
                            {
                                SalaId = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                CineId = cineId
                            });
                        }
                    }
                }
            }
            return salas;
        }
    }
}
