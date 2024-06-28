﻿using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Util;
using Medhub_Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Medhub_Backend.Persistence.Persistence;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public AppDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Clinic> Clinics { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<TestRequest> TestRequests { get; set; }
    public DbSet<TestResult> TestResults { get; set; }
    public DbSet<TestType> TestTypes { get; set; }
    public DbSet<Laboratory> Laboratories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var encryptionKey = _configuration["ColumnEncryptionKey"];
        modelBuilder.UseEncryption(new GenerateEncryptionProvider(encryptionKey));

        modelBuilder.Entity<TestRequest>()
            .HasMany(tr => tr.TestTypes)
            .WithMany(tt => tt.TestRequests);
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

        if (!TestTypes.Any())
        {
            TestTypes.AddRange(
                new TestType { Name = "Blood" },
                new TestType { Name = "EKG" },
                new TestType { Name = "Sugar" }
            );
            await SaveChangesAsync();
        }
    }
}