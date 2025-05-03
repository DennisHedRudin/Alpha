using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebbApp_Alpha.ViewModels.Projects;

public class EditProjectForm
{
    public string Id { get; set; } = null!;

    [Display(Name = "Project Image", Prompt = "Select an image")]
    [DataType(DataType.Upload)]
    public IFormFile? ProjectImage { get; set; }

    public string? ExistingImageUrl { get; set; }

    [Display(Name = "Project Name", Prompt = "Enter project name")]
    [Required(ErrorMessage = "Required")]
    [DataType(DataType.Text)]
    public string ProjectName { get; set; } = null!;

    [Display(Name = "Client", Prompt = "Select a client")]
    [Required(ErrorMessage = "Required")]
    public string ClientId { get; set; } = null!;

    [Display(Name = "Description", Prompt = "Enter description")]
    [DataType(DataType.MultilineText)]
    public string? Description { get; set; }

    [Display(Name = "Start Date")]
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Required")]
    public DateTime StartDate { get; set; }

    [Display(Name = "End Date")]
    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }

    [Display(Name = "Team Member")]
    [Required(ErrorMessage = "Required")]
    public string MemberId { get; set; } = null!;

    [Display(Name = "Budget", Prompt = "Enter project budget")]
    [Range(0, double.MaxValue, ErrorMessage = "Enter a valid number")]
    public decimal? Budget { get; set; }

    [Display(Name = "Status")]
    [Required(ErrorMessage = "Required")]
    public int StatusId { get; set; }

    public IEnumerable<SelectListItem> Clients { get; set; } = [];
    public IEnumerable<SelectListItem> Members { get; set; } = [];

    public IEnumerable<SelectListItem> Statuses { get; set; } = [];
}