namespace MedHub_Backend.Helper;

public class LocalStorageHelper
{
    private static string GetCurrentDirectory()
    {
        var result = Directory.GetCurrentDirectory();
        return result;
    }

    private static string GetUploadsDirectory()
    {
        var result = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\Documents");
        if (!Directory.Exists(result)) Directory.CreateDirectory(result);

        return result;
    }

    public static string GetClinicUserPath(string clinicName, string patientName)
    {
        var clinicPath = Path.Combine(GetUploadsDirectory(), clinicName);
        var userPath = Path.Combine(clinicPath, patientName);
        if (!Directory.Exists(userPath)) Directory.CreateDirectory(userPath);

        return userPath;
    }

    public static string GetUploadFilePath(string fileName)
    {
        var result = Path.Combine(GetUploadsDirectory(), fileName);
        return result;
    }
}