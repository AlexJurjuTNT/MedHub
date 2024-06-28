using Medhub_Backend.Application.Abstractions.Service;
using Medhub_Backend.Application.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;

namespace Medhub_Backend.Application.Service;

public class LocalFileService : IFileService
{
    public async Task<string> UploadFile(IFormFile file, string uploadPath)
    {
        string fullPath;
        try
        {
            var fileInfo = new FileInfo(file.FileName);
            var fileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + DateTime.Now.Ticks + fileInfo.Extension;
            var filePath = Path.Combine(uploadPath, fileName);
            fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("File upload failed: " + ex.Message);
        }

        return fullPath;
    }

    public async Task<(byte[], string, string)> DownloadFile(string fileName)
    {
        try
        {
            var filePath = LocalStorageHelper.GetUploadFilePath(fileName);
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType)) contentType = "application/octet-stream";

            var bytes = await File.ReadAllBytesAsync(filePath);
            return (bytes, contentType, Path.GetFileName(filePath));
        }

        catch (Exception ex)
        {
            throw new Exception("File download failed: " + ex.Message);
        }
    }
}