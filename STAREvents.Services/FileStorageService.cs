using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using STAREvents.Services.Data.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace STAREvents.Services.Data
{
    public class FileStorageService : IFileStorageService
    {
        private readonly string connectionString;
        private readonly BlobServiceClient blobServiceClient;

        public FileStorageService(IConfiguration configuration)
        {
            var azureBlobSettings = configuration.GetSection("AzureBlobStorage");
            connectionString = azureBlobSettings["ConnectionString"];
            blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> UploadFileAsync(IFormFile file, string containerName)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

            var uniqueFileName = Guid.NewGuid().ToString() + "-" + file.FileName;
            var blobClient = containerClient.GetBlobClient(uniqueFileName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
            }

            return blobClient.Uri.ToString(); 
        }

        public async Task<string> UploadFileLocallyAsync(IFormFile file, string folderPath)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderPath);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder); 
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream); 
            }

            return $"/{folderPath.Replace("\\", "/")}/{uniqueFileName}";
        }
    }
}
