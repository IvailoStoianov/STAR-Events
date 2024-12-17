using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using STAREvents.Services.Data;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace STAREvents.Services.Tests
{
    [TestFixture]
    public class FileStorageServiceTests
    {
        private Mock<IConfiguration> _configurationMock;
        private Mock<BlobServiceClient> _blobServiceClientMock;
        private Mock<BlobContainerClient> _blobContainerClientMock;
        private Mock<BlobClient> _blobClientMock;
        private FileStorageService _fileStorageService;

        [SetUp]
        public void SetUp()
        {
            _configurationMock = new Mock<IConfiguration>();
            _blobServiceClientMock = new Mock<BlobServiceClient>();
            _blobContainerClientMock = new Mock<BlobContainerClient>();
            _blobClientMock = new Mock<BlobClient>();

            var azureBlobSectionMock = new Mock<IConfigurationSection>();
            azureBlobSectionMock.Setup(s => s["ConnectionString"]).Returns("UseDevelopmentStorage=true;");
            _configurationMock.Setup(c => c.GetSection("AzureBlobStorage")).Returns(azureBlobSectionMock.Object);

            _blobServiceClientMock.Setup(bs => bs.GetBlobContainerClient(It.IsAny<string>()))
                .Returns(_blobContainerClientMock.Object);

            _blobContainerClientMock.Setup(bc => bc.GetBlobClient(It.IsAny<string>()))
                .Returns(_blobClientMock.Object);

            _fileStorageService = new FileStorageService(_configurationMock.Object);
        }


        
        [Test]
        public async Task UploadFileLocallyAsync_ShouldSaveFileToLocalFolder()
        {
            var mockFile = new Mock<IFormFile>();
            var fileName = "test.jpg";
            var folderPath = "images/test-folder";

            mockFile.Setup(f => f.FileName).Returns(fileName);
            mockFile.Setup(f => f.OpenReadStream()).Returns(new MemoryStream(new byte[] { 1, 2, 3 }));

            var localFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderPath);
            Directory.CreateDirectory(localFolderPath);

            var result = await _fileStorageService.UploadFileLocallyAsync(mockFile.Object, folderPath);

            Assert.That(result.StartsWith($"/{folderPath.Replace("\\", "/")}/"), Is.True);

            var savedFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", result.TrimStart('/').Replace("/", "\\"));
            Assert.That(File.Exists(savedFilePath), Is.True);

            File.Delete(savedFilePath);
        }


        [Test]
        public void Constructor_ShouldThrowException_WhenConnectionStringIsMissing()
        {
            var invalidConfigurationMock = new Mock<IConfiguration>();
            var azureBlobSectionMock = new Mock<IConfigurationSection>();
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            azureBlobSectionMock.Setup(s => s["ConnectionString"]).Returns<string?>(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            invalidConfigurationMock.Setup(c => c.GetSection("AzureBlobStorage")).Returns(azureBlobSectionMock.Object);

            Assert.Throws<ArgumentNullException>(() => new FileStorageService(invalidConfigurationMock.Object));
        }
    }
}
