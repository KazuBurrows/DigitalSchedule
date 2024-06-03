using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CDHB_Official.sakila;

public partial class DbAa1c85CdhbContext : DbContext
{
    public DbAa1c85CdhbContext()
    {
    }

    public DbAa1c85CdhbContext(DbContextOptions<DbAa1c85CdhbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<Migrationhistory> Migrationhistories { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Staff> Staffs { get; set; }

    public virtual DbSet<Subspecialty> Subspecialties { get; set; }

    public virtual DbSet<Theatre> Theatres { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseLazyLoadingProxies().UseMySQL("Server=MYSQL5048.site4now.net;Database=db_aa1c85_cdhb;Uid=aa1c85_cdhb;Pwd=Rockstar03");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__efmigrationshistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Migrationhistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__migrationhistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ContextKey).HasMaxLength(300);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("schedule");

            entity.HasIndex(e => e.SessionId, "FK_Session");

            entity.HasIndex(e => e.TheatreId, "FK_Theatre");

            entity.Property(e => e.Day).HasMaxLength(255);
            entity.Property(e => e.IsAm).HasColumnName("IsAM");
            entity.Property(e => e.SessionId).HasColumnName("Session_Id");
            entity.Property(e => e.TheatreId).HasColumnName("Theatre_Id");
            entity.Property(e => e.TimeEnd)
                .HasColumnType("time")
                .HasColumnName("Time_End");
            entity.Property(e => e.TimeStart)
                .HasColumnType("time")
                .HasColumnName("Time_Start");

            entity.HasOne(d => d.Session).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Session");

            entity.HasOne(d => d.Theatre).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.TheatreId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Theatre");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("session");

            entity.HasIndex(e => e.StaffId, "FK_Staff");

            entity.HasIndex(e => e.SubspecialtyId, "FK_Subspecialty");

            entity.Property(e => e.AnaestheticType)
                .HasMaxLength(255)
                .HasColumnName("Anaesthetic_Type");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.StaffId).HasColumnName("Staff_Id");
            entity.Property(e => e.SubspecialtyId).HasColumnName("Subspecialty_Id");

            entity.HasOne(d => d.Staff).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Staff");

            entity.HasOne(d => d.Subspecialty).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.SubspecialtyId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Subspecialty");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("staffs");

            entity.Property(e => e.Code).HasMaxLength(12);
        });

        modelBuilder.Entity<Subspecialty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("subspecialty");

            entity.Property(e => e.Code).HasMaxLength(12);
            entity.Property(e => e.Speciality).HasMaxLength(255);
            entity.Property(e => e.SubSpeciality)
                .HasMaxLength(255)
                .HasColumnName("Sub_Speciality");
        });

        modelBuilder.Entity<Theatre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("theatre");

            entity.Property(e => e.Equipment).HasMaxLength(255);
            entity.Property(e => e.Facility).HasMaxLength(255);
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.ScopeTheatreCode)
                .HasMaxLength(12)
                .HasColumnName("Scope_Theatre_Code");
            entity.Property(e => e.Specialties).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
