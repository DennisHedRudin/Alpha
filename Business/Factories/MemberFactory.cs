using Data.Entities;
using WebbApp_Alpha.Models;

namespace Business.Factories;

public class MemberFactory
{
    
    public static MemberEntity Create(SignUpFormModel signupForm)
    {
        return new MemberEntity
        {
            UserName = signupForm.Email,
            FirstName = signupForm.FullName,
            Email = signupForm.Email,           

        };
    }
    
}
