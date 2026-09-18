namespace MyMillenniumApi.DTOs
{
    public class ArtItemDto
    {
        public required int Id { get; set; }
        public required string ImageUrl { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
