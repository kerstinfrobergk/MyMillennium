using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace MyMillennium.Functions.Services
{
    public class BlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public BlobStorageService(BlobServiceClient blobStorageService, IConfiguration config)
        {
            _blobServiceClient = blobStorageService;
            _containerName = config["BlobContainerName"]
                ?? throw new InvalidOperationException("Blob container name could not be found.");
        }

        public async Task<Stream> DownloadBlobAsync(string blobName)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            var response = await blobClient.DownloadStreamingAsync();

            return response.Value.Content;
        }

        public async Task UploadBlobAsync(Stream stream, string blobName)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            await blobClient.UploadAsync(stream, overwrite: true);
        }

    }
}
