using Azure.Storage.Blobs;
using Azure.Storage.Sas;

namespace MyMillenniumApi.Services
{
    public interface IBlobStorageService
    {
        Task UploadBlobAsync(Stream stream, string blobName, CancellationToken cancellationToken);
        Task DeleteBlobAsync(string blobName, CancellationToken cancellationToken);
        string GetBlobSasUrl(string blobName);
    }

    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;
        public BlobStorageService(BlobServiceClient blobServiceClient, IConfiguration config)
        {
            _blobServiceClient = blobServiceClient;
            _containerName = config["AzureStorage:ContainerName"] ?? throw new InvalidOperationException("Blob container name could not be found.");
        }

        public async Task UploadBlobAsync(Stream stream, string blobName, CancellationToken cancellationToken)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            await blobClient.UploadAsync(stream, overwrite: true, cancellationToken);
        }

        public async Task DeleteBlobAsync(string blobName, CancellationToken cancellationToken)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            await blobClient.DeleteAsync(cancellationToken: cancellationToken);
        }

        public string GetBlobSasUrl(string blobName)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            var sasUri = blobClient.GenerateSasUri(
                BlobSasPermissions.Read,
                DateTimeOffset.UtcNow.AddMinutes(30));

            return sasUri.ToString();
        }
    }
}
