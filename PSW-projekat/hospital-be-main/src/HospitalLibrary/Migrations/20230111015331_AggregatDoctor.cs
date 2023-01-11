using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class AggregatDoctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorSelectedSymptomsId",
                table: "Symptoms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoctorSelectedPrescriptionsId",
                table: "Prescriptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DoctorExaminations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stage = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: true),
                    startTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorExaminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorExaminations_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoctorExaminationEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AggregateId = table.Column<int>(type: "int", nullable: true),
                    phase = table.Column<int>(type: "int", nullable: false),
                    selectionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Report = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorExaminationEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorExaminationEvents_DoctorExaminations_AggregateId",
                        column: x => x.AggregateId,
                        principalTable: "DoctorExaminations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Symptoms_DoctorSelectedSymptomsId",
                table: "Symptoms",
                column: "DoctorSelectedSymptomsId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DoctorSelectedPrescriptionsId",
                table: "Prescriptions",
                column: "DoctorSelectedPrescriptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorExaminationEvents_AggregateId",
                table: "DoctorExaminationEvents",
                column: "AggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorExaminations_DoctorId",
                table: "DoctorExaminations",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_DoctorExaminationEvents_DoctorSelectedPrescriptionsId",
                table: "Prescriptions",
                column: "DoctorSelectedPrescriptionsId",
                principalTable: "DoctorExaminationEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Symptoms_DoctorExaminationEvents_DoctorSelectedSymptomsId",
                table: "Symptoms",
                column: "DoctorSelectedSymptomsId",
                principalTable: "DoctorExaminationEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_DoctorExaminationEvents_DoctorSelectedPrescriptionsId",
                table: "Prescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Symptoms_DoctorExaminationEvents_DoctorSelectedSymptomsId",
                table: "Symptoms");

            migrationBuilder.DropTable(
                name: "DoctorExaminationEvents");

            migrationBuilder.DropTable(
                name: "DoctorExaminations");

            migrationBuilder.DropIndex(
                name: "IX_Symptoms_DoctorSelectedSymptomsId",
                table: "Symptoms");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_DoctorSelectedPrescriptionsId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "DoctorSelectedSymptomsId",
                table: "Symptoms");

            migrationBuilder.DropColumn(
                name: "DoctorSelectedPrescriptionsId",
                table: "Prescriptions");
        }
    }
}
