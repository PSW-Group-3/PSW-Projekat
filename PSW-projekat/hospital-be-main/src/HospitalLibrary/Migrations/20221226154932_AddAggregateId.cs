using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalLibrary.Migrations
{
    public partial class AddAggregateId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentSchedulingEvents_ScheduleAppointmentByPatients_ScheduleAppointmentByPatientId",
                table: "AppointmentSchedulingEvents");

            migrationBuilder.RenameColumn(
                name: "ScheduleAppointmentByPatientId",
                table: "AppointmentSchedulingEvents",
                newName: "AggregateId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentSchedulingEvents_ScheduleAppointmentByPatientId",
                table: "AppointmentSchedulingEvents",
                newName: "IX_AppointmentSchedulingEvents_AggregateId");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "AppointmentSchedulingEvents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentSchedulingEvents_ScheduleAppointmentByPatients_AggregateId",
                table: "AppointmentSchedulingEvents",
                column: "AggregateId",
                principalTable: "ScheduleAppointmentByPatients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentSchedulingEvents_ScheduleAppointmentByPatients_AggregateId",
                table: "AppointmentSchedulingEvents");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "AppointmentSchedulingEvents");

            migrationBuilder.RenameColumn(
                name: "AggregateId",
                table: "AppointmentSchedulingEvents",
                newName: "ScheduleAppointmentByPatientId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentSchedulingEvents_AggregateId",
                table: "AppointmentSchedulingEvents",
                newName: "IX_AppointmentSchedulingEvents_ScheduleAppointmentByPatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentSchedulingEvents_ScheduleAppointmentByPatients_ScheduleAppointmentByPatientId",
                table: "AppointmentSchedulingEvents",
                column: "ScheduleAppointmentByPatientId",
                principalTable: "ScheduleAppointmentByPatients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
