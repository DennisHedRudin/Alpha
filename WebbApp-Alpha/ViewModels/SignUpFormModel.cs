using System.ComponentModel.DataAnnotations;

namespace WebbApp_Alpha.ViewModels
{
    public class SignUpFormModel
    {
        [Display(Name = "Full name", Prompt = "Your full name")]
        [Required(ErrorMessage = "You must enter your full name.")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Full name", Prompt = "Your full name")]
        [Required(ErrorMessage = "You must enter your full name.")]
        public string LastName { get; set; } = null!;

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

        [Required(ErrorMessage = "You must accept the terms.")]
        [Display(Name = "Terms & Conditions", Prompt = "I accept the terms & conditions.")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the terms and conditions.")]
        public bool TermsAndConditions { get; set; }

    }
}
