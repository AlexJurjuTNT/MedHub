using Medhub_Backend.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Medhub_Backend.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        // from npsql 6 only DateTimeUtc is supported, added this line to revert behaviour, add time as local time
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        // connect to db
        var connectionString = configuration["DatabaseSettings:PostgreSQLConnectionString"];
        services.AddDbContext<AppDbContext>(
            options => options
                .UseNpgsql(connectionString)
                .UseLazyLoadingProxies()
        );

        return services;
    }
}