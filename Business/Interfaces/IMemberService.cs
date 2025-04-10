using Business.Models.Members;
using Domain.Models;

namespace Business.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetAllMembers();
    }
}