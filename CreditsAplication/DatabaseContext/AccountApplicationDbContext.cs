using System;
using System.Collections.Generic;
using CreditsAplication.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CreditsAplication.DatabaseContext;

public partial class AccountApplicationDbContext : DbContext
{
    public AccountApplicationDbContext()
    {
    }

    public AccountApplicationDbContext(DbContextOptions<AccountApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC0763D35E9B");

            entity.ToTable("Account");

            entity.HasIndex(e => e.AccountNumber, "UQ__Account__BE2ACD6F5D824367").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.AccountNumber).HasMaxLength(50);
            entity.Property(e => e.AccountType);
            entity.Property(e => e.InitialBalance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Client).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__Account__ClientI__3F466844");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Client__3214EC07E51D6DAB");

            entity.ToTable("Client");

            entity.HasIndex(e => e.PersonId, "UQ__Client__AA2FFBE46C4E5259").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Status);

            entity.HasOne(d => d.Person).WithOne(p => p.Client)
                .HasForeignKey<Client>(d => d.PersonId)
                .HasConstraintName("FK__Client__PersonId__3B75D760");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Person__3214EC07499F1BAC");

            entity.ToTable("Person");

            entity.HasIndex(e => e.Identification, "UQ__Person__724F06FDC1AFD1EB").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Identification).HasMaxLength(20);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC07BA53E00B");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TransactionType).HasMaxLength(50);
            entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Account).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Transacti__Accou__4316F928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
