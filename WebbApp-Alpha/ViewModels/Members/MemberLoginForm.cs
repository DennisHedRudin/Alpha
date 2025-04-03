using System.ComponentModel.DataAnnotations;

namespace WebbApp_Alpha.ViewModels.Members;

public class MemberLoginForm
{
    [Required]
    [Display(Name = "Email", Prompt = "Enter email adress")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required]
    [Display(Name = "Password", Prompt = "Enter password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
