using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebbApp_Alpha.ViewModels.Clients;

public class EditClientForm
{

    public int Id { get; set; }

    [Display(Name = "Client Image", Prompt = "Select an image")]
    [DataType(DataType.Upload)]
    public IFormFile? ClientImage { get; set; }


    [Display(Name = "Client Name", Prompt = "Enter client name")]
    [Required(ErrorMessage = "Required")]
    [DataType(DataType.Text)]
    public string ClientName { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Required")]
    [Display(Name = "Email", Prompt = "Enter email")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "You must enter an valid email adress")]
    public string Email { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "Location", Prompt = "Enter location")]
    public string? Location { get; set; }

    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone", Prompt = "Enter phonenumber")]
    public string? Phone { get; set; }
}
