using EvenQ_API.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenQ_API.Repo
{
    public class RefferalsRepo : IRefferals
    {

        private readonly AppDbContext appDbContext;

        public RefferalsRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Refferals> AddRefferal(Refferals refferals, Member member)
        {

            if (refferals.Invitee != null)
            {
                appDbContext.Entry(refferals.Invitee).State = EntityState.Unchanged;
            }

            if (refferals.Inviter != null)
            {
                appDbContext.Entry(refferals.Inviter).State = EntityState.Unchanged;
            }

            var result = await appDbContext.Refferals.AddAsync(refferals);
            if (Int32.Parse(refferals.InviterId) <= member.NumberOfRefferals && Int32.Parse(refferals.InviteeId) <= member.NumberOfRefferals)
                await appDbContext.SaveChangesAsync();
  
            return result.Entity;
        }


        public async Task<Refferals> GetRefferal(int refferalID)
        {
            return await appDbContext.Refferals.Include(r => r.Inviter).Include(re => re.Invitee).FirstOrDefaultAsync(refe => refe.IDRefferal == refferalID);
        }

        public async Task<IEnumerable<Refferals>> GetRefferals()
        {
            return await appDbContext.Refferals.ToListAsync();
        }


        public async Task<Refferals> UpdateRefferal(Refferals refferals)
        {
            var results = await appDbContext.Refferals.FirstOrDefaultAsync(r => r.IDRefferal == refferals.IDRefferal);

            if (results != null)
            {
                if (refferals.Inviter != null)
                {
                    results.InviterId = refferals.Inviter.UID;
                }
                results.InviterId = refferals.InviterId;

                if (refferals.Invitee != null)
                {
                    results.InviteeId = refferals.Invitee.UID;
                }
                results.InviteeId = refferals.InviteeId;
                results.date = refferals.date;

                await appDbContext.SaveChangesAsync();

                return results;
            }

            return null;
        }
    }
}
