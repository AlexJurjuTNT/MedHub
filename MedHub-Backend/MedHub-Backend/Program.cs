using System.Reflection;
using MedHub_Backend.Data;
using MedHub_Backend.Service;
using MedHub_Backend.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

// add services to DI container
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IClinicService, ClinicService>();

// add swagger documentation
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "MedHub-Backend", Version = "1.0" });

    // rename methods in swagger doc to start with controller name
    options.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"]}");

    // add options for xml documentation for methods in controllers
    var xmlFile = $"{Assembly.GetEntryAssembly()!.GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

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

// connect to db
var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnectionString")!;
builder.Services.AddDbContext<AppDbContext>(
    options => options
        .UseNpgsql(connectionString)
        .UseLazyLoadingProxies()
);

// add automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// allow cors for frontend application
app.UseCors(c =>
{
    c.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();