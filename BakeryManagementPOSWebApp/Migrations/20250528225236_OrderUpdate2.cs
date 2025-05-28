using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BakeryManagementPOSWebApp.Migrations
{
    /// <inheritdoc />
    public partial class OrderUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_ProcessedById",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProcessedById",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProcessedById",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentType",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeID",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EmployeeID",
                table: "Orders",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_EmployeeID",
                table: "Orders",
                column: "EmployeeID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_EmployeeID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_EmployeeID",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentType",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProcessedById",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProcessedById",
                table: "Orders",
                column: "ProcessedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_ProcessedById",
                table: "Orders",
                column: "ProcessedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
