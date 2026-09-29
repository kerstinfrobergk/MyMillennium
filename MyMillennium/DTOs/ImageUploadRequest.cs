using MyMillennium.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyMillenniumApi.DTOs
{
    public class ImageUploadRequest
    {
        [Required]
        public required IFormFile File { get; set; }
        [Required]
        public required string Title { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        public required Category ItemCategory { get; set; }
    }
}
