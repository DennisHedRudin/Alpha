
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;


namespace Business.Services;



public class AuthService(SignInManager<MemberEntity> signInManager, UserManager<MemberEntity> userManager, IMemberService memberSerivce) : IAuthService
{
    private readonly SignInManager<MemberEntity> _signInManager = signInManager;
    private readonly UserManager<MemberEntity> _userManager = userManager;
    private readonly IMemberService _memberService = memberSerivce;

    public async Task<AuthResult> LoginAsync(LoginModelData model)
    {
        if (model == null)
            return new AuthResult { Success = false, StatusCode = 400, Error = "Not all required feilds are supplied." };

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.IsPersistent, false);
        return result.Succeeded
          ? new AuthResult { Success = true, StatusCode = 200 }
          : new AuthResult { Success = false, StatusCode = 401, Error = "Invalid emaill or password" };
    }
    


    public async Task<AuthResult> SignUpAsync(MemberSignUpModel formData)
    {
        if (formData == null)
            return new AuthResult { Success = false, StatusCode = 400, Error = "Not all required feilds are supplied." };

        var result = await _memberService.CreateMemberAsync(formData);

        if(!result.Success)
           return new AuthResult { Success = false, StatusCode = result.StatusCode, Error = "Unable create member." };          
           

        var user = await _userManager.FindByEmailAsync(formData.Email);
        if (user == null)
            return new AuthResult { Success = false, StatusCode = 500, Error = "User creation succeeded but user not found." };

        
        await _signInManager.SignInAsync(user, isPersistent: false);

        return new AuthResult { Success = true, StatusCode = 201 };
    }

    public async Task<AuthResult> SignoutAsync()
    {
        await _signInManager.SignOutAsync();
        return new AuthResult { Success = true, StatusCode = 200 };
    }
}



