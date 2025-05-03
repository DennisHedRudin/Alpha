
namespace WebbApp_Alpha.ViewModels.Projects;

public class ProjectsViewModel
{
    public IEnumerable<ProjectViewModel> Projects { get; set; } = Enumerable.Empty<ProjectViewModel>();
    public AddProjectViewModel AddForm { get; set; } = new AddProjectViewModel();
    public EditProjectViewModel EditForm { get; set; } = new EditProjectViewModel();
}