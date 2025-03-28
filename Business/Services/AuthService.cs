using Business.Factories;
using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using WebbApp_Alpha.Models;

namespace Business.Services;

public interface IAuthService
{
    Task<bool> LoginAsync(MemberLoginForm loginForm);
    Task<bool> SignUpAsync(SignUpFormModel SignUpForm);
}

public class AuthService(SignInManager<MemberEntity> signInManager, UserManager<MemberEntity> userManager) : IAuthService
{
    private readonly SignInManager<MemberEntity> _signInManager = signInManager;
    private readonly UserManager<MemberEntity> _userManager = userManager;

    public async Task<bool> LoginAsync(MemberLoginForm loginForm)
    {
        var result = await _signInManager.PasswordSignInAsync(loginForm.Email, loginForm.Password, false, false);
        return result.Succeeded;
    }


    public async Task<bool> SignUpAsync(SignUpFormModel SignUpForm)
    {
        var memberEntity = MemberFactory.Create(SignUpForm);
        
        var result = await _userManager.CreateAsync(memberEntity, SignUpForm.Password);
        return result.Succeeded;
    }
}



