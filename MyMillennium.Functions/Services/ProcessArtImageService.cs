using Microsoft.EntityFrameworkCore;
using MyMillennium.Contracts.Messages;
using MyMillennium.Data.DataAccess;

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
                .Where(x => x.Id == processArtImage.ArtItemId)
                .FirstOrDefaultAsync();

            if (artItem == null)
            {
                return;
            }

            var originalBlob = await _blobStorageService.DownloadBlobAsync(processArtImage.BlobName);

            if(originalBlob == null)
            {
                return;
            }

            var thumbnail = _imageThumbnailService.ResizeImageToThumbnailSize(originalBlob);

            var thumbnailBlobName = $"thumbnail/{Guid.NewGuid()}";
            
            await _blobStorageService.UploadBlobAsync(thumbnail, thumbnailBlobName);

            // TODO: Update ArtItem with ProcessingStatus and thumbnail
        }
    }
}
