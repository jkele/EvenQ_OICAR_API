using EvenQ_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenQ_API.Repo
{
    public interface IEvents
    {
        Task<IEnumerable<Event>> SearchEventTitle(string name);
        Task<IEnumerable<Event>> GetEvents();
        Task<IEnumerable<Event>> GetUpcomingEvents();
        Task<Event> GetEvent(int eventID);
        Task<Event> AddEvent(Event Event);
        Task<Event> UpdateEvent(Event Event);
        Task DeleteEvent(int eventID);
    }
}
