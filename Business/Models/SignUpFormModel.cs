using System.ComponentModel.DataAnnotations;

namespace WebbApp_Alpha.Models
{
    public class SignUpFormModel
    {
        [Display(Name = "Last name", Prompt = "Your full name")]
        [Required(ErrorMessage = "You must enter your first name.")]
        public string FullName { get; set; } = null!;

        [Display(Name = "Email", Prompt = "Your email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "You must enter your email.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "You must enter an valid email adress")]
        public string Email { get; set; } = null!;

        [Display(Name = "Password", Prompt = "Enter your password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "You must enter your password.")]
        [RegularExpression(@"^(?=.*[a-ö])(?=.*[A-Ö])(?=.*\d)(?=.*[\W_]).{8,}$", ErrorMessage = "You must enter a strong password")]
        public string Password { get; set; } = null!;

        [Display(Name = "Confirm Password", Prompt = "Confirm your password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "You must confirm your password.")]
        [Compare(nameof(Password), ErrorMessage = "Your password do not match!")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
