namespace MyMillenniumApi.DTOs
{
    public class ArtItemDto
    {
        public int Id { get; set; }
        public required string ImageUrl { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ThumbnailUrl { get; set; }
    }
}
