using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace CineAPI.Repository
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAllAsync();
        Task<Ticket?> GetByIdAsync(int ticketId);
        Task<List<Ticket>> GetByEmailAsync(string email);
        Task<List<Ticket>> GetBySesionAsync(int sesionId);
        Task<int> AddAsync(Ticket ticket);
        Task<bool> DeleteAsync(int ticketId);
    }

    public class TicketRepository : ITicketRepository
    {
        private readonly string _connectionString;

        public TicketRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Ticket>> GetAllAsync()
        {
            var tickets = new List<Ticket>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT TicketId, SesionId, NombreInvitado, EmailCompra, ButacaId, FechaDeCompra FROM Tickets";
                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        tickets.Add(new Ticket
                        {
                            TicketId = reader.GetInt32(0),
                            SesionId = reader.GetInt32(1),
                            NombreInvitado = reader.GetString(2),
                            EmailCompra = reader.GetString(3),
                            ButacaId = reader.GetInt32(4),
                            FechaDeCompra = reader.GetDateTime(5)
                        });
                    }
                }
            }
            return tickets;
        }

        public async Task<Ticket?> GetByIdAsync(int ticketId)
        {
            Ticket? ticket = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT TicketId, SesionId, NombreInvitado, EmailCompra, ButacaId, FechaDeCompra FROM Tickets WHERE TicketId = @TicketId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TicketId", ticketId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            ticket = new Ticket
                            {
                                TicketId = reader.GetInt32(0),
                                SesionId = reader.GetInt32(1),
                                NombreInvitado = reader.GetString(2),
                                EmailCompra = reader.GetString(3),
                                ButacaId = reader.GetInt32(4),
                                FechaDeCompra = reader.GetDateTime(5)
                            };
                        }
                    }
                }
            }
            return ticket;
        }

        public async Task<List<Ticket>> GetByEmailAsync(string email)
        {
            var tickets = new List<Ticket>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT TicketId, SesionId, NombreInvitado, EmailCompra, ButacaId, FechaDeCompra FROM Tickets WHERE EmailCompra = @Email";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            tickets.Add(new Ticket
                            {
                                TicketId = reader.GetInt32(0),
                                SesionId = reader.GetInt32(1),
                                NombreInvitado = reader.GetString(2),
                                EmailCompra = reader.GetString(3),
                                ButacaId = reader.GetInt32(4),
                                FechaDeCompra = reader.GetDateTime(5)
                            });
                        }
                    }
                }
            }
            return tickets;
        }

        public async Task<List<Ticket>> GetBySesionAsync(int sesionId)
        {
            var tickets = new List<Ticket>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT TicketId, SesionId, NombreInvitado, EmailCompra, ButacaId, FechaDeCompra FROM Tickets WHERE SesionId = @SesionId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SesionId", sesionId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            tickets.Add(new Ticket
                            {
                                TicketId = reader.GetInt32(0),
                                SesionId = reader.GetInt32(1),
                                NombreInvitado = reader.GetString(2),
                                EmailCompra = reader.GetString(3),
                                ButacaId = reader.GetInt32(4),
                                FechaDeCompra = reader.GetDateTime(5)
                            });
                        }
                    }
                }
            }
            return tickets;
        }

        public async Task<int> AddAsync(Ticket ticket)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO Tickets (SesionId, NombreInvitado, EmailCompra, ButacaId, FechaDeCompra) VALUES (@SesionId, @NombreInvitado, @EmailCompra, @ButacaId, @FechaDeCompra); SELECT SCOPE_IDENTITY();";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SesionId", ticket.SesionId);
                    command.Parameters.AddWithValue("@NombreInvitado", ticket.NombreInvitado);
                    command.Parameters.AddWithValue("@EmailCompra", ticket.EmailCompra);
                    command.Parameters.AddWithValue("@ButacaId", ticket.ButacaId);
                    command.Parameters.AddWithValue("@FechaDeCompra", ticket.FechaDeCompra);
                    return Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }

        public async Task<bool> DeleteAsync(int ticketId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Tickets WHERE TicketId = @TicketId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TicketId", ticketId);
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }
    }
}