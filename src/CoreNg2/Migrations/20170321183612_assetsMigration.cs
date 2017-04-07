using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreNg2.Migrations
{
    public partial class assetsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    FK_AssetID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Fields_ToAssets",
                        column: x => x.FK_AssetID,
                        principalTable: "Assets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wells",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    FK_FieldsID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wells", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Wells_ToFields",
                        column: x => x.FK_FieldsID,
                        principalTable: "Fields",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Name = table.Column<string>(type: "varchar(256)", nullable: false),
                    FK_WellsID = table.Column<int>(nullable: false),
                    GreaterThan = table.Column<int>(nullable: false),
                    GreaterThanActive = table.Column<bool>(nullable: false),
                    ID = table.Column<int>(nullable: false),
                    tagName = table.Column<string>(type: "varchar(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Measurme__737584F79A904643", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Measurements_ToWells",
                        column: x => x.FK_WellsID,
                        principalTable: "Wells",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fields_FK_AssetID",
                table: "Fields",
                column: "FK_AssetID");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_FK_WellsID",
                table: "Measurements",
                column: "FK_WellsID");

            migrationBuilder.CreateIndex(
                name: "IX_Wells_FK_FieldsID",
                table: "Wells",
                column: "FK_FieldsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "Wells");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "Assets");
        }
    }
}
