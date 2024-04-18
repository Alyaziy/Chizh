﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Chizh;

public partial class User24Context : DbContext
{
    public User24Context()
    {
    }

    public User24Context(DbContextOptions<User24Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Check> Checks { get; set; }

    public virtual DbSet<Muscle> Muscles { get; set; }

    public virtual DbSet<Poze> Pozes { get; set; }

    public virtual DbSet<Train> Trains { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=192.168.200.35;database=user24;user=user24;password=22598;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Check>(entity =>
        {
            entity.ToTable("Check");

            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Weight1).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Checks)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_Check_User");
        });

        modelBuilder.Entity<Muscle>(entity =>
        {
            entity.ToTable("Muscle");

            entity.Property(e => e.MuTittle).HasMaxLength(10);
        });

        modelBuilder.Entity<Poze>(entity =>
        {
            entity.ToTable("Poze");

            entity.Property(e => e.Image).HasColumnType("image");
            entity.Property(e => e.Time).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdMuscleNavigation).WithMany(p => p.Pozes)
                .HasForeignKey(d => d.IdMuscle)
                .HasConstraintName("FK_Poze_Muscle");
        });

        modelBuilder.Entity<Train>(entity =>
        {
            entity.ToTable("Train");

            entity.Property(e => e.TrTime).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TrTittle).HasMaxLength(10);

            entity.HasOne(d => d.IdMuscleNavigation).WithMany(p => p.Trains)
                .HasForeignKey(d => d.IdMuscle)
                .HasConstraintName("FK_Train_Muscle");

            entity.HasOne(d => d.IdPozeNavigation).WithMany(p => p.Trains)
                .HasForeignKey(d => d.IdPoze)
                .HasConstraintName("FK_Train_Poze");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Name).HasMaxLength(10);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Weight).HasColumnType("decimal(18, 0)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
