using Azure.Storage.Blobs;
using Azure.Storage.Sas;

namespace MyMillenniumApi.Services
{
    public class BlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;
        public BlobStorageService(BlobServiceClient blobServiceClient, IConfiguration config)
        {
            _blobServiceClient = blobServiceClient;
            _containerName = config["AzureStorage:ContainerName"] ?? throw new InvalidOperationException("Azure Storage container name is not valid.");
        }

        public async Task UploadBlobAsync(Stream stream, string blobName)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            await blobClient.UploadAsync(stream, overwrite: true);
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
