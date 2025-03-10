using Microsoft.AspNetCore.Mvc;

namespace WebbApp_Alpha.Controllers;

[Route("projects")]
public class ProjectsController : Controller
{
    [Route("")]
    public IActionResult Projects()
    {
        return View();
    }

  
}
