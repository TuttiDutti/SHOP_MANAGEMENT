using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopManagement.Migrations
{
    public partial class newabsence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "From",
                table: "AbsenceViewModel");

            migrationBuilder.RenameColumn(
                name: "To",
                table: "AbsenceViewModel",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Absence",
                newName: "DateTo");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateFrom",
                table: "Absence",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateFrom",
                table: "Absence");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "AbsenceViewModel",
                newName: "To");

            migrationBuilder.RenameColumn(
                name: "DateTo",
                table: "Absence",
                newName: "Date");

            migrationBuilder.AddColumn<DateTime>(
                name: "From",
                table: "AbsenceViewModel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
