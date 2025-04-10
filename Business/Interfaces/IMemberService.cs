using Business.Models;

namespace Business.Interfaces
{
    public interface IMemberService
    {
        Task<MemberResult> GetMembersAsync();
    }
}