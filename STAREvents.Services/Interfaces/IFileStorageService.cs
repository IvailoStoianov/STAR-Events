using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAREvents.Services.Data.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> UploadFileAsync(IFormFile file, string containerName);
        Task<string> UploadFileLocallyAsync(IFormFile file, string folderPath);
    }
}
