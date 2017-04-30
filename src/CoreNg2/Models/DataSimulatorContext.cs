using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoreNg2.Models
{
    public partial class DataSimulatorContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=tcp:chevrondata.database.windows.net,1433;Initial Catalog=DataSimulator;Persist Security Info=False;User ID=chaincoders;Password=EveryDayImCoding0;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrentValues>(entity =>
            {
                entity.HasKey(e => new { e.Tag, e.Time, e.Value })
                    .HasName("PK_current_values");

                entity.ToTable("current_values");

                entity.Property(e => e.Tag)
                    .HasColumnName("tag")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.HasKey(e => new { e.Tag, e.Time, e.Value })
                    .HasName("PK_history");

                entity.ToTable("history");

                entity.Property(e => e.Tag)
                    .HasColumnName("tag")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<Input>(entity =>
            {
                entity.HasKey(e => e.EventId)
                    .HasName("PK_events_temp");

                entity.ToTable("input");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.Starttime)
                    .HasColumnName("starttime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Tag)
                    .HasColumnName("tag")
                    .HasColumnType("varchar(50)");
            });
        }

        public virtual DbSet<CurrentValues> CurrentValues { get; set; }
        public virtual DbSet<History> History { get; set; }
        public virtual DbSet<Input> Input { get; set; }

        // Unable to generate entity type for table 'dbo.dates'. Please see the warning messages.
    }
}