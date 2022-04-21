using EvenQ_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenQ_API.Repo
{
    public interface IRefferals
    {
        Task<Refferals> GetRefferal(int refferalID);
        Task<Refferals> AddRefferal(Refferals Refferals);
        Task<Refferals> UpdateRefferal(Refferals Refferals);
    }
}
