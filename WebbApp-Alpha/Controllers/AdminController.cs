
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebbApp_Alpha.ViewModels.Projects;

namespace WebbApp_Alpha.Controllers;


[Authorize]
public class AdminController(IMemberService memberService, IProjectService projectService) : Controller
{
    private readonly IMemberService _memberService = memberService;
    private readonly IProjectService _projectService = projectService;

    public async Task<IActionResult> Projects()
    {
        var result = await _projectService.GetProjectsAsync();

        var viewModel = new ProjectsViewModel
        {
            Projects = result.Result,
            AddForm = new AddProjectForm(),
            EditForm = new EditProjectViewModel()
        };

        return View(viewModel);
    }

    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Members()
    {
        var members = await _memberService.GetMembersAsync();

        return View(members);
    }



}
