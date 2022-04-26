using EvenQ_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenQ_API.Repo
{
    public interface ITickets
    {
        Task<IEnumerable<Ticket>> GetTickets();
        Task<Ticket> GetTicket(int ticketID);
        Task<IEnumerable<Ticket>> GetTicketByUID(string UID);
        Task<Ticket> AddTicket(Ticket Ticket);
        Task<Ticket> UpdateTicket(Ticket Ticket);
        Task DeleteTicket(int ticketID);
    }
}
