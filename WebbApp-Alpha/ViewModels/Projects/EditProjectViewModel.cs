using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebbApp_Alpha.ViewModels.Projects;

public class EditProjectViewModel
{
    public IEnumerable<SelectListItem> Clients { get; set; } = [];
    public IEnumerable<SelectListItem> Members { get; set; } = [];
    public IEnumerable<SelectListItem> Statuses { get; set; } = [];
}
