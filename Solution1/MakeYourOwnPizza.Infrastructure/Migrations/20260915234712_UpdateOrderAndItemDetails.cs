using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MakeYourOwnPizza.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderAndItemDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "size",
                table: "OrderItem",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "apartment",
                table: "Order",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Order",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "district",
                table: "Order",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "floor",
                table: "Order",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "formattedAddress",
                table: "Order",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "note",
                table: "Order",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "street",
                table: "Order",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "size",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "apartment",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "city",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "district",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "floor",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "formattedAddress",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "note",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "street",
                table: "Order");
        }
    }
}
