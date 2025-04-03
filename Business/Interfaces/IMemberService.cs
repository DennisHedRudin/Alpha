using Business.Models.Members;

namespace Business.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAllMembers();
    }
}