using Microsoft.Extensions.Logging;
using MyMillennium.Contracts.Messages;
using MyMillennium.Data.DataAccess;
using MyMillennium.Data.Entities;

namespace MyMillennium.Functions.Services
{
    public interface IProcessArtImageService
    {
        Task ProcessArtItemAsync(ProcessArtImage processArtImage, CancellationToken cancellationToken);
    }
    public class ProcessArtImageService : IProcessArtImageService
    {
        private readonly ILogger<ProcessArtImageService> _logger;
        private readonly AppDbContext _dbContext;
        private readonly IBlobStorageService _blobStorageService;
        private readonly IImageThumbnailService _imageThumbnailService;

        public ProcessArtImageService(ILogger<ProcessArtImageService> logger, AppDbContext dbContext, IBlobStorageService blobStorageService, IImageThumbnailService imageThumbnailService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _blobStorageService = blobStorageService;
            _imageThumbnailService = imageThumbnailService;
        }

        public async Task ProcessArtItemAsync(ProcessArtImage processArtImage, CancellationToken cancellationToken)
        {
            var artItem = await _dbContext.ArtItems
                .FindAsync(processArtImage.ArtItemId, cancellationToken);

            if (artItem == null)
            {
                _logger.LogWarning("Could not find ArtItem with ID: {artItemId}. Service Bus message not completed.",
                    processArtImage.ArtItemId);

                throw new InvalidOperationException($"Could not find ArtItem with ID: {processArtImage.ArtItemId}.");
            }

            using var originalBlob = await _blobStorageService
                .DownloadBlobAsync(processArtImage.BlobName, cancellationToken);

            using var thumbnail = _imageThumbnailService.ResizeImageToThumbnailSize(originalBlob);

            var thumbnailBlobName = $"thumbnail/{Guid.NewGuid()}.jpg";
            
            await _blobStorageService.UploadBlobAsync(thumbnail, thumbnailBlobName, cancellationToken);

            artItem.ThumbnailBlobName = thumbnailBlobName;
            artItem.ProcessingStatus = ProcessingStatus.Completed;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
