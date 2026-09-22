using MyMillennium.Contracts.Messages;
using MyMillennium.Data.DataAccess;
using MyMillennium.Data.Entities;

namespace MyMillennium.Functions.Services
{
    public class ProcessArtImageService
    {
        private readonly AppDbContext _dbContext;
        private readonly BlobStorageService _blobStorageService;
        private readonly ImageThumbnailService _imageThumbnailService;

        public ProcessArtImageService(AppDbContext dbContext, BlobStorageService blobStorageService, ImageThumbnailService imageThumbnailService)
        {
            _dbContext = dbContext;
            _blobStorageService = blobStorageService;
            _imageThumbnailService = imageThumbnailService;
        }

        public async Task ProcessArtItemAsync(ProcessArtImage processArtImage)
        {
            var artItem = await _dbContext.ArtItems
                .FindAsync(processArtImage.ArtItemId);

            if (artItem == null)
            {
                //TODO: throw an exception instead so message don't complete
                return;
            }

            using var originalBlob = await _blobStorageService
                .DownloadBlobAsync(processArtImage.BlobName);

            if(originalBlob == null)
            {
                //TODO: throw an exception instead so message don't complete
                return;
            }

            using var thumbnail = _imageThumbnailService
                .ResizeImageToThumbnailSize(originalBlob);

            var thumbnailBlobName = $"thumbnail/{Guid.NewGuid()}.jpg";
            
            await _blobStorageService
                .UploadBlobAsync(thumbnail, thumbnailBlobName);

            artItem.ThumbnailBlobName = thumbnailBlobName;
            artItem.ProcessingStatus = ProcessingStatus.Completed;

            await _dbContext.SaveChangesAsync();
        }
    }
}
