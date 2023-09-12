using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class MealModelChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealAnswers_Meals_MealId",
                table: "MealAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Persons_PersonId",
                table: "Meals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealAnswers",
                table: "MealAnswers");

            migrationBuilder.DropIndex(
                name: "IX_MealAnswers_MealId",
                table: "MealAnswers");

            migrationBuilder.RenameColumn(
                name: "HealthScoreDelta",
                table: "PatientHealthInformations",
                newName: "Score");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Meals",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Meals_PersonId",
                table: "Meals",
                newName: "IX_Meals_PatientId");

            migrationBuilder.AlterColumn<int>(
                name: "MealId",
                table: "MealAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealAnswers",
                table: "MealAnswers",
                columns: new[] { "MealId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_MealAnswers_Meals_MealId",
                table: "MealAnswers",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Patients_PatientId",
                table: "Meals",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealAnswers_Meals_MealId",
                table: "MealAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Patients_PatientId",
                table: "Meals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealAnswers",
                table: "MealAnswers");

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "PatientHealthInformations",
                newName: "HealthScoreDelta");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Meals",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Meals_PatientId",
                table: "Meals",
                newName: "IX_Meals_PersonId");

            migrationBuilder.AlterColumn<int>(
                name: "MealId",
                table: "MealAnswers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealAnswers",
                table: "MealAnswers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MealAnswers_MealId",
                table: "MealAnswers",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_MealAnswers_Meals_MealId",
                table: "MealAnswers",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Persons_PersonId",
                table: "Meals",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
