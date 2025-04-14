using Business.Models;
using Domain.Models;


namespace Business.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(LoginModelData model);
        Task<AuthResult> SignoutAsync();
        Task<AuthResult> SignUpAsync(MemberSignUpModel SignUpForm);
    }
}