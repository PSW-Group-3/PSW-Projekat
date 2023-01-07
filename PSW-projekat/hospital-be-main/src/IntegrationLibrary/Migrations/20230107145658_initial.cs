using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegrationLibrary.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BloodBanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServerAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordResetKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountStatus = table.Column<int>(type: "int", nullable: false),
                    GRPCServerAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodBanks", x => x.Id);
                });

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
                    BloodType = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodBankId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyBloodRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BloodQuantity = table.Column<int>(type: "int", nullable: false),
                    BloodType = table.Column<int>(type: "int", nullable: false),
                    BloodBankId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyBloodRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Newses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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

            migrationBuilder.CreateTable(
                name: "ReportSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryYears = table.Column<int>(type: "int", nullable: false),
                    DeliveryMonths = table.Column<int>(type: "int", nullable: false),
                    DeliveryDays = table.Column<int>(type: "int", nullable: false),
                    CalculationYears = table.Column<int>(type: "int", nullable: false),
                    CalculationMonths = table.Column<int>(type: "int", nullable: false),
                    CalculationDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfMonth = table.Column<int>(type: "int", nullable: false),
                    APlus = table.Column<int>(type: "int", nullable: false),
                    BPlus = table.Column<int>(type: "int", nullable: false),
                    ABPlus = table.Column<int>(type: "int", nullable: false),
                    OPlus = table.Column<int>(type: "int", nullable: false),
                    AMinus = table.Column<int>(type: "int", nullable: false),
                    BMinus = table.Column<int>(type: "int", nullable: false),
                    ABMinus = table.Column<int>(type: "int", nullable: false),
                    OMinus = table.Column<int>(type: "int", nullable: false),
                    BankEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankApiKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HospitalEmail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    Demands = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    BloodBankId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TenderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bids_Tenders_TenderId",
                        column: x => x.TenderId,
                        principalTable: "Tenders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bids_TenderId",
                table: "Bids",
                column: "TenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "BloodBanks");

            migrationBuilder.DropTable(
                name: "BloodRequests");

            migrationBuilder.DropTable(
                name: "EmergencyBloodRequests");

            migrationBuilder.DropTable(
                name: "Newses");

            migrationBuilder.DropTable(
                name: "ReportSettings");

            migrationBuilder.DropTable(
                name: "ScheduledOrders");

            migrationBuilder.DropTable(
                name: "Tenders");
        }
    }
}
