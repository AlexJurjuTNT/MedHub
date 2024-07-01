using Medhub_Backend.Application.Abstractions.Service;
using Medhub_Backend.Application.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;

namespace Medhub_Backend.Application.Service;

public class LocalFileService : IFileService
{
    public async Task<string> UploadFile(IFormFile file, string clinicName, string patientName)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("Invalid file", nameof(file));
        }

        try
        {
            var fileName = GenerateUniqueFileName(file.FileName);
            var uploadPath = LocalStorageHelper.GetClinicUserPath(clinicName, patientName);
            var fullPath = Path.Combine(uploadPath, fileName);

            using var fileStream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            return fullPath;
        }
        catch (Exception ex)
        {
            throw new IOException($"File upload failed: {ex.Message}", ex);
        }
    }

    public async Task<(byte[] Content, string ContentType, string FileName)> DownloadFile(string fileName)
    {
        try
        {
            var filePath = LocalStorageHelper.GetUploadFilePath(fileName);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found", fileName);
            }

            var contentType = GetContentType(filePath);
            var bytes = await File.ReadAllBytesAsync(filePath);

            return (bytes, contentType, Path.GetFileName(filePath));
        }
        catch (Exception ex)
        {
            throw new IOException($"File download failed: {ex.Message}", ex);
        }
    }

    private string GenerateUniqueFileName(string originalFileName)
    {
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(originalFileName);
        var fileExtension = Path.GetExtension(originalFileName);
        return $"{fileNameWithoutExtension}_{DateTime.Now:yyyyMMddHHmmssfff}{fileExtension}";
    }

    private string GetContentType(string filePath)
    {
        var provider = new FileExtensionContentTypeProvider();
        return provider.TryGetContentType(filePath, out var contentType)
            ? contentType
            : "application/octet-stream";
    }
}