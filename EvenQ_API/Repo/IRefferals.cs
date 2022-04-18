using EvenQ_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenQ_API.Repo
{
    public interface IRefferals
    {
        Task<IEnumerable<Refferals>> SearchInvitee(string invitee);
        Task<IEnumerable<Refferals>> SearchInviter(string inviter);
        Task<IEnumerable<Refferals>> GetRefferals();
        Task<Refferals> GetRefferal(int refferalID);
        Task<Refferals> AddRefferal(Refferals Refferals);
        Task<Refferals> UpdateRefferal(Refferals Refferals);
        Task DeleteRefferal(int refferalID);
    }
}
