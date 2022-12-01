using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TaxApp.Infrastructure.DataAccess.Models
{
    public partial class TaxApplicationDbContext : DbContext
    {
        public TaxApplicationDbContext()
        {
        }

        public TaxApplicationDbContext(DbContextOptions<TaxApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CountyMaster> CountyMaster { get; set; }
        public virtual DbSet<Municipality> Municipality { get; set; }
        public virtual DbSet<MunicipalityTaxDetails> MunicipalityTaxDetails { get; set; }
        public virtual DbSet<TaxSlabDetails> TaxSlabDetails { get; set; }
        public virtual DbSet<TaxSlabTypeMaster> TaxSlabTypeMaster { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CountyMaster>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Municipality>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.CountyMaster)
                    .WithMany(p => p.InverseCountyMaster)
                    .HasForeignKey(d => d.CountyMasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Municipality_Municipality");
            });

            modelBuilder.Entity<MunicipalityTaxDetails>(entity =>
            {
                entity.HasOne(d => d.Municipality)
                    .WithMany(p => p.MunicipalityTaxDetails)
                    .HasForeignKey(d => d.MunicipalityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MunicipalityTaxDetails_Municipality");

                entity.HasOne(d => d.TaxSlabDetails)
                    .WithMany(p => p.MunicipalityTaxDetails)
                    .HasForeignKey(d => d.TaxSlabDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MunicipalityTaxDetails_TaxSlabDetails");
            });

            modelBuilder.Entity<TaxSlabDetails>(entity =>
            {
                entity.Property(e => e.ApplicableFrom).HasColumnType("datetime");

                entity.Property(e => e.ApplicableTo).HasColumnType("datetime");

                entity.HasOne(d => d.TaxSlabType)
                    .WithMany(p => p.TaxSlabDetails)
                    .HasForeignKey(d => d.TaxSlabTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaxSlabDetails_TaxSlabTypeMaster");
            });

            modelBuilder.Entity<TaxSlabTypeMaster>(entity =>
            {
                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
