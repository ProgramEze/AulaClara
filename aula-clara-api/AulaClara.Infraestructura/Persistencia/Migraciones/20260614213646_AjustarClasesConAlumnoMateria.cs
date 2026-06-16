using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AulaClara.Infraestructura.Persistencia.Migraciones
{
    /// <inheritdoc />
    public partial class AjustarClasesConAlumnoMateria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clases_Alumnos_AlumnoId",
                table: "Clases");

            migrationBuilder.DropForeignKey(
                name: "FK_Clases_Materias_MateriaId",
                table: "Clases");

            migrationBuilder.DropIndex(
                name: "IX_Clases_AlumnoId",
                table: "Clases");

            migrationBuilder.DropColumn(
                name: "AlumnoId",
                table: "Clases");

            migrationBuilder.RenameColumn(
                name: "MateriaId",
                table: "Clases",
                newName: "AlumnoMateriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Clases_MateriaId",
                table: "Clases",
                newName: "IX_Clases_AlumnoMateriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clases_AlumnoMaterias_AlumnoMateriaId",
                table: "Clases",
                column: "AlumnoMateriaId",
                principalTable: "AlumnoMaterias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clases_AlumnoMaterias_AlumnoMateriaId",
                table: "Clases");

            migrationBuilder.RenameColumn(
                name: "AlumnoMateriaId",
                table: "Clases",
                newName: "MateriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Clases_AlumnoMateriaId",
                table: "Clases",
                newName: "IX_Clases_MateriaId");

            migrationBuilder.AddColumn<Guid>(
                name: "AlumnoId",
                table: "Clases",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Clases_AlumnoId",
                table: "Clases",
                column: "AlumnoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clases_Alumnos_AlumnoId",
                table: "Clases",
                column: "AlumnoId",
                principalTable: "Alumnos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clases_Materias_MateriaId",
                table: "Clases",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
