using Microsoft.AspNetCore.Mvc;

namespace WebbApp_Alpha.Controllers;


public class ProjectsController : Controller
{
    
    

    
    public IActionResult AllProjects()
    {
        return View();
    }

    
    public IActionResult StartedProjects()
    {
        return View();
    }

    
    public IActionResult CompletedProjects()
    {
        return View();
    }



}
