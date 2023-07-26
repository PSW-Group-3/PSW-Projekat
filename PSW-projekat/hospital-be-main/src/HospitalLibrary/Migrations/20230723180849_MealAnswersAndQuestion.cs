using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class MealAnswersAndQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyDiets");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfMeal",
                table: "Meals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "MealQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MealType = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealQuestion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MealQuestionId = table.Column<int>(type: "int", nullable: true),
                    MealId = table.Column<int>(type: "int", nullable: true),
                    Answer = table.Column<float>(type: "real", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealAnswers_MealQuestion_MealQuestionId",
                        column: x => x.MealQuestionId,
                        principalTable: "MealQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MealAnswers_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MealAnswers_MealId",
                table: "MealAnswers",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_MealAnswers_MealQuestionId",
                table: "MealAnswers",
                column: "MealQuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MealAnswers");

            migrationBuilder.DropTable(
                name: "MealQuestion");

            migrationBuilder.DropColumn(
                name: "DateOfMeal",
                table: "Meals");

            migrationBuilder.CreateTable(
                name: "DailyDiets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BreakfastId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DinnerId = table.Column<int>(type: "int", nullable: true),
                    LunchId = table.Column<int>(type: "int", nullable: true),
                    PersonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyDiets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyDiets_Meals_BreakfastId",
                        column: x => x.BreakfastId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyDiets_Meals_DinnerId",
                        column: x => x.DinnerId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyDiets_Meals_LunchId",
                        column: x => x.LunchId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailyDiets_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyDiets_BreakfastId",
                table: "DailyDiets",
                column: "BreakfastId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyDiets_DinnerId",
                table: "DailyDiets",
                column: "DinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyDiets_LunchId",
                table: "DailyDiets",
                column: "LunchId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyDiets_PersonId",
                table: "DailyDiets",
                column: "PersonId");
        }
    }
}
