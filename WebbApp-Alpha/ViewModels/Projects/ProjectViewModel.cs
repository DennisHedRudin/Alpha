using Domain.Models;

namespace WebbApp_Alpha.ViewModels.Projects;

public class ProjectsViewModel
{
    public IEnumerable<Project> Projects { get; set; } = new List<Project>();
    public AddProjectForm AddForm { get; set; } = new AddProjectForm();
    public EditProjectViewModel EditForm { get; set; } = new EditProjectViewModel();
}