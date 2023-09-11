using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class WorkoutModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Workouts_GymWorkoutId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Workouts");

            migrationBuilder.CreateTable(
                name: "GymWorkouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<double>(type: "float", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymWorkouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GymWorkouts_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GymWorkouts_PatientId",
                table: "GymWorkouts",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_GymWorkouts_GymWorkoutId",
                table: "Exercises",
                column: "GymWorkoutId",
                principalTable: "GymWorkouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_GymWorkouts_GymWorkoutId",
                table: "Exercises");

            migrationBuilder.DropTable(
                name: "GymWorkouts");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Workouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Workouts_GymWorkoutId",
                table: "Exercises",
                column: "GymWorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
