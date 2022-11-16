using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class AddDoctorClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_User_DoctorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_User_PatientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_User_UserId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingDays_User_UserId",
                table: "WorkingDays");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Township = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Allergies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Specialization = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BloodType = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patients_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientAllergies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    AllergyId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAllergies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientAllergies_Allergies_AllergyId",
                        column: x => x.AllergyId,
                        principalTable: "Allergies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientAllergies_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Deleted", "Number", "PostCode", "Street", "Township" },
                values: new object[,]
                {
                    { 1, "Novi Sad", false, "1", "21000", "neka ulica1", "Novi Sad" },
                    { 2, "Novi Sad", false, "2", "21000", "neka ulica2", "Novi Sad" },
                    { 3, "Ledinci", false, "3", "21207", "neka ulica3", "Novi Sad" },
                    { 4, "Sremska Kamenica", false, "4", "21200", "neka ulica4", "Novi Sad" }
                });

            migrationBuilder.InsertData(
                table: "Allergies",
                columns: new[] { "Id", "Deleted", "Name" },
                values: new object[,]
                {
                    { 13, false, "Alergija13" },
                    { 12, false, "Alergija12" },
                    { 11, false, "Alergija11" },
                    { 9, false, "Laktoza" },
                    { 8, false, "Gluten" },
                    { 7, false, "Kikiriki" },
                    { 10, false, "Alergija10" },
                    { 5, false, "Pcela" },
                    { 4, false, "Macka" },
                    { 3, false, "Pas" },
                    { 2, false, "Prasina" },
                    { 1, false, "Polen" },
                    { 6, false, "Ambrozija" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "BloodType", "Deleted", "DoctorId", "PersonId" },
                values: new object[,]
                {
                    { 5, 0, false, null, null },
                    { 6, 1, false, null, null },
                    { 7, 2, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "AddressId", "BirthDate", "Deleted", "Email", "Gender", "Name", "Role", "Surname" },
                values: new object[,]
                {
                    { 6, null, new DateTime(2022, 11, 9, 0, 0, 0, 0, DateTimeKind.Local), false, "pacijent2@gmail.com", 1, "Pacijent2", 1, "Markovic" },
                    { 1, null, new DateTime(2022, 11, 9, 0, 0, 0, 0, DateTimeKind.Local), false, "pera@gmail.com", 0, "Pera", 2, "Petrovic" },
                    { 2, null, new DateTime(2022, 11, 9, 0, 0, 0, 0, DateTimeKind.Local), false, "nikola@gmail.com", 0, "Nikola", 0, "Nikolic" },
                    { 3, null, new DateTime(2022, 11, 9, 0, 0, 0, 0, DateTimeKind.Local), false, "marko@gmail.com", 0, "Marko", 0, "Markovic" },
                    { 4, null, new DateTime(2022, 11, 9, 0, 0, 0, 0, DateTimeKind.Local), false, "stefan@gmail.com", 0, "Stefan", 0, "Stefanovic" },
                    { 5, null, new DateTime(2022, 11, 9, 0, 0, 0, 0, DateTimeKind.Local), false, "pacijent1@gmail.com", 0, "Pacijent1", 1, "Nikolic" },
                    { 7, null, new DateTime(2022, 11, 9, 0, 0, 0, 0, DateTimeKind.Local), false, "pacijent3@gmail.com", 2, "Pacijent3", 1, "Stefanovic" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_PersonId",
                table: "Doctors",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAllergies_AllergyId",
                table: "PatientAllergies",
                column: "AllergyId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAllergies_PatientId",
                table: "PatientAllergies",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DoctorId",
                table: "Patients",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PersonId",
                table: "Patients",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_AddressId",
                table: "Persons",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Persons_DoctorId",
                table: "Appointments",
                column: "DoctorId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Persons_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Persons_UserId",
                table: "Feedbacks",
                column: "UserId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingDays_Persons_UserId",
                table: "WorkingDays",
                column: "UserId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Persons_DoctorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Persons_PatientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Persons_UserId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingDays_Persons_UserId",
                table: "WorkingDays");

            migrationBuilder.DropTable(
                name: "PatientAllergies");

            migrationBuilder.DropTable(
                name: "Allergies");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_User_DoctorId",
                table: "Appointments",
                column: "DoctorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_User_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_User_UserId",
                table: "Feedbacks",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingDays_User_UserId",
                table: "WorkingDays",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
