using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace MyMillennium.Functions.Services
{
    public class ImageThumbnailService
    {
        public Stream ResizeImageToThumbnailSize(Stream originalBlob)
        {
            using var image = Image.Load(originalBlob);

            using var thumbnail = image.Clone(x => x.AutoOrient().Resize(new ResizeOptions
            {
                Size = new Size(300, 300),
                Mode = ResizeMode.Crop,
                Sampler = KnownResamplers.Bicubic
            }));

            var thumbnailStream = new MemoryStream();

            thumbnail.SaveAsJpeg(thumbnailStream);

            thumbnailStream.Position = 0;

            return thumbnailStream;
        }
    }
}
