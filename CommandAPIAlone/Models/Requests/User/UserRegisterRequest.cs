using System.ComponentModel.DataAnnotations;

namespace CommandAPIAlone.Models.Requests.User
{
    public class UserRegisterRequest
    {
        [Required, MaxLength(100), EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(6, ErrorMessage = "Password at least must be 6 characters")]
        public string Password { get; set; } = string.Empty;

        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
