using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BakeryManagementPOSWebApp.Migrations
{
    /// <inheritdoc />
    public partial class OrderItemUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "RowPrice",
                table: "OrderItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowPrice",
                table: "OrderItems");
        }
    }
}
