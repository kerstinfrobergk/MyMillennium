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
        private readonly AppDbContext _dbContext;
        private readonly IBlobStorageService _blobStorageService;
        private readonly IImageThumbnailService _imageThumbnailService;

        public ProcessArtImageService(AppDbContext dbContext, IBlobStorageService blobStorageService, IImageThumbnailService imageThumbnailService)
        {
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
                //TODO: throw an exception instead so message don't complete
                return;
            }

            using var originalBlob = await _blobStorageService
                .DownloadBlobAsync(processArtImage.BlobName, cancellationToken);

            if (originalBlob == null)
            {
                //TODO: throw an exception instead so message don't complete
                return;
            }

            using var thumbnail = _imageThumbnailService
                .ResizeImageToThumbnailSize(originalBlob);

            var thumbnailBlobName = $"thumbnail/{Guid.NewGuid()}.jpg";
            
            await _blobStorageService
                .UploadBlobAsync(thumbnail, thumbnailBlobName, cancellationToken);

            artItem.ThumbnailBlobName = thumbnailBlobName;
            artItem.ProcessingStatus = ProcessingStatus.Completed;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
