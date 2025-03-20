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

    [Route("/AllProjects")]
    public IActionResult AllProjects()
    {
        return View();
    }

    [Route("/StartedProjects")]
    public IActionResult StartedProjects()
    {
        return View();
    }

    [Route("/CompletedProjects")]
    public IActionResult CompletedProjects()
    {
        return View();
    }



}
