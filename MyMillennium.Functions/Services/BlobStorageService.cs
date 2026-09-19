using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMillennium.Functions.Services
{
    public class BlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public BlobStorageService(BlobServiceClient blobStorageService, IConfiguration config)
        {
            _blobServiceClient = blobStorageService;
            _containerName = config["AzureStorage:ContainerName"] ?? throw new InvalidOperationException("Azure Storage container name could not be found.");
        }

        public async Task<Stream> DownloadBlobAsync(string blobName)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            var response = await blobClient.DownloadStreamingAsync();

            return response.Value.Content;
        }
    }
}
