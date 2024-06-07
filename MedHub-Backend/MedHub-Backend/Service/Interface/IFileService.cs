namespace MedHub_Backend.Service.Interface;

public interface IFileService
{
    Task<string> UploadFile(IFormFile file);
    Task<string> UploadFile(IFormFile file, string uploadPath);
    Task<(byte[], string, string)> DownloadFile(string fileName);
}