using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualHoftalon_Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Patients_Cpf",
                table: "Patients",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Email",
                table: "Patients",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Rg",
                table: "Patients",
                column: "Rg",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_Cpf",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_Email",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_Rg",
                table: "Patients");
        }
    }
}
