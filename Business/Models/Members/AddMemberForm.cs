using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Business.Models.Members;

public class AddMemberForm
{
    [Display(Name = "Member Image", Prompt = "Select an image")]
    [DataType(DataType.Upload)]
    public IFormFile? MemberImage { get; set; }


    [Display(Name = "Member Name", Prompt = "Enter member name")]
    [Required(ErrorMessage = "Required")]
    [DataType(DataType.Text)]
    public string MemberName { get; set; } = null!;

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
