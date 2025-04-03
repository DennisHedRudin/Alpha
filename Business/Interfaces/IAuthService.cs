using Business.Models;
using Business.Models.Members;


namespace Business.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginModel model);
        Task SignoutAsync();
        Task<bool> SignUpAsync(MemberSignUpModel SignUpForm);
    }
}