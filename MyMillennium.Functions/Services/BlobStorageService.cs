using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace MyMillennium.Functions.Services
{
    public interface IBlobStorageService
    {
        Task<Stream> DownloadBlobAsync(string blobName, CancellationToken cancellationToken);
        Task UploadBlobAsync(Stream stream, string blobName, CancellationToken cancellationToken);
    }

    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public BlobStorageService(BlobServiceClient blobStorageService, IConfiguration config)
        {
            _blobServiceClient = blobStorageService;
            _containerName = config["BlobContainerName"]
                ?? throw new InvalidOperationException("Blob container name could not be found.");
        }

        public async Task<Stream> DownloadBlobAsync(string blobName, CancellationToken cancellationToken)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            var response = await blobClient
                .DownloadStreamingAsync(cancellationToken: cancellationToken);

            return response.Value.Content;
        }

        public async Task UploadBlobAsync(Stream stream, string blobName, CancellationToken cancellationToken)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            await blobClient
                .UploadAsync(stream, overwrite: true, cancellationToken);
        }
    }
}
