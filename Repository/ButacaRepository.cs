using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace CineAPI.Repository
{
    public class ButacaRepository : IButacaRepository
    {
        private readonly string _connectionString;

        public ButacaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Butaca>> GetAllAsync()
        {
            var butacas = new List<Butaca>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT ButacaId, SalaId, Estado, PrecioButaca, TicketId FROM Butacas";
                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        butacas.Add(new Butaca
                        {
                            ButacaId = reader.GetInt32(0),
                            SalaId = reader.GetInt32(1),
                            Estado = reader.GetString(2),
                            PrecioButaca = reader.GetDouble(3),
                            TicketId = reader.IsDBNull(4) ? null : reader.GetInt32(4)
                        });
                    }
                }
            }
            return butacas;
        }

        public async Task<Butaca?> GetByIdAsync(int butacaId)
        {
            Butaca? butaca = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT ButacaId, SalaId, Estado, PrecioButaca, TicketId FROM Butacas WHERE ButacaId = @ButacaId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ButacaId", butacaId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            butaca = new Butaca
                            {
                                ButacaId = reader.GetInt32(0),
                                SalaId = reader.GetInt32(1),
                                Estado = reader.GetString(2),
                                PrecioButaca = reader.GetDouble(3),
                                TicketId = reader.IsDBNull(4) ? null : reader.GetInt32(4)
                            };
                        }
                    }
                }
            }
            return butaca;
        }

        public async Task<List<Butaca>> GetBySalaAsync(int salaId)
        {
            var butacas = new List<Butaca>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT ButacaId, SalaId, Estado, PrecioButaca, TicketId FROM Butacas WHERE SalaId = @SalaId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SalaId", salaId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            butacas.Add(new Butaca
                            {
                                ButacaId = reader.GetInt32(0),
                                SalaId = reader.GetInt32(1),
                                Estado = reader.GetString(2),
                                PrecioButaca = reader.GetDouble(3),
                                TicketId = reader.IsDBNull(4) ? null : reader.GetInt32(4)
                            });
                        }
                    }
                }
            }
            return butacas;
        }

        public async Task<int> AddAsync(Butaca butaca)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO Butacas (SalaId, Estado, PrecioButaca, TicketId) VALUES (@SalaId, @Estado, @PrecioButaca, @TicketId); SELECT SCOPE_IDENTITY();";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SalaId", butaca.SalaId);
                    command.Parameters.AddWithValue("@Estado", butaca.Estado);
                    command.Parameters.AddWithValue("@PrecioButaca", butaca.PrecioButaca);
                    command.Parameters.AddWithValue("@TicketId", (object?)butaca.TicketId ?? DBNull.Value);
                    return Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }

        public async Task<bool> UpdateEstadoAsync(int butacaId, string estado)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE Butacas SET Estado = @Estado WHERE ButacaId = @ButacaId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Estado", estado);
                    command.Parameters.AddWithValue("@ButacaId", butacaId);
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public async Task<bool> DeleteAsync(int butacaId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Butacas WHERE ButacaId = @ButacaId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ButacaId", butacaId);
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }
    }
}