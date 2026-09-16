using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MakeYourOwnPizza.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateDriverEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_driverId",
                table: "Order");

            migrationBuilder.CreateTable(
                name: "Driver",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Zone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Driver", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Driver_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Driver_UserId",
                table: "Driver",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Driver_driverId",
                table: "Order",
                column: "driverId",
                principalTable: "Driver",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Driver_driverId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "Driver");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_driverId",
                table: "Order",
                column: "driverId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
