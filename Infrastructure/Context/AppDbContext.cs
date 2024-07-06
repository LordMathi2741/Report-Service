using Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get;  }
    public DbSet<Report> Reports { get;  }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            throw new Exception("No connection string configured.");
        }
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.AddInterceptors();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Report>().ToTable("reports");
        modelBuilder.Entity<Report>().HasKey(r => r.Id);
        modelBuilder.Entity<Report>().Property(r => r.Id).HasColumnName("id").ValueGeneratedOnAdd();
        modelBuilder.Entity<Report>().Property(r => r.Brand).HasColumnName("brand").IsRequired();
        modelBuilder.Entity<Report>().Property(r => r.Type).HasColumnName("type").IsRequired();
        modelBuilder.Entity<Report>().Property(r => r.CertificationNumber).HasColumnName("certification_number").IsRequired();
        modelBuilder.Entity<Report>().Property(r => r.CylinderNumber).HasColumnName("cylinder_number").IsRequired();
        modelBuilder.Entity<Report>().Property(r => r.MadeDate).HasColumnName("made_date").IsRequired();
        modelBuilder.Entity<Report>().Property(r => r.EmitDate).HasColumnName("emit_date").IsRequired();
        modelBuilder.Entity<Report>().Property(r => r.VehicleIdentifier).HasColumnName("vehicle_identifier").IsRequired();
        modelBuilder.Entity<Report>().Property(r => r.OperationCenter).HasColumnName("operation_center").IsRequired();
        modelBuilder.Entity<Report>().Property(r => r.CreatedDate).HasColumnName("created_date").IsRequired();
        modelBuilder.Entity<Report>().Property(r => r.UpdatedDate).HasColumnName("updated_date");
        
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<User>().Property(u => u.Id).HasColumnName("id").ValueGeneratedOnAdd();
        modelBuilder.Entity<User>().Property(u => u.Username).HasColumnName("username").IsRequired();
        modelBuilder.Entity<User>().Property(u => u.Password).HasColumnName("password").IsRequired();
        modelBuilder.Entity<User>().Property(u => u.Email).HasColumnName("email").IsRequired();
        modelBuilder.Entity<User>().Property(u => u.CreatedDate).HasColumnName("created_date").IsRequired();
        modelBuilder.Entity<User>().Property(u => u.UpdatedDate).HasColumnName("updated_date");
        
        modelBuilder.Entity<Report>().HasOne(r => r.User).WithMany(u => u.Reports).HasForeignKey(r => r.UserId);
    }
}