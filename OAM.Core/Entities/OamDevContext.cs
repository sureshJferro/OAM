using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OAM.Core.Entities;

public partial class OamDevContext : DbContext
{
    public OamDevContext()
    {
    }

    public OamDevContext(DbContextOptions<OamDevContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=SURESH-P;Database=OAM_Dev;User ID=sa;Password=Ferro@1234;Language=British English; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
                .HasColumnType("datetime")
                .HasColumnName("Is_Deleted");
            entity.Property(e => e.PasswordHash).HasDefaultValueSql("(0x)");
            entity.Property(e => e.PasswordSalt).HasDefaultValueSql("(0x)");
            entity.Property(e => e.UpdatedTimeStamp)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Time_Stamp");
            entity.Property(e => e.UserName).HasMaxLength(450);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
