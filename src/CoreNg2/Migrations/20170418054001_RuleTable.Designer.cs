using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CoreNg2.Models;

namespace CoreNg2.Migrations
{
    [DbContext(typeof(AssetsDBContext))]
    [Migration("20170418054001_RuleTable")]
    partial class RuleTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoreNg2.Models.Assets", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("CoreNg2.Models.Fields", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<int>("FkAssetId")
                        .HasColumnName("FK_AssetID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("FkAssetId")
                        .HasName("IX_Fields_FK_AssetID");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("CoreNg2.Models.Measurements", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<int>("FkWellsId")
                        .HasColumnName("FK_WellsID");

                    b.Property<int>("GreaterThan");

                    b.Property<bool>("GreaterThanActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasColumnName("tagName")
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("FkWellsId")
                        .HasName("IX_Measurements_FK_WellsID");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("CoreNg2.Models.Rules", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<int>("FkMeasurementsId")
                        .HasColumnName("FK_MeasurementsID");

                    b.Property<int>("FkRuleTypeId")
                        .HasColumnName("FK_RuleTypeId");

                    b.Property<bool>("IsActive")
                        .HasColumnName("isActive");

                    b.Property<double>("Value")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("0.0");

                    b.HasKey("Id");

                    b.HasIndex("FkMeasurementsId");

                    b.ToTable("Rules");
                });

            modelBuilder.Entity("CoreNg2.Models.RuleType", b =>
                {
                    b.Property<int>("RuleTypeId")
                        .HasColumnName("RuleTypeID");

                    b.Property<string>("RuleDescription")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.HasKey("RuleTypeId");

                    b.ToTable("RuleType");
                });

            modelBuilder.Entity("CoreNg2.Models.Wells", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<int>("FkFieldsId")
                        .HasColumnName("FK_FieldsID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("FkFieldsId")
                        .HasName("IX_Wells_FK_FieldsID");

                    b.ToTable("Wells");
                });

            modelBuilder.Entity("CoreNg2.Models.WEvents", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime");

                    b.Property<int>("MaxValue");

                    b.Property<int>("RuleId")
                        .HasColumnName("RuleID");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("RuleId");

                    b.ToTable("W_Events");
                });

            modelBuilder.Entity("CoreNg2.Models.Fields", b =>
                {
                    b.HasOne("CoreNg2.Models.Assets", "FkAsset")
                        .WithMany("Fields")
                        .HasForeignKey("FkAssetId")
                        .HasConstraintName("FK_Fields_ToAssets");
                });

            modelBuilder.Entity("CoreNg2.Models.Measurements", b =>
                {
                    b.HasOne("CoreNg2.Models.Wells", "FkWells")
                        .WithMany("Measurements")
                        .HasForeignKey("FkWellsId")
                        .HasConstraintName("FK_Measurements_ToWells");
                });

            modelBuilder.Entity("CoreNg2.Models.Rules", b =>
                {
                    b.HasOne("CoreNg2.Models.Measurements", "FkMeasurements")
                        .WithMany("Rules")
                        .HasForeignKey("FkMeasurementsId")
                        .HasConstraintName("FK_Rules_ToMeasurements");
                });

            modelBuilder.Entity("CoreNg2.Models.Wells", b =>
                {
                    b.HasOne("CoreNg2.Models.Fields", "FkFields")
                        .WithMany("Wells")
                        .HasForeignKey("FkFieldsId")
                        .HasConstraintName("FK_Wells_ToFields");
                });

            modelBuilder.Entity("CoreNg2.Models.WEvents", b =>
                {
                    b.HasOne("CoreNg2.Models.Rules", "Rule")
                        .WithMany("WEvents")
                        .HasForeignKey("RuleId")
                        .HasConstraintName("FK_W_Events_ToRules");
                });
        }
    }
}
