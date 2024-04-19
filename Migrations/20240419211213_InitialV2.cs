using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualHoftalon_Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hour",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Appointments",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Hour",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Appointments");

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "Appointments",
                type: "datetime2",
                nullable: true);
        }
    }
}
