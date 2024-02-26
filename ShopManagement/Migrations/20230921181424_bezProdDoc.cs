using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopManagement.Migrations
{
    public partial class bezProdDoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Products_ProductId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_ProductId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Documents");

            migrationBuilder.CreateTable(
                name: "ProductViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NetPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrossPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VAT = table.Column<int>(type: "int", nullable: false),
                    Offer = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainPhoto = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductViewModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductViewModel");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ProductId",
                table: "Documents",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Products_ProductId",
                table: "Documents",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
