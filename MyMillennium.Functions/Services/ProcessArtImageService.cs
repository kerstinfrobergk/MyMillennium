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

            // TODO: Retrieve the blob represented by processArtImage.BlobName
            var originalBlob = await _blobStorageService.DownloadBlobAsync(processArtImage.BlobName);

            if(originalBlob == null)
            {
                return;
            }

            // TODO: Generate thumbnail
        }
    }
}
