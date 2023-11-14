using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PastryShop.Dal.Migrations
{
    /// <inheritdoc />
    public partial class LineItemChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineItem_Orders_LineItemId",
                table: "LineItem");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "LineItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "LineItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_LineItem_OrderId",
                table: "LineItem",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_LineItem_Orders_OrderId",
                table: "LineItem",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineItem_Orders_OrderId",
                table: "LineItem");

            migrationBuilder.DropIndex(
                name: "IX_LineItem_OrderId",
                table: "LineItem");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "LineItem");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "LineItem");

            migrationBuilder.AddForeignKey(
                name: "FK_LineItem_Orders_LineItemId",
                table: "LineItem",
                column: "LineItemId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
