using Business.Interfaces;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebbApp_Alpha.ViewModels;


namespace WebbApp_Alpha.Controllers;

public class AuthController(IAuthService authService) : Controller
{
    private readonly IAuthService _authService = authService;

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
            return LocalRedirect("~/");                 

      

        ViewBag.ErrorMessage = result.Error;
        return View(model);

    }

    
    public IActionResult Login(string returnUrl = "~/")
    {
        ViewBag.ReturnUrl = returnUrl;


        return View();
    }

    [HttpPost]

    public async Task<IActionResult> Login(LoginModelData form, string returnUrl = "~/")
    {
        ViewBag.ErrorMessage = null;
        ViewBag.ReturnUrl = returnUrl;

        if (!ModelState.IsValid)
            return View(form);


        var signInFormData = form.MapTo<LoginModelData>();
        var result = await _authService.LoginAsync(signInFormData);
        if (result.Success)
        {
            return LocalRedirect(returnUrl);
        }

        ViewBag.ErrorMessage = result.Error;
        return View(form);

    }




    public async Task<IActionResult> Signout()
    {
        await _authService.SignoutAsync();
        return LocalRedirect("~/");
    }
}
