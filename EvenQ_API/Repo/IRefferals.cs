using EvenQ_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenQ_API.Repo
{
    public interface IRefferals
    {
        Task<Refferal> GetRefferal(int refferalID);
        Task<Refferal> AddRefferal(Refferal Refferals);
        Task<Refferal> UpdateRefferal(Refferal Refferals);
    }
}
