using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebbApp_Alpha.ViewModels.Projects;

public class AddProjectViewModel
{
    [Display(Name = "Project Image", Prompt = "Select an image")]
    [DataType(DataType.Upload)]
    public IFormFile? ProjectImage { get; set; }

    [Required(ErrorMessage = "Project name is required")]
    [Display(Name = "Project Name", Prompt = "Project Name")]
    public string ProjectName { get; set; } = null!;

    [Display(Name = "Client Name", Prompt = "Client Name")]
    [Required(ErrorMessage = "You must select a client")]
    public string ClientId { get; set; } = null!;

    [Display(Name = "Description", Prompt = "Type something")]
    [DataType(DataType.MultilineText)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Start date is required")]
    [Display(Name = "Start Date")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Display(Name = "End Date")]
    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }

    [Display(Name = "Project Members", Prompt = "Select members")]
    [Required(ErrorMessage = "You must select a member")]
    public string MemberId { get; set; } = null!;


    [Display(Name = "Budget", Prompt = "0")]
    [Range(0, double.MaxValue, ErrorMessage = "Budget must be a positive number")]
    public decimal? Budget { get; set; }

    public IEnumerable<SelectListItem> Clients { get; set; } = [];
    public IEnumerable<SelectListItem> Members { get; set;} = [];
}
