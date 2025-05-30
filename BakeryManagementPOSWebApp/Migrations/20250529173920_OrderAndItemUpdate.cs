using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BakeryManagementPOSWebApp.Migrations
{
    /// <inheritdoc />
    public partial class OrderAndItemUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "Orders",
                newName: "TotalAmount");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatePickedUp",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "DiscountPercent",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "SumOfItems",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductPrice",
                table: "OrderItems",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatePickedUp",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SumOfItems",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Orders",
                newName: "Discount");
        }
    }
}
