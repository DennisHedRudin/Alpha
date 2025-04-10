using Business.Models;


namespace Business.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginModel model);
        Task SignoutAsync();
        Task<bool> SignUpAsync(MemberSignUpModel SignUpForm);
    }
}