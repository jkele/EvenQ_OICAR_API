using EvenQ_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Firebase.Auth;
using FirebaseAdmin.Auth;

namespace EvenQ_API.Repo
{
    public class MemberRepo : IMemberRepo
    {

        private readonly AppDbContext appDbContext;

        public MemberRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        private static Random random = new Random();

        public FirebaseAuthProvider auth;

        AbstractFirebaseAuth afa;


        public MemberRepo()
        {
            auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyB6-iD9rlVsAQfOZKsmDBVPBlpEFpGrBa0"));
        }
        public async Task DeleteMember(string UID)
        {
            var results = await appDbContext.Members.FirstOrDefaultAsync(m => m.UID == UID);

            if (results != null)
            {
                string randomized = RandomString(10);
                results.FirstName = randomized;
                results.LastName = randomized;

                await afa.DeleteUserAsync(UID);
                await UpdateMember(results);
                await appDbContext.SaveChangesAsync();
            }
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<Member> AddMember(Member member)
        {
            var result = await appDbContext.Members.AddAsync(member);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }


        public async Task<Member> GetMember(string UID)
        {
            return await appDbContext.Members.FirstOrDefaultAsync(m => m.UID == UID);
        }


        public async Task<IEnumerable<Member>> GetMembers()
        {
            return await appDbContext.Members.ToListAsync();
        }

        public async Task<bool> IsInviteValid(string RefferalCode)
        {
            var results = appDbContext.Members.Where(r => r.RefferalCode == RefferalCode);

            if (results != null)
            {
                if (results.First().NumberOfRefferals > 0)
                {
                    var sub = results.First();
                    sub.NumberOfRefferals = sub.NumberOfRefferals - 1;
                    await appDbContext.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }



        public async Task<Member> IsAdmin(string UID)
        {
            return await appDbContext.Members.Where(m => m.IsAdmin == true).FirstOrDefaultAsync(m => m.UID == UID);
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

        public async Task<Member> UpdateMemberAdmin(Member member)
        {
            var results = await appDbContext.Members.FirstOrDefaultAsync(m => m.UID == member.UID);

            if (results != null)
            {
                results.FirstName = member.FirstName;
                results.LastName = member.LastName;

                await appDbContext.SaveChangesAsync();

                return results;
            }

            return null;
        }
    }
}
