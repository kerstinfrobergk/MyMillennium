using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace MyMillennium.Functions.Services
{
    public class ImageThumbnailService
    {

        public Stream ResizeImageToThumbnailSize(Stream originalBlob)
        {
            var image = Image.Load(originalBlob);

            image.Mutate(x => x.AutoOrient().Resize(new ResizeOptions
            {
                Size = new Size(300, 300),
                Mode = ResizeMode.Crop,
                PadColor = Color.White,
                Sampler = KnownResamplers.Bicubic
            }));

            // TODO: Encode resized image/thumbnail into a new Stream

            // TODO: Returning Stream
        }
    }
}
