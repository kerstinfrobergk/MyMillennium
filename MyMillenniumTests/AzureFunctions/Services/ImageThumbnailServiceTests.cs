using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using MyMillennium.Functions.Services;
using SixLabors.ImageSharp;

namespace MyMillenniumTests.AzureFunctions.Services
{
    [TestFixture]
    public class ImageThumbnailServiceTests
    {
        private ImageThumbnailService _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new ImageThumbnailService();
        }

        // Test that return image is size 300x300 pixels
        [TestCase("road-to-cabin.jpg", 300, 300)]
        public void ResizeImageToThumbnailSize_ThumbnailHasSize300x300_ReturnTrue(
            string inputFileName,
            int expectedWidth,
            int expectedHeight)
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Ressources", inputFileName);

            using var fileStream = File.OpenRead(filePath);
            using var thumbnailStream = _uut.ResizeImageToThumbnailSize(fileStream);
            using var thumbnail = Image.Load(thumbnailStream);

            Assert.That(thumbnail.Size.Width, Is.EqualTo(expectedWidth));
            Assert.That(thumbnail.Size.Height, Is.EqualTo(expectedHeight));
        }
    }
}
