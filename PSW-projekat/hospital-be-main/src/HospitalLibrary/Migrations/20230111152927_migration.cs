using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_DoctorExaminationEvents_DoctorSelectedPrescriptionsId",
                table: "Prescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Symptoms_DoctorExaminationEvents_DoctorSelectedSymptomsId",
                table: "Symptoms");

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

            migrationBuilder.AddColumn<string>(
                name: "Prescriptions",
                table: "DoctorExaminationEvents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Symptoms",
                table: "DoctorExaminationEvents",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prescriptions",
                table: "DoctorExaminationEvents");

            migrationBuilder.DropColumn(
                name: "Symptoms",
                table: "DoctorExaminationEvents");

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

            migrationBuilder.CreateIndex(
                name: "IX_Symptoms_DoctorSelectedSymptomsId",
                table: "Symptoms",
                column: "DoctorSelectedSymptomsId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DoctorSelectedPrescriptionsId",
                table: "Prescriptions",
                column: "DoctorSelectedPrescriptionsId");

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
    }
}
