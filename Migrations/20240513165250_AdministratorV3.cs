using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualHoftalon_Server.Migrations
{
    /// <inheritdoc />
    public partial class AdministratorV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrators_Logins_LoginId",
                table: "Administrators");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Logins_LoginId",
                table: "Doctors");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrators_Logins_LoginId",
                table: "Administrators",
                column: "LoginId",
                principalTable: "Logins",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Logins_LoginId",
                table: "Doctors",
                column: "LoginId",
                principalTable: "Logins",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrators_Logins_LoginId",
                table: "Administrators");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Logins_LoginId",
                table: "Doctors");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrators_Logins_LoginId",
                table: "Administrators",
                column: "LoginId",
                principalTable: "Logins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Logins_LoginId",
                table: "Doctors",
                column: "LoginId",
                principalTable: "Logins",
                principalColumn: "Id");
        }
    }
}
