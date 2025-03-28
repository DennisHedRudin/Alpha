using Business.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebbApp_Alpha.Models;

namespace WebbApp_Alpha.Controllers;

public class AuthController(IAuthService authService) : Controller
{
    private readonly IAuthService _authService = authService;

    public IActionResult Register()
    {
        ViewBag.ErrorMessage = null;

        var formData = new SignUpFormModel();
        return View(formData);
    }

    [HttpPost]

    public async Task<IActionResult> Register(SignUpFormModel formData)
    {
        if (ModelState.IsValid)
        {
            var result = await _authService.SignUpAsync(formData);
            if (result)
                return LocalRedirect("~/");

        }

        ViewBag.ErrorMessage = "Incorrect email or password.";
        return View(formData);
    }

    
    public IActionResult Login()
    {
        ViewBag.ErrorMessage = "";

        return View();
    }

    [HttpPost]

    public async Task<IActionResult> Login(MemberLoginForm form, string returnUrl = "~/")
    {
        ViewBag.ErrorMessage = "";

        if (ModelState.IsValid)
        {
            var result = await _authService.LoginAsync(form);
            if (result)
                return Redirect(returnUrl);

        }

        ViewBag.ErrorMessage = "Incorrect email or password.";
        return View(form);


    }




    public new IActionResult SignOut()
    {
        return View();
    }
}
