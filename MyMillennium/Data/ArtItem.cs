namespace MyMillenniumApi.Data
{
    public class ArtItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Category ItemCategory { get; set; } = Category.Inspiration;
        public string BlobName { get; set; } = string.Empty;
        public ProcessingStatus ProcessingStatus { get; set; }
    }

    public enum Category
    {
        //TODO: Consider what categories make sense
        Inspiration = 0,
        ProfilePicture = 1,
    }

    public enum ProcessingStatus
    {
        Pending = 0,
        Completed = 1,
        Failed = 2
    }
}
