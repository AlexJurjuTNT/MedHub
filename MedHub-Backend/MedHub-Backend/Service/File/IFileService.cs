namespace MedHub_Backend.Service.File;

public interface IFileService
{
    Task<string> UploadFile(IFormFile file, string uploadPath);
    Task<(byte[], string, string)> DownloadFile(string fileName);
}