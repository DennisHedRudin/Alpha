using Business.Interfaces;
using Business.Services;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebbApp_Alpha.ViewModels.Projects;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebbApp_Alpha.Controllers;

[Authorize]
public class ProjectsController(IProjectService projectService, IClientService clientService, IStatusService statusService, IMemberService memberService) : Controller
{
    private readonly IProjectService _projectService = projectService;
    private readonly IClientService _clientService = clientService;
    private readonly IStatusService _statusService = statusService;
    private readonly IMemberService _memberService = memberService;

    public async Task<IActionResult> Index()
    {
        
        var clientsDto = (await _clientService.GetClientsAsync()).Result ?? Enumerable.Empty<Client>();
        var clientItems = clientsDto
            .Select(c => new SelectListItem(c.ClientName, c.Id))
            .ToList();

        var membersDto = (await _memberService.GetMembersAsync()).Result ?? Enumerable.Empty<Member>();
        var memberItems = membersDto
            .Select(m => new SelectListItem($"{m.FirstName} {m.LastName}", m.Id))
            .ToList();

        var statusesDto = (await _statusService.GetStatusesAsync()).Result ?? Enumerable.Empty<Status>();
        var statusItems = statusesDto
            .Select(s => new SelectListItem(s.StatusName, s.Id.ToString()))
            .ToList();

        
        var projects = (await _projectService.GetProjectsAsync()).Result
            ?.Where(p => p.Member != null && p.Client != null && p.Status != null)
            .ToList()
            ?? [];

        
        var projectlist = projects.Select(p => new ProjectViewModel
        {
            Id = p.Id,
            ProjectImage = p.Image ?? "/images/project/project-template.svg",
            ProjectName = p.ProjectName,
            Description = p.Description ?? "",
            Client = p.Client,
            TimeLeft = p.EndDate.HasValue
                ? $"{(p.EndDate.Value - DateTime.Now).Days} days left"
                : "No end date",
            Members = [$"{p.Member.FirstName} {p.Member.LastName}"],

            EditForm = new EditProjectViewModel
            {
                Form = new EditProjectForm
                {
                    Id = p.Id,
                    ExistingImageUrl = p.Image,
                    ProjectName = p.ProjectName,
                    Description = p.Description,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Budget = p.Budget,
                    ClientId = p.Client.Id,
                    MemberId = p.Member.Id,
                    StatusId = p.Status.Id
                },
                Clients = clientItems,
                Members = memberItems,
                Statuses = statusItems
            }
        }).ToList();

       
        var vm = new ProjectsViewModel
        {
            Projects = projectlist,
            AddForm = new AddProjectViewModel
            {
                Clients = clientItems,
                Members = memberItems
            },
            
        };

        return View(vm);
    }



    public async Task<IActionResult> Started()
    {
        var dtoList = (await _projectService.GetProjectsAsync()).Result
                      ?? Enumerable.Empty<Project>();

        var vmList = dtoList
            .Where(p => p.EndDate.HasValue && p.EndDate.Value > DateTime.Now)
            .Select(p => p.MapTo<ProjectViewModel>())
            .ToList();

        var vm = new ProjectsViewModel
        {
            Projects = vmList,
            AddForm = new AddProjectViewModel(),
            EditForm = new EditProjectViewModel()
        };


        return View("Index", vm);
    }


    public async Task<IActionResult> Completed()
    {
        var dtoList = (await _projectService.GetProjectsAsync()).Result
                      ?? Enumerable.Empty<Project>();

        var vmList = dtoList
            .Where(p => p.EndDate.HasValue && p.EndDate.Value <= DateTime.Now)
            .Select(p => p.MapTo<ProjectViewModel>())
            .ToList();

        var vm = new ProjectsViewModel
        {
            Projects = vmList,
            AddForm = new AddProjectViewModel(),
            EditForm = new EditProjectViewModel()
        };

        return View("Index", vm);
    }


    [HttpPost]
    public async Task<IActionResult> AddProject(AddProjectViewModel form)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                );

            return BadRequest(new { success = false, errors });
        }

        var dto = form.MapTo<AddProjectFormData>();
        var result = await _projectService.CreateProjectAsync(dto);

        if (result.Success)
            return Ok(new { success = true });
        else
            return Problem(result.Error ?? "Could not create project.");
    }



    [HttpPost]
    public async Task<IActionResult> UpdateProject(EditProjectForm form)
    {
        if (!ModelState.IsValid)
        {
            
            var clients = (await _clientService.GetClientsAsync()).Result!
                          .Select(c => new SelectListItem(c.ClientName, c.Id));

            var members = (await _memberService.GetMembersAsync()).Result!
                          .Select(m => new SelectListItem($"{m.FirstName} {m.LastName}", m.Id));

            var statuses = (await _statusService.GetStatusesAsync()).Result!
                           .Select(s => new SelectListItem(s.StatusName, s.Id.ToString()));

            var editForm = new EditProjectViewModel
            {
                Form = form,
                Clients = clients,
                Members = members,
                Statuses = statuses
            };

            
            var projectsResult = await _projectService.GetProjectsAsync();
            var clientItems = clients.ToList();
            var memberItems = members.ToList();
            var statusItems = statuses.ToList();

            var projectlist = projectsResult.Result!
                .Select(p => new ProjectViewModel
                {
                    Id = p.Id,
                    ProjectImage = p.Image ?? "/images/project/project-template.svg",
                    ProjectName = p.ProjectName,
                    Description = p.Description ?? "",
                    Client = p.Client,
                    TimeLeft = p.EndDate.HasValue
                        ? $"{(p.EndDate.Value - DateTime.Now).Days} days left"
                        : "No end date",
                    Members = [$"{p.Member.FirstName} {p.Member.LastName}"],

                    EditForm = p.Id == form.Id 
                        ? editForm
                        : new EditProjectViewModel
                        {
                            Form = new EditProjectForm { Id = p.Id },
                            Clients = clientItems,
                            Members = memberItems,
                            Statuses = statusItems
                        }
                })
                .ToList();

            var vm = new ProjectsViewModel
            {
                Projects = projectlist,
                AddForm = new AddProjectViewModel
                {
                    Clients = clientItems,
                    Members = memberItems
                },
                EditForm = editForm 
            };

            
            ViewData["OpenEditModalId"] = form.Id;

            return View("Index", vm);
        }

        var dto = form.MapTo<EditProjectFormData>();
        var result = await _projectService.UpdateProjectAsync(dto);

        if (result.Success)
            return RedirectToAction("Index");

        return Problem(result.Error ?? "Could not update project.");
    }


    [HttpPost]
    public async Task<IActionResult> DeleteProject(string id)
    {
        if (string.IsNullOrEmpty(id))
            return BadRequest(new { success = false, error = "Missing project id." });

        var result = await _projectService.DeleteProjectAsync(id);
        if (result.Success)

            return RedirectToAction("Index");
        else
            return Problem("Unable to delete project.");
    }
}




