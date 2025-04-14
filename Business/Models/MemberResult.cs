using Domain.Models;

namespace Business.Models;

public class MemberResult : ServiceResult
{
    public IEnumerable<Member>? Member { get; set; }
}



