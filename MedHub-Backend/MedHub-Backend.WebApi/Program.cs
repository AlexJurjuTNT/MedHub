using Medhub_Backend.Application;
using Medhub_Backend.Infrastructure;
using Medhub_Backend.Infrastructure.Persistence;
using MedHub_Backend.WebApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPersistence(builder.Configuration);
    builder.Services.AddApplication(builder.Configuration);
    builder.Services.AddWebApi();
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
        context.SeedDbAsync().Wait();
    }

    app.Run();
}