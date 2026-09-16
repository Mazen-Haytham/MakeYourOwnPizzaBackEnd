using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MakeYourOwnPizza.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDriverIdToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "driverId",
                table: "Order",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "imageUrl",
                table: "Ingredients",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "stock",
                table: "Ingredients",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Order_driverId",
                table: "Order",
                column: "driverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_driverId",
                table: "Order",
                column: "driverId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_driverId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_driverId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "driverId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "imageUrl",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "stock",
                table: "Ingredients");
        }
    }
}
