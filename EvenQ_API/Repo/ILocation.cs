using EvenQ_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenQ_API.Repo
{
    public interface ILocation
    {
        Task<IEnumerable<Location>> SearchLocation(string name);
        Task<IEnumerable<Location>> GetLocations();
        Task<Location> GetLocation(int locationID);
        Task<Location> AddLocation(Location Location);
        Task<Location> UpdateLocation(Location Location);
        Task DeleteLocation(int locationID);
    }
}
