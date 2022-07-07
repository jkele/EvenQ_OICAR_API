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

        public async Task<Refferal> AddRefferal(Refferal refferals)
        {

            if (refferals.Invitee != null)
            {
                appDbContext.Entry(refferals.Invitee).State = EntityState.Unchanged;
            }

            if (refferals.Inviter != null)
            {
                appDbContext.Entry(refferals.Inviter).State = EntityState.Unchanged;
            }

            if (appDbContext.Refferals.Where(r => r.Inviter.UID == refferals.InviterId).Count() > appDbContext.Members.First(m => m.UID == refferals.InviterId).NumberOfRefferals)
            {
                return null;

            }


            var result = await appDbContext.Refferals.AddAsync(refferals);
            await appDbContext.SaveChangesAsync();

            return result.Entity;
        }


        public async Task<Refferal> GetRefferal(int refferalID)
        {
            return await appDbContext.Refferals.Include(r => r.Inviter).Include(re => re.Invitee).FirstOrDefaultAsync(refe => refe.IDRefferal == refferalID);
        }

        public async Task<IEnumerable<Refferal>> GetRefferals()
        {
            return await appDbContext.Refferals.Include(r => r.Inviter).Include(re => re.Invitee).ToListAsync();
        }


        public async Task<Refferal> UpdateRefferal(Refferal refferals)
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
                results.Date = refferals.Date;

                await appDbContext.SaveChangesAsync();

                return results;
            }

            return null;
        }
    }
}
