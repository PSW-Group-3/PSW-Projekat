using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class WorkoutTypeRenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Workouts",
                newName: "WorkoutType");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "GymWorkouts",
                newName: "WorkoutType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkoutType",
                table: "Workouts",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "WorkoutType",
                table: "GymWorkouts",
                newName: "Type");
        }
    }
}
