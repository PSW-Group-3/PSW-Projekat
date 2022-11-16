using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class ChangeUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
