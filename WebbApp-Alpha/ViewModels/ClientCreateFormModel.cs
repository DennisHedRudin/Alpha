using System.ComponentModel.DataAnnotations;

namespace WebbApp_Alpha.Models;

public class ClientCreateFormModel
{
    [Display(Name = "Client Name", Prompt = "Enter client name")]
    [Required(ErrorMessage = "Required")]
    public string ClientName { get; set; } = null!;

    [Display(Name = "Contact Person", Prompt = "Enter contact person")]
    [Required(ErrorMessage ="Required")]    
    public string ContactPerson { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Required")]
    [Display(Name = "Email", Prompt = "Enter email")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "You must enter an valid email adress")]
    public string Email { get; set; } = null!;

    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone", Prompt = "Enter phone number")]
    public string? Phone {  get; set; }

}
