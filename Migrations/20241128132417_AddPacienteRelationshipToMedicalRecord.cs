using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPacienteRelationshipToMedicalRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Prontuarios_PacienteId",
                table: "Prontuarios",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prontuarios_Pacientes_PacienteId",
                table: "Prontuarios",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prontuarios_Pacientes_PacienteId",
                table: "Prontuarios");

            migrationBuilder.DropIndex(
                name: "IX_Prontuarios_PacienteId",
                table: "Prontuarios");
        }
    }
}
