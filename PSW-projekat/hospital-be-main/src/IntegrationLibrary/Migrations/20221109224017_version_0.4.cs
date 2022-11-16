using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegrationLibrary.Migrations
{
    public partial class version_04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BloodRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequiredForDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BloodQuantity = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    RequestState = table.Column<int>(type: "int", nullable: false),
                    BloodType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodRequests", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "ReportSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDeliveryDate",
                value: new DateTime(2022, 11, 9, 0, 0, 0, 0, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BloodRequests");

            migrationBuilder.UpdateData(
                table: "ReportSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDeliveryDate",
                value: new DateTime(2022, 11, 7, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
