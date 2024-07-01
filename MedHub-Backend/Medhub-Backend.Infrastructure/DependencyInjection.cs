using Medhub_Backend.Application.Abstractions.Persistence;
using Medhub_Backend.Infrastructure.Persistence;
using Medhub_Backend.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Medhub_Backend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
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

        services.AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IClinicRepository, ClinicRepository>();
        services.AddScoped<ILaboratoryRepository, LaboratoryRepository>();
        services.AddScoped<IPatientInformationRepository, PatientInformationRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ITestRequestRepository, TestRequestRepository>();
        services.AddScoped<ITestResultRepository, TestResultRepository>();
        services.AddScoped<ITestTypeRepository, TestTypeRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}