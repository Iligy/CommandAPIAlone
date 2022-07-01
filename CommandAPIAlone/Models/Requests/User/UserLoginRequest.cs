using System.ComponentModel.DataAnnotations;

namespace CommandAPIAlone.Models.Requests.User
{
    public class UserLoginRequest
    {
        [Required, MaxLength(100), EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
