using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreNg2.Migrations
{
    public partial class MeasurementTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GreaterThan",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "GreaterThanActive",
                table: "Measurements");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Measurements",
                type: "varchar(256)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Measurements");

            migrationBuilder.AddColumn<int>(
                name: "GreaterThan",
                table: "Measurements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "GreaterThanActive",
                table: "Measurements",
                nullable: false,
                defaultValue: false);
        }
    }
}
