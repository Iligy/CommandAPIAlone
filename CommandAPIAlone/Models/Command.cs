using System.ComponentModel.DataAnnotations;

namespace CommandAPIAlone.Models
{
    public class Command
    {
        [Key]
        [Required]
        public int Id { get; set; }

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
