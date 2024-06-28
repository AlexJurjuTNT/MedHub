using Microsoft.AspNetCore.Http;

namespace Medhub_Backend.Application.Service.Interface;

public interface IFileService
{
    Task<string> UploadFile(IFormFile file, string uploadPath);
    Task<(byte[], string, string)> DownloadFile(string fileName);
}