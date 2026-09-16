using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MakeYourOwnPizza.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderItemDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "OrderItem",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "OrderItem");
        }
    }
}
