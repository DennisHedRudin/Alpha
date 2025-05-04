using Domain.Models;

namespace WebbApp_Alpha.ViewModels.Projects;

public class ProjectViewModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProjectImage { get; set; } = null!;
    public string ProjectName { get; set; } = null!;
    public Client Client { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string TimeLeft { get; set; } = null!;
    public IEnumerable<string> Members { get; set; } = [];

    public EditProjectViewModel EditForm { get; set; } = new();
}
