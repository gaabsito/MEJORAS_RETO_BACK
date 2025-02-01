using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineAPI.Repository;

namespace CineAPI.Service
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            try
            {
                return await _ticketRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los tickets", ex);
            }
        }

        public async Task<Ticket?> GetTicketByIdAsync(int ticketId)
        {
            try
            {
                var ticket = await _ticketRepository.GetByIdAsync(ticketId);
                if (ticket == null)
                {
                    throw new KeyNotFoundException($"No se encontró el ticket con ID {ticketId}");
                }
                return ticket;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el ticket con ID {ticketId}", ex);
            }
        }

        public async Task<List<Ticket>> GetTicketsByEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentException("El email de búsqueda no puede estar vacío");
                }

                var tickets = await _ticketRepository.GetByEmailAsync(email);
                if (!tickets.Any())
                {
                    throw new KeyNotFoundException($"No se encontraron tickets para el email '{email}'");
                }
                return tickets;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al buscar tickets por email '{email}'", ex);
            }
        }

        public async Task<List<Ticket>> GetTicketsBySesionAsync(int sesionId)
        {
            try
            {
                var tickets = await _ticketRepository.GetBySesionAsync(sesionId);
                if (!tickets.Any())
                {
                    throw new KeyNotFoundException($"No se encontraron tickets para la sesión con ID {sesionId}");
                }
                return tickets;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al buscar tickets por sesión ID {sesionId}", ex);
            }
        }

        public async Task<int> AddTicketAsync(Ticket ticket)
        {
            try
            {
                return await _ticketRepository.AddAsync(ticket);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar un nuevo ticket", ex);
            }
        }

        public async Task<bool> DeleteTicketAsync(int ticketId)
        {
            try
            {
                return await _ticketRepository.DeleteAsync(ticketId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el ticket con ID {ticketId}", ex);
            }
        }
    }
}