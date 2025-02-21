using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoelHiltonFilm.Models
{
    public class MovieForm
    {
        [Key]
        public int? MovieId { get; set; }
        
        [ForeignKey("CategoryId")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public string Title { get; set; } = "Unknown Title";
        [Range(1888, int.MaxValue, ErrorMessage = "Year must be 1888 or later.")]
        public int Year { get; set; } = 2000;
        public string? Director { get; set; }
        public string? Rating { get; set; }
        public bool Edited { get; set; } = false;
        public string? LentTo { get; set; }
        [Required]
        public bool CopiedToPlex { get; set; } = false;
        public string? Notes { get; set; }
    }
}
