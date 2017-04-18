using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoreNg2.Migrations
{
    public partial class RuleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FK_MeasurementsID = table.Column<int>(nullable: false),
                    FK_RuleTypeId = table.Column<int>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    Value = table.Column<double>(nullable: false, defaultValueSql: "0.0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Rules_ToMeasurements",
                        column: x => x.FK_MeasurementsID,
                        principalTable: "Measurements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RuleType",
                columns: table => new
                {
                    RuleTypeID = table.Column<int>(nullable: false),
                    RuleDescription = table.Column<string>(type: "varchar(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleType", x => x.RuleTypeID);
                });

            migrationBuilder.CreateTable(
                name: "W_Events",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EndTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    MaxValue = table.Column<int>(nullable: false),
                    RuleID = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_W_Events", x => x.ID);
                    table.ForeignKey(
                        name: "FK_W_Events_ToRules",
                        column: x => x.RuleID,
                        principalTable: "Rules",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rules_FK_MeasurementsID",
                table: "Rules",
                column: "FK_MeasurementsID");

            migrationBuilder.CreateIndex(
                name: "IX_W_Events_RuleID",
                table: "W_Events",
                column: "RuleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RuleType");

            migrationBuilder.DropTable(
                name: "W_Events");

            migrationBuilder.DropTable(
                name: "Rules");
        }
    }
}
