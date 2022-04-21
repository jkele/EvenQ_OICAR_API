using EvenQ_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EvenQ_API.Repo
{
    public class MemberRepo : IMemberRepo
    {

        private readonly AppDbContext appDbContext;

        public MemberRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Member> AddMember(Member member)
        {
            var result = await appDbContext.Members.AddAsync(member);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteMember(string UID)
        {
            var results = await appDbContext.Members.FirstOrDefaultAsync(m => m.UID == UID);

            if (results != null)
            {
                appDbContext.Members.Remove(results);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Member> GetMember(string UID)
        {
            return await appDbContext.Members.FirstOrDefaultAsync(m => m.UID == UID);
        }


        public async Task<IEnumerable<Member>> GetMembers()
        {
            return await appDbContext.Members.ToListAsync();
        }

        public async Task<IEnumerable<Member>> SearchMember(string name)
        {
            IQueryable<Member> query = appDbContext.Members;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(m => m.FirstName.Contains(name) || m.LastName.Contains(name));
            }

            return await query.ToListAsync();

        }


        public async Task<Member> UpdateMember(Member member)
        {
            var results = await appDbContext.Members.FirstOrDefaultAsync(m => m.UID == member.UID);

            if (results != null)
            {
                results.FirstName = member.FirstName;
                results.LastName = member.LastName;
                results.RefferalCode = member.RefferalCode;
                results.IsAdmin = member.IsAdmin;
                if (member.NumberOfRefferals != 0)
                {
                    results.NumberOfRefferals = member.NumberOfRefferals;
                }
                results.MembershipValid = member.MembershipValid;

                await appDbContext.SaveChangesAsync();

                return results;
            }

            return null;
        }
    }
}
