namespace MyMillenniumApi.Data
{
    public class ArtItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Category ItemCategory { get; set; } = Category.Inspiration;
        public string BlobName { get; set; } = string.Empty;
    }

    public enum Category
    {
        Inspiration = 0,
        ProfilePicture = 1,
    }
}
