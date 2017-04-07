using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoreNg2.Migrations
{
    public partial class AutoIncrement_mesurementsPK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__Measurme__737584F79A904643",
                table: "Measurements");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "Wells",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "Measurements",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Measurements",
                table: "Measurements",
                column: "ID");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "Fields",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Measurements",
                table: "Measurements");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "Wells",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "Measurements",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK__Measurme__737584F79A904643",
                table: "Measurements",
                column: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "Fields",
                nullable: false);
        }
    }
}
