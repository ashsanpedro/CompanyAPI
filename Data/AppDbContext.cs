using Microsoft.EntityFrameworkCore;
using dotnet_crud_api.Models;

namespace dotnet_crud_api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Position> Positions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
        modelBuilder.Entity<Department>().Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
        modelBuilder.Entity<Position>().Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
    }
}