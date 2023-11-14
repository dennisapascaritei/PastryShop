using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PastryShop.Dal.Migrations
{
    /// <inheritdoc />
    public partial class LineItemChanges2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LineItem_ProductId",
                table: "LineItem",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_LineItem_Products_ProductId",
                table: "LineItem",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineItem_Products_ProductId",
                table: "LineItem");

            migrationBuilder.DropIndex(
                name: "IX_LineItem_ProductId",
                table: "LineItem");
        }
    }
}
