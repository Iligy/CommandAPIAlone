using System.ComponentModel.DataAnnotations;

namespace CommandAPIAlone.Models
{
    public class PracticeModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string? JokeQuestion { get; set; }

        [Required]
        [MaxLength(100)]
        public string? JokeAnswer { get; set; }
    }
}
