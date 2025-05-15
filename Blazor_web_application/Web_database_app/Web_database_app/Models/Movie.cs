using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_database_app.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? RelaseDate { get; set; }

        public int VoteCount { get; set; } = 0;
        public float TotalScore { get; set; } = 0;
        [NotMapped]
        public float? Rate => VoteCount == 0 ? null : TotalScore / VoteCount;

        public string? ImageUrl { get; set; }

        public string? ImageFileName { get; set; } 

        [NotMapped]
        public IFormFile? UploadedImage { get; set; } 

    }
}
