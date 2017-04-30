using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoreNg2.Models
{
    public partial class AssetsDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=tcp:chevrondata.database.windows.net,1433;Initial Catalog=DataSimulator;Persist Security Info=False;User ID=chaincoders;Password=EveryDayImCoding0;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assets>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(256)");
            });

            modelBuilder.Entity<Fields>(entity =>
            {
                entity.HasIndex(e => e.FkAssetId)
                    .HasName("IX_Fields_FK_AssetID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FkAssetId).HasColumnName("FK_AssetID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(256)");

                entity.HasOne(d => d.FkAsset)
                    .WithMany(p => p.Fields)
                    .HasForeignKey(d => d.FkAssetId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Fields_ToAssets");
            });

            modelBuilder.Entity<Measurements>(entity =>
            {
                entity.HasIndex(e => e.FkWellsId)
                    .HasName("IX_Measurements_FK_WellsID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).HasColumnType("varchar(256)");

                entity.Property(e => e.FkWellsId).HasColumnName("FK_WellsID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(256)");

                entity.Property(e => e.TagName)
                    .IsRequired()
                    .HasColumnName("tagName")
                    .HasColumnType("varchar(256)");

                entity.HasOne(d => d.FkWells)
                    .WithMany(p => p.Measurements)
                    .HasForeignKey(d => d.FkWellsId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Measurements_ToWells");
            });

            modelBuilder.Entity<RuleType>(entity =>
            {
                entity.Property(e => e.RuleTypeId)
                    .HasColumnName("RuleTypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.RuleDescription)
                    .IsRequired()
                    .HasColumnType("varchar(256)");
            });

            modelBuilder.Entity<Rules>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FkMeasurementsId).HasColumnName("FK_MeasurementsID");

                entity.Property(e => e.FkRuleTypeId).HasColumnName("FK_RuleTypeId");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Value).HasDefaultValueSql("0.0");

                entity.HasOne(d => d.FkMeasurements)
                    .WithMany(p => p.Rules)
                    .HasForeignKey(d => d.FkMeasurementsId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Rules_ToMeasurements");
            });

            modelBuilder.Entity<WEvents>(entity =>
            {
                entity.ToTable("W_Events");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.RuleId).HasColumnName("RuleID");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.Rule)
                    .WithMany(p => p.WEvents)
                    .HasForeignKey(d => d.RuleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_W_Events_ToRules");
            });

            modelBuilder.Entity<Wells>(entity =>
            {
                entity.HasIndex(e => e.FkFieldsId)
                    .HasName("IX_Wells_FK_FieldsID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FkFieldsId).HasColumnName("FK_FieldsID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(256)");

                entity.HasOne(d => d.FkFields)
                    .WithMany(p => p.Wells)
                    .HasForeignKey(d => d.FkFieldsId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Wells_ToFields");
            });
        }

        public virtual DbSet<Assets> Assets { get; set; }
        public virtual DbSet<Fields> Fields { get; set; }
        public virtual DbSet<Measurements> Measurements { get; set; }
        public virtual DbSet<RuleType> RuleType { get; set; }
        public virtual DbSet<Rules> Rules { get; set; }
        public virtual DbSet<WEvents> WEvents { get; set; }
        public virtual DbSet<Wells> Wells { get; set; }

        // Unable to generate entity type for table 'dbo.history'. Please see the warning messages.
    }
}