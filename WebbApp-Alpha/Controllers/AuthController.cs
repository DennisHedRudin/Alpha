using Microsoft.AspNetCore.Mvc;

namespace WebbApp_Alpha.Controllers;

public class AuthController : Controller
{
    public IActionResult Register()
    {
        return View();
    }

    public IActionResult Login()
    {
        //return LocalRedirect("/projects");
        return View();
    }
}
