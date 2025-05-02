using Business.Interfaces;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebbApp_Alpha.ViewModels.Projects;

namespace WebbApp_Alpha.Controllers;


public class ProjectsController(IProjectService projectService) : Controller
{
    private readonly IProjectService _projectService = projectService;

    

    [HttpPost]
    public async Task<IActionResult> AddProject(AddProjectViewModel model)
    {
        var addProjectFormData = model.MapTo<AddProjectFormData>(); 

        var result = await _projectService.CreateProjectAsync(addProjectFormData);

        return View();
    }

    [HttpPost]
    public IActionResult UpdateProject(EditProjectForm model)
    {
        var editProjectFormData = model.MapTo<EditProjectForm>();

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
