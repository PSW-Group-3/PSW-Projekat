using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegrationLibrary.Migrations
{
    public partial class version_05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Newses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    _title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    _text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    _dateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    _status = table.Column<int>(type: "int", nullable: false),
                    _bloodBankId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BloodBankId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Newses",
                columns: new[] { "Id", "BloodBankId", "DateTime", "Status", "Text", "Title", "_bloodBankId", "_dateTime", "_status", "_text", "_title" },
                values: new object[] { 2, 1, new DateTime(2022, 11, 16, 0, 0, 0, 0, DateTimeKind.Local), 0, "Come and give me blood!", "First blood of the year!", 1, new DateTime(2022, 11, 16, 0, 0, 0, 0, DateTimeKind.Local), 0, "Come and give me blood!", "First blood of the year!" });

            migrationBuilder.UpdateData(
                table: "ReportSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDeliveryDate",
                value: new DateTime(2022, 11, 16, 0, 0, 0, 0, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Newses");

            migrationBuilder.UpdateData(
                table: "ReportSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDeliveryDate",
                value: new DateTime(2022, 11, 9, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
