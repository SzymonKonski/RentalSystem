using Rental.Infrastructure.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Rental.Infrastructure.Services
{
    public class UploadService : IUploadService
    {
        private readonly string storageConnectionString;
        public UploadService(IConfiguration configuration)
        {
            storageConnectionString = configuration.GetConnectionString("AzureStorage");
        }

        public async Task<string> UploadPdfAsync(Stream fileStream, string contentType)
        {
            return await UploadAsync(fileStream, "file-pdf-container", contentType);
        }

        public async Task<string> UploadImageAsync(Stream fileStream, string contentType)
        {

            return await  UploadAsync(fileStream, "file-image-container", contentType);
        }

        private async Task<string> UploadAsync(Stream fileStream, string blobContainerName, string contentType)
        {
            var container = new BlobContainerClient(storageConnectionString, blobContainerName);
            var createResponse = await container.CreateIfNotExistsAsync();
            if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                await container.SetAccessPolicyAsync(PublicAccessType.Blob);

            bool exists;
            BlobClient blob;
            do
            {
                var blobName = Guid.NewGuid().ToString();
                blob = container.GetBlobClient(blobName);
                exists = await blob.ExistsAsync();

            } while (exists);

            await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contentType });
            return blob.Uri.ToString();
        }
    }
}
