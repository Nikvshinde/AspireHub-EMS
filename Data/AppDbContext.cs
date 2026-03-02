using AspireHub_EMS.Model;
using Microsoft.EntityFrameworkCore;
using System.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<Leave> Leaves { get; set; }
    public DbSet<Salary> Salaries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>()
            .HasMany(d => d.Employees)
            .WithOne(e => e.Department)
            .HasForeignKey(e => e.DepartmentId);

        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Attendances)
            .WithOne(a => a.Employee)
            .HasForeignKey(a => a.EmployeeId);

        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Leaves)
            .WithOne(l => l.Employee)
            .HasForeignKey(l => l.EmployeeId);

        modelBuilder.Entity<Employee>()
            .HasMany(e => e.Salaries)
            .WithOne(s => s.Employee)
            .HasForeignKey(s => s.EmployeeId);
    }
}