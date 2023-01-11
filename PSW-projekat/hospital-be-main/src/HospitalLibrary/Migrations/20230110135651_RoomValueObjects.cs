using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class RoomValueObjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bloods_Rooms_RoomId",
                table: "Bloods");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.RenameColumn(
                name: "Floor",
                table: "Rooms",
                newName: "Floor_Floor");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Rooms",
                newName: "Number_Name");

            migrationBuilder.AlterColumn<int>(
                name: "Floor_Floor",
                table: "Rooms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PrescriptionId",
                table: "Medicines",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Bloods",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DoctorsCouncils",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorsCouncils", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Examinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentId = table.Column<int>(type: "int", nullable: true),
                    Report = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examinations_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotificationFor = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoctorDoctorsCouncil",
                columns: table => new
                {
                    CouncilsId = table.Column<int>(type: "int", nullable: false),
                    DoctorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorDoctorsCouncil", x => new { x.CouncilsId, x.DoctorsId });
                    table.ForeignKey(
                        name: "FK_DoctorDoctorsCouncil_Doctors_DoctorsId",
                        column: x => x.DoctorsId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorDoctorsCouncil_DoctorsCouncils_CouncilsId",
                        column: x => x.CouncilsId,
                        principalTable: "DoctorsCouncils",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExaminationId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Examinations_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Symptoms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExaminationId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symptoms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Symptoms_Examinations_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_PrescriptionId",
                table: "Medicines",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorDoctorsCouncil_DoctorsId",
                table: "DoctorDoctorsCouncil",
                column: "DoctorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_AppointmentId",
                table: "Examinations",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_ExaminationId",
                table: "Prescriptions",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_Symptoms_ExaminationId",
                table: "Symptoms",
                column: "ExaminationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bloods_Rooms_RoomId",
                table: "Bloods",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Prescriptions_PrescriptionId",
                table: "Medicines",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bloods_Rooms_RoomId",
                table: "Bloods");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Prescriptions_PrescriptionId",
                table: "Medicines");

            migrationBuilder.DropTable(
                name: "DoctorDoctorsCouncil");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Symptoms");

            migrationBuilder.DropTable(
                name: "DoctorsCouncils");

            migrationBuilder.DropTable(
                name: "Examinations");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_PrescriptionId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "PrescriptionId",
                table: "Medicines");

            migrationBuilder.RenameColumn(
                name: "Floor_Floor",
                table: "Rooms",
                newName: "Floor");

            migrationBuilder.RenameColumn(
                name: "Number_Name",
                table: "Rooms",
                newName: "Number");

            migrationBuilder.AlterColumn<int>(
                name: "Floor",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Bloods",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Deleted", "Floor", "Number", "RoomType" },
                values: new object[,]
                {
                    { 1, false, 1, "101A", 5 },
                    { 2, false, 2, "204", 5 },
                    { 3, false, 3, "305B", 5 },
                    { 4, false, 3, "STORAGE", 0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Bloods_Rooms_RoomId",
                table: "Bloods",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
