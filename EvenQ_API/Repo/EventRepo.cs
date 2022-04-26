using EvenQ_API.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace EvenQ_API.Repo
{
    public class EventRepo : IEvents
    {
        private readonly AppDbContext appDbContext;

        public EventRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }



        public async Task<Event> AddEvent(Event events)
        {
            if (events.Location != null)
            {
                appDbContext.Entry(events.Location).State = EntityState.Unchanged;
            }

            var result = await appDbContext.Events.AddAsync(events);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteEvent(int eventID)
        {
            var results = await appDbContext.Events.FirstOrDefaultAsync(e => e.IDEvent == eventID);

            if (results != null)
            {
                appDbContext.Events.Remove(results);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Event> GetEvent(int eventID)
        {
            return await appDbContext.Events.Include(e => e.Location).FirstOrDefaultAsync(e => e.IDEvent == eventID);
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            return await appDbContext.Events.Include(e => e.Location).ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetUpcomingEvents()
        {
         
         return await appDbContext.Events.Where(e => e.Date > DateTime.UtcNow).Include(e => e.Location).ToListAsync();
        }


        public async Task<IEnumerable<Event>> SearchEventTitle(string name)
        {
            IQueryable<Event> query = appDbContext.Events;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Title.Contains(name));
            }

            return await query.ToListAsync();
        }

        public async Task<Event> UpdateEvent(Event events)
        {
            var results = await appDbContext.Events.FirstOrDefaultAsync(e => e.IDEvent == events.IDEvent);


            if (results != null)
            {
                results.Title = events.Title;
                results.Description = events.Description;
                results.PosterImage = events.PosterImage;
                results.Date = events.Date;
                if (events.LocationId != 0)
                {
                    results.LocationId = events.LocationId;
                }
                else if (events.Location != null)
                {
                    results.LocationId = events.Location.IDLocation;
                }
              
                if (events.TicketPrice != 0)
                {
                    results.TicketPrice = events.TicketPrice;
                }
 

                await appDbContext.SaveChangesAsync();

                return results;
            }

            return null;
        }
    }
}
