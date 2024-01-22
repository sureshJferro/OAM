﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace OAM.Core.Entities;

public partial class OamDevContext : DbContext
{
    //Declaration
    private readonly IConfiguration _config;

    //Constructor
    public OamDevContext(IConfiguration configuration)
    {
        _config = configuration;
    }


    public OamDevContext(DbContextOptions<OamDevContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApiRequestResponseLog> ApiRequestResponseLogs { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(_config.GetConnectionString(Helpers.Constants.OAMConnection));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApiRequestResponseLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__api_requ__9E2397E07D548E63");

            entity.ToTable("api_request_response_log");

            entity.Property(e => e.LogId).HasColumnName("log_id");
            entity.Property(e => e.CreateTimeStamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("create_time_stamp");
            entity.Property(e => e.RequestBody).HasColumnName("request_body");
            entity.Property(e => e.RequestMethod)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("request_method");
            entity.Property(e => e.RequestPath)
                .HasMaxLength(255)
                .HasColumnName("request_path");
            entity.Property(e => e.ResponseBody).HasColumnName("response_body");
            entity.Property(e => e.ResponseStatusCode).HasColumnName("response_status_code");
            entity.Property(e => e.UpdateTimeStamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("update_time_stamp");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.CreatedTimeStamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Time_Stamp");
            entity.Property(e => e.Email).HasMaxLength(450);
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("Is_Deleted");
            entity.Property(e => e.PasswordHash).HasDefaultValueSql("(0x)");
            entity.Property(e => e.PasswordSalt).HasDefaultValueSql("(0x)");
            entity.Property(e => e.UpdatedTimeStamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Updated_Time_Stamp");
            entity.Property(e => e.UserName).HasMaxLength(450);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
