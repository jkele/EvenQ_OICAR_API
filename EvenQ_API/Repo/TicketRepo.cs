using EvenQ_API.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenQ_API.Repo
{
    public class TicketRepo : ITickets
    {

        private readonly AppDbContext appDbContext;

        public TicketRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Ticket> AddTicket(Ticket ticket)
        {
            if (ticket.Member != null)
            {
                appDbContext.Entry(ticket.Member).State = EntityState.Unchanged;
            }

            if (ticket.Event != null)
            {
                appDbContext.Entry(ticket.Event).State = EntityState.Unchanged;
            }

            var result = await appDbContext.Tickets.AddAsync(ticket);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteTicket(int ticketID)
        {
            var results = await appDbContext.Tickets.FirstOrDefaultAsync(t => t.IDTicket == ticketID);

            if (results != null)
            {
                appDbContext.Tickets.Remove(results);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Ticket> GetTicket(int ticketID)
        {
            return await appDbContext.Tickets.Include(t => t.Member).Include(t => t.Event).FirstOrDefaultAsync(t => t.IDTicket == ticketID);
        }

        public async Task<IEnumerable<Ticket>> GetTicketByUID(string UID)
        {
            return await appDbContext.Tickets.Where(e => e.MemberId == UID).Include(t => t.Member).Include(t => t.Event).ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTickets()
        {
            return await appDbContext.Tickets.Include(e => e.Member).Include(e => e.Event).ToListAsync();
        }

        public async Task<Ticket> UpdateTicket(Ticket ticket)
        {
            var results = await appDbContext.Tickets.FirstOrDefaultAsync(t => t.IDTicket == ticket.IDTicket);

            if (results != null)
            {
                results.TicketQR = ticket.TicketQR;
                if (ticket.Member != null)
                {
                    results.MemberId = ticket.Member.UID;
                }
                results.MemberId = ticket.MemberId;

                if (ticket.Event != null)
                {
                    results.EventId = ticket.Event.IDEvent;
                }
                results.EventId = ticket.EventId;
                results.IsValid = ticket.IsValid;

                await appDbContext.SaveChangesAsync();

                return results;
            }

            return null;
        }
    }
}
