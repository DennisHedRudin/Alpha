
namespace WebbApp_Alpha.ViewModels.Projects;

public class ProjectsViewModel
{
    public IEnumerable<ProjectViewModel> Projects { get; set; } = [];
    public AddProjectViewModel AddForm { get; set; } = new AddProjectViewModel();
    public EditProjectForm EditForm { get; set; } = new EditProjectForm();
}