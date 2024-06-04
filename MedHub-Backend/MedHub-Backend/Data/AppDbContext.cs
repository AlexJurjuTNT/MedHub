using MedHub_Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace MedHub_Backend.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Clinic> Clinics { get; set; }
    public DbSet<Role> Roles { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}