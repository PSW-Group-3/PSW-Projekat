using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class mealQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealAnswers_MealQuestion_MealQuestionId",
                table: "MealAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealQuestion",
                table: "MealQuestion");

            migrationBuilder.RenameTable(
                name: "MealQuestion",
                newName: "MealQuestions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealQuestions",
                table: "MealQuestions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MealAnswers_MealQuestions_MealQuestionId",
                table: "MealAnswers",
                column: "MealQuestionId",
                principalTable: "MealQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealAnswers_MealQuestions_MealQuestionId",
                table: "MealAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealQuestions",
                table: "MealQuestions");

            migrationBuilder.RenameTable(
                name: "MealQuestions",
                newName: "MealQuestion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealQuestion",
                table: "MealQuestion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MealAnswers_MealQuestion_MealQuestionId",
                table: "MealAnswers",
                column: "MealQuestionId",
                principalTable: "MealQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
