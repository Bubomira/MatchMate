using MatchMateCore.Attributes;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;


namespace MatchMateCore.Dtos.AuthenticationViewModels
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
        public IList<AuthenticationScheme> ExternalLogins { get; set; } = new List<AuthenticationScheme>();

        [IsAtLeastSixteen]
        public string Birthday { get; set; } = string.Empty;

        public string? ReturnUrl { get; set; }
    }
}
