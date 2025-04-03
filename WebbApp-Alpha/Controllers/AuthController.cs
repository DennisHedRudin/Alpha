using Business.Interfaces;
using Business.Models;
using Business.Models.Members;
using Microsoft.AspNetCore.Mvc;
using WebbApp_Alpha.ViewModels;
using WebbApp_Alpha.ViewModels.Members;

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

    public async Task<IActionResult> Register(SignUpFormModel model)
    {
        if (ModelState.IsValid)
        {
            var member = new MemberSignUpModel
            {
               FirstName = model.FirstName,
               LastName = model.LastName,
               Email = model.Email,
               Password = model.Password,

            };

            var result = await _authService.SignUpAsync(member);
            if (result)
                return LocalRedirect("~/");

        }

        ViewBag.ErrorMessage = "Incorrect email or password.";
        return View(model);
    }

    
    public IActionResult Login(string returnUrl = "~/")
    {
        ViewBag.ErrorMessage = "";
        ViewBag.ReturnUrl = returnUrl;

        return View();
    }

    [HttpPost]

    public async Task<IActionResult> Login(MemberLoginForm dto, string returnUrl = "~/")
    {
        ViewBag.ErrorMessage = "";
        

        if (ModelState.IsValid)
        {
            var loginModel = new LoginModel
            {
                Email = dto.Email,
                Password = dto.Password,
            };

            var result = await _authService.LoginAsync(loginModel);
            if (result)
                return LocalRedirect(returnUrl);

        }

        ViewBag.ErrorMessage = "Incorrect email or password.";
        return View(dto);


    }




    public async Task<IActionResult> Signout()
    {
        await _authService.SignoutAsync();
        return LocalRedirect("~/");
    }
}
