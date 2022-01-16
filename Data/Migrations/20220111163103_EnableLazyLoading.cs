using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class EnableLazyLoading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_PaymentDetails_PaymentDetailsId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_PaymentDetailsId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "PaymentDetailsId",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "PaymentDetails",
                newName: "OrderDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetails_OrderDetailsId",
                table: "PaymentDetails",
                column: "OrderDetailsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDetails_OrderDetails_OrderDetailsId",
                table: "PaymentDetails",
                column: "OrderDetailsId",
                principalTable: "OrderDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetails_OrderDetails_OrderDetailsId",
                table: "PaymentDetails");

            migrationBuilder.DropIndex(
                name: "IX_PaymentDetails_OrderDetailsId",
                table: "PaymentDetails");

            migrationBuilder.RenameColumn(
                name: "OrderDetailsId",
                table: "PaymentDetails",
                newName: "OrderId");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "OrderItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentDetailsId",
                table: "OrderDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_PaymentDetailsId",
                table: "OrderDetails",
                column: "PaymentDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_PaymentDetails_PaymentDetailsId",
                table: "OrderDetails",
                column: "PaymentDetailsId",
                principalTable: "PaymentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
