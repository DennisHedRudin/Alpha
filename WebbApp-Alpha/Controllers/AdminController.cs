
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebbApp_Alpha.Controllers;


[Authorize]
public class AdminController : Controller
{

    public IActionResult Projects()
    {
        return View();
    }

    //[Authorize(Roles = "admin")]
    public IActionResult Members()
    {
        return View();
    }

    //[Authorize(Roles = "admin")]
    public IActionResult Clients()
    {
        return View();
    }

  
}
