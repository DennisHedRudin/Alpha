using Business.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebbApp_Alpha.Controllers;


public class ProjectsController(IProjectService projectService) : Controller
{
    private readonly IProjectService _projectService = projectService;

    public async Task<IActionResult> AllProjects()
    {

        var model = new ProjectsViewModel
        {
            ProjectsController = await _projectService.GetProjectsAsync(),
        };

        return View(model);
    }

    [HttpPost]
    public async IActionResult AddProject(AddProjectViewModel model)
    {
        var addProjectFormData = model.MapTo<AddProjectFormData> 

        var result = await _projectService.CreateProjectAsync(model);
        return View();
    }

    [HttpPost]
    public IActionResult UpdateProject(EditProjectViewModel model)
    {
        return View();
    }

    [HttpPost]
    public IActionResult DeleteProject(string id)
    {
        return Json(new {});
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
