using Business.Models;
using Domain.Models;

namespace Business.Interfaces
{
    public interface IMemberService
    {
        Task<MemberResult> GetMembersAsync();
        Task<MemberResult> AddMemberToRole(string userId, string roleName);
        Task<MemberResult> CreateMemberAsync(MemberSignUpModel form, string roleName = "Member");
    }
}