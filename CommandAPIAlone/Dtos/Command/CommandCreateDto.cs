using System.ComponentModel.DataAnnotations;

namespace CommandAPIAlone.Dtos.Command
{
    public class CommandCreateDto
    {
        [Required]
        [MaxLength(250)]
        public string? HowTo { get; set; }

        [Required]
        [MaxLength(250)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Platform { get; set; }
    }
}
