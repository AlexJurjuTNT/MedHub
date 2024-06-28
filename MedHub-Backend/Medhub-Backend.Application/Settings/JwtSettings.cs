namespace Medhub_Backend.Application.Settings;

public class JwtSettings
{
    public static string SectionName { get; } = "JwtSettings";
    public string Secret { get; set; } = null!;
    public int ExpiryInMinutes { get; set; }
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
}