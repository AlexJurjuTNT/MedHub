using Medhub_Backend.Application;
using Medhub_Backend.Persistence;
using Medhub_Backend.Persistence.Persistence;
using MedHub_Backend.WebApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddAuthorization();
    builder.Services.AddControllers();

    builder.Services.AddPersistence(builder.Configuration);
    builder.Services.AddBusiness(builder.Configuration);
    builder.Services.AddConfiguration();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors(c =>
    {
        c.AllowAnyOrigin()
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
        context.Database.Migrate();
        context.SeedRolesAsync().Wait();
    }

    app.Run();
}