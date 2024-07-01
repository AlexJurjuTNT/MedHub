using Microsoft.AspNetCore.Http;

namespace Medhub_Backend.Application.Abstractions.Service;

public interface IFileService
{
    Task<string> UploadFile(IFormFile file, string clinicName, string patientName);
    Task<(byte[] Content, string ContentType, string FileName)> DownloadFile(string fileName);
}