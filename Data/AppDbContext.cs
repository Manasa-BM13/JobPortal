using EmployeeTaskManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTaskManagementAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<JobPost> Jobs => Set<JobPost>();
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<JobApplication>()
            .HasOne(a => a.Job)
            .WithMany()
            .HasForeignKey(a => a.JobId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
