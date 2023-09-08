using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class WorkoutsPatientAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Workouts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_PatientId",
                table: "Workouts",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Patients_PatientId",
                table: "Workouts",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Patients_PatientId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_PatientId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Workouts");
        }
    }
}
