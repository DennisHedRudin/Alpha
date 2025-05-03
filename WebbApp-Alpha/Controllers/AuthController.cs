using System.Security.Claims;

using Business.Interfaces;
using Data.Entities;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebbApp_Alpha.ViewModels;
using WebbApp_Alpha.ViewModels.Members;


namespace WebbApp_Alpha.Controllers;

public class AuthController(IAuthService authService, SignInManager<MemberEntity> signInManager, UserManager<MemberEntity> userManager) : Controller
{
    private readonly IAuthService _authService = authService;
    private readonly SignInManager<MemberEntity> _signInManager = signInManager;
    private readonly UserManager<MemberEntity> _userManager = userManager;

    #region Internal
    public IActionResult Register()
    {

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(SignUpFormModel model)
    {
        if (!ModelState.IsValid)
            return View(model);
        
        var signUpFormData = model.MapTo<MemberSignUpModel>();
        var result = await _authService.SignUpAsync(signUpFormData);

        if (result.Success)
        {

            return RedirectToAction(nameof(Login));
        }

        
        ViewBag.ErrorMessage = result.Error;
        return View(model);
    }


    public IActionResult Login(string returnUrl = "~/")
    {
        ViewBag.ReturnUrl = returnUrl;


        return View();
    }

    [HttpPost]

    public async Task<IActionResult> Login(MemberLoginForm form, string returnUrl = "~/")
    {
        ViewBag.ErrorMessage = null;
        ViewBag.ReturnUrl = returnUrl;

        if (!ModelState.IsValid)
            return View(form);


        var signInFormData = form.MapTo<LoginModelData>();
        var result = await _authService.LoginAsync(signInFormData);
        if (result.Success)
        {
            return RedirectToAction("Index", "Projects");
        }

        ViewBag.ErrorMessage = result.Error;
        return View(form);

    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Signout()
    {
        await _authService.SignoutAsync();
        return RedirectToAction("Login", "Auth");
    }

    #endregion

    #region External

    [HttpPost]
    public IActionResult ExternalSignIn(string provider, string returnUrl = null!)
    {
        if (string.IsNullOrEmpty(provider))
        {
            ModelState.AddModelError("", "invalid provider");
            return View("Login");
        }

        var redirectUrl = Url.Action("ExternalSignInCallback", "Auth", new { returnUrl })!;
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return Challenge(properties, provider);
    }

    public async Task<IActionResult> ExternalSignInCallback(string returnUrl = null!, string remoteError = null!)
    {
        returnUrl = Url.Content("~/");

        if (!string.IsNullOrEmpty(remoteError))
        {
            ModelState.AddModelError("", $"Error from external provider: {remoteError}");
            return View("Login");
        }

        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
            return RedirectToAction("Login");

        var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        if (signInResult.Succeeded)
        {
            return LocalRedirect(returnUrl);
        }
        else
        {
            string firstName = string.Empty;
            string lastName = string.Empty;

            try
            {
                firstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!;
                lastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!;

            }
            catch { }


            string email = info.Principal.FindFirstValue(ClaimTypes.Email)!;
            string username = $"ext_{info.LoginProvider.ToLower()}_{email}";

            var member = new MemberEntity { UserName = email, FirstName = firstName, LastName = lastName, Email = email };

            var identityResult = await _userManager.CreateAsync(member);
            if (identityResult.Succeeded)
            {
                await _userManager.AddLoginAsync(member, info);
                await _signInManager.SignInAsync(member, isPersistent: false);
                return LocalRedirect(returnUrl);

            }

            foreach(var error in identityResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View("Login");
        }


    }

    #endregion
}
