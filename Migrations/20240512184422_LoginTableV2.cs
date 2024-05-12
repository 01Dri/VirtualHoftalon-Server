﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualHoftalon_Server.Migrations
{
    /// <inheritdoc />
    public partial class LoginTableV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Logins_Username",
                table: "Logins",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Logins_Username",
                table: "Logins");
        }
    }
}
