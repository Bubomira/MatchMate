
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MatchMateCore.Dtos.AuthenticationViewModels
{
    public class LoginModel
    {
        public IList<AuthenticationScheme> ExternalLogins { get; set; } = new List<AuthenticationScheme>();
        public string? ReturnUrl { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
