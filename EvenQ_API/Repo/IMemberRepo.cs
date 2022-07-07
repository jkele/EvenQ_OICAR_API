using EvenQ_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenQ_API.Repo
{
    public interface IMemberRepo
    {
        Task<IEnumerable<Member>> SearchMember(string name);
        Task<IEnumerable<Member>> GetMembers();
        Task<Member> GetMember(string UID);

        Task<Member> IsAdmin(string UID);
        Task<Member> AddMember(Member Member);
        Task<Member> UpdateMember(Member Member);

        Task<Member> UpdateMemberAdmin(Member Member);
        public Task<bool> IsInviteValid(string RefferalCode);
        Task DeleteMember(string UID);
    }
}
