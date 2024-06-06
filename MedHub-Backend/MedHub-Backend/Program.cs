using System.Reflection;
using System.Text;
using MedHub_Backend.Data;
using MedHub_Backend.Service;
using MedHub_Backend.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

// add services to DI container
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IClinicService, ClinicService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ITestRequestService, TestRequestService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<ITestTypeService, TestTypeService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<ITestResultService, TestResultService>();
builder.Services.AddScoped<IFileService, LocalFileService>();

// add automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


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
var connectionString = builder.Configuration["DatabaseSettings:PostgreSQLConnectionString"];
builder.Services.AddDbContext<AppDbContext>(
    options => options
        .UseNpgsql(connectionString)
        .UseLazyLoadingProxies()
);

// add authentication using jwt bearer token
var jwtSecretKey = builder.Configuration["JwtSettings:SECRET_KEY"];
var issuer = builder.Configuration["JwtSettings:ISSUER"];
var audience = builder.Configuration["JwtSettings:AUDIENCE"];

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

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

// seed roles at startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate(); // apply any pending migrations
    context.SeedRolesAsync().Wait(); // seed roles
}

app.Run();