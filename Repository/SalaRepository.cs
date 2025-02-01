using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace CineAPI.Repository
{
    public class SalaRepository : ISalaRepository
    {
        private readonly string _connectionString;

        public SalaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Sala>> GetAllAsync()
        {
            var salas = new List<Sala>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT SalaId, Nombre, CineId FROM Salas";
                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        salas.Add(new Sala
                        {
                            SalaId = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            CineId = reader.GetInt32(2)
                        });
                    }
                }
            }
            return salas;
        }

        public async Task<Sala?> GetByIdAsync(int salaId)
        {
            Sala? sala = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT SalaId, Nombre, CineId FROM Salas WHERE SalaId = @SalaId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SalaId", salaId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sala = new Sala
                            {
                                SalaId = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                CineId = reader.GetInt32(2)
                            };
                        }
                    }
                }
            }
            return sala;
        }
    }
}
