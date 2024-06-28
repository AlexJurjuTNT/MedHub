using System.Reflection;
using Microsoft.OpenApi.Models;

namespace MedHub_Backend.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddAuthorization();
        services.AddControllers();

        services.AddSwaggerDoc();
        services.AddAutomapper();
        services.AddGlobalExceptionHandling();
        return services;
    }

    private static IServiceCollection AddAutomapper(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }

    private static IServiceCollection AddSwaggerDoc(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "MedHub-Backend.Api", Version = "1.0" });

            // rename methods in swagger doc to start with controller name
            options.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"]}");

            // add options for xml documentation for methods in controllers
            // var xmlFile = $"{Assembly.GetEntryAssembly()!.GetName().Name}.xml";
            // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            // options.IncludeXmlComments(xmlPath);

            // configure jwt token bearer authentication documentation for swagger ui
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Add JWT Token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });

            // possible bug - when using swagger ui with a different port number than the one noted below all requests will show CORS error
            options.AddServer(new OpenApiServer { Url = "http://localhost:5210", Description = "Local server" });
        });

        return services;
    }

    private static IServiceCollection AddGlobalExceptionHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        return services;
    }
}