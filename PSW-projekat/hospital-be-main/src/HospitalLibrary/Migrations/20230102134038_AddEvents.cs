using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class AddEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentDoctorSelectedEvent",
                table: "AppointmentSchedulingEvents");

            migrationBuilder.DropColumn(
                name: "AppointmentDoctorSpecializationSelectedEvent",
                table: "AppointmentSchedulingEvents");

            migrationBuilder.DropColumn(
                name: "AppointmentTime",
                table: "AppointmentSchedulingEvents");

            migrationBuilder.DropColumn(
                name: "AppointmentTimeSelectedEvent",
                table: "AppointmentSchedulingEvents");

            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "AppointmentSchedulingEvents");

            migrationBuilder.RenameColumn(
                name: "DoctorSpecialization",
                table: "AppointmentSchedulingEvents",
                newName: "selectedItem");

            migrationBuilder.AddColumn<DateTime>(
                name: "endTime",
                table: "ScheduleAppointmentByPatients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "startTime",
                table: "ScheduleAppointmentByPatients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "phase",
                table: "AppointmentSchedulingEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "selectionTime",
                table: "AppointmentSchedulingEvents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "endTime",
                table: "ScheduleAppointmentByPatients");

            migrationBuilder.DropColumn(
                name: "startTime",
                table: "ScheduleAppointmentByPatients");

            migrationBuilder.DropColumn(
                name: "phase",
                table: "AppointmentSchedulingEvents");

            migrationBuilder.DropColumn(
                name: "selectionTime",
                table: "AppointmentSchedulingEvents");

            migrationBuilder.RenameColumn(
                name: "selectedItem",
                table: "AppointmentSchedulingEvents",
                newName: "DoctorSpecialization");

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDoctorSelectedEvent",
                table: "AppointmentSchedulingEvents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDoctorSpecializationSelectedEvent",
                table: "AppointmentSchedulingEvents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentTime",
                table: "AppointmentSchedulingEvents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentTimeSelectedEvent",
                table: "AppointmentSchedulingEvents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "AppointmentSchedulingEvents",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
