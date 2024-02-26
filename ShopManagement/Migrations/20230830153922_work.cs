using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopManagement.Migrations
{
    public partial class work : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkTypeName",
                table: "WorkType",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "WorkTimeId",
                table: "WorkType",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "WorkType",
                newName: "WorkTypeName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "WorkType",
                newName: "WorkTimeId");
        }
    }
}
