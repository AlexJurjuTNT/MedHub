using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using MedHub_Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace MedHub_Backend.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Clinic> Clinics { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<TestRequest> TestRequests { get; set; }
    public DbSet<TestResult> TestResults { get; set; }
    public DbSet<TestType> TestTypes { get; set; }

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseEncryption(new GenerateEncryptionProvider("1234567890123456"));

        // create the many-to-many link between testType and testRequest
        // modelBuilder.Entity<TestRequest>()
        //     .HasMany(tr => tr.TestTypes)
        //     .WithMany(tt => tt.TestRequests);
    }

    public async Task SeedRolesAsync()
    {
        if (!Roles.Any())
        {
            Roles.AddRange(
                new Role { Name = "Patient" },
                new Role { Name = "Doctor" },
                new Role { Name = "Admin" }
            );
            await SaveChangesAsync();
        }
    }
}