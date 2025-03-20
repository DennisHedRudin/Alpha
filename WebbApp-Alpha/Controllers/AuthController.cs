using Microsoft.AspNetCore.Mvc;
using WebbApp_Alpha.Models;

namespace WebbApp_Alpha.Controllers;

public class AuthController : Controller
{
    [Route("Register")]
    public IActionResult Register()
    {
        var formData = new SignUpFormModel();
        return View(formData);
    }

    [HttpPost]

    public IActionResult Register(SignUpFormModel formData)
    {
        if (!ModelState.IsValid)
            return View(formData);

        return View();
    }

    [Route("Login")]
    public IActionResult Login()
    {
        
        return View();
    }

    public new IActionResult SignOut()
    {
        return View();
    }
}
