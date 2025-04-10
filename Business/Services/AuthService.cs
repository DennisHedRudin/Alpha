using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Microsoft.AspNetCore.Identity;


namespace Business.Services;



public class AuthService(SignInManager<MemberEntity> signInManager, UserManager<MemberEntity> userManager) : IAuthService
{
    private readonly SignInManager<MemberEntity> _signInManager = signInManager;
    private readonly UserManager<MemberEntity> _userManager = userManager;

    public async Task<bool> LoginAsync(LoginModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
        return result.Succeeded;
    }


    public async Task<bool> SignUpAsync(MemberSignUpModel SignUpForm)
    {
        var memberEntity = MemberFactory.Create(SignUpForm);
        
        var result = await _userManager.CreateAsync(memberEntity, SignUpForm.Password);
        return result.Succeeded;
    }

    public async Task SignoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}



