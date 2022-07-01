using System.ComponentModel.DataAnnotations;

namespace CommandAPIAlone.Models.Requests.EmailService
{
    public class EmailRequest
    {
        [Required, EmailAddress, MaxLength(100)]
        public string To { get; set; } = string.Empty;

        [Required, MaxLength(70)]
        public string Subject { get; set; } = string.Empty;

        [Required]
        public string Body { get; set; } = string.Empty;
    }
}
