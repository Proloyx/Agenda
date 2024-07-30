using System;
using System.Collections.Generic;
using Agenda.Models.DashboardModels;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Schedule> Schedules { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Workcenter> Workcenters { get; set; }
    public DbSet<MonthlySalary> MonthlySalaries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Scheduleid).HasName("schedules_pkey");

            entity.ToTable("schedules");

            entity.HasIndex(e => new { e.Centerid, e.Workdate, e.Starttime }, "unique_schedule").IsUnique();

            entity.Property(e => e.Scheduleid).HasColumnName("scheduleid");
            entity.Property(e => e.Centerid).HasColumnName("centerid");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .HasColumnName("description");
            entity.Property(e => e.Endtime).HasColumnName("endtime");
            entity.Property(e => e.Starttime).HasColumnName("starttime");
            entity.Property(e => e.Workdate).HasColumnName("workdate");
            entity.Property(e => e.Workedhours).HasColumnName("workedhours")
                .HasColumnType("decimal(10, 2)");;

            entity.HasOne(d => d.Center).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.Centerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("schedules_centerid_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Workcenter>(entity =>
        {
            entity.HasKey(e => e.Centerid).HasName("workcenters_pkey");

            entity.ToTable("workcenters");

            entity.Property(e => e.Centerid).HasColumnName("centerid");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("address");
            entity.Property(e => e.Grossrate)
                .HasPrecision(10, 2)
                .HasColumnName("grossrate");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Netrate)
                .HasPrecision(10, 2)
                .HasColumnName("netrate");
            entity.Property(w => w.IsActive)
                .HasColumnName("isactive")
                .HasDefaultValue(true);
            entity.Property(e => e.Paymentday).HasColumnName("paymentday");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Workcenters)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("workcenters_userid_fkey");
        });

        modelBuilder.Entity<MonthlySalary>().HasNoKey();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
