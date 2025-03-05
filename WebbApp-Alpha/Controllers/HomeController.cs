using Microsoft.AspNetCore.Mvc;

namespace WebbApp_Alpha.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Home";
        return View();
    }
}
