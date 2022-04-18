using EvenQ_API.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenQ_API.Repo
{
    public class LocationRepo : ILocation
    {
        private readonly AppDbContext appDbContext;

        public LocationRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        public async Task<Location> AddLocation(Location location)
        {
            if (location.IDLocation != 0)
            {
                appDbContext.Entry(location.IDLocation).State = EntityState.Unchanged;
            }

            var result = await appDbContext.Locations.AddAsync(location);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteLocation(int locationID)
        {
            var results = await appDbContext.Locations.FirstOrDefaultAsync(l => l.IDLocation == locationID);

            if (results != null)
            {
                appDbContext.Locations.Remove(results);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Location> GetLocation(int locationID)
        {
            return await appDbContext.Locations.FirstOrDefaultAsync(l => l.IDLocation == locationID);
        }

        public async Task<IEnumerable<Location>> GetLocations()
        {
            return await appDbContext.Locations.ToListAsync();
        }

        public async Task<IEnumerable<Location>> SearchLocation(string name)
        {
            IQueryable<Location> query = appDbContext.Locations;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(l => l.City.Contains(name) || l.Street.Contains(name));
            }

            return await query.ToListAsync();
        }

        public async Task<Location> UpdateLocation(Location location)
        {
            var results = await appDbContext.Locations.FirstOrDefaultAsync(l => l.IDLocation == location.IDLocation);
            if (results != null)
            {
                results.City = location.City;
                results.Street = location.Street;
                results.Coordinates = location.Coordinates;

                await appDbContext.SaveChangesAsync();

                return results;
            }

            return null;
        }
    }
}
