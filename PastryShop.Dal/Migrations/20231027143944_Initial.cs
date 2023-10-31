using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PastryShop.Dal.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShipmentTypes",
                columns: table => new
                {
                    ShipmentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentTypes", x => x.ShipmentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ShippingAddress",
                columns: table => new
                {
                    ShippingAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingAddress", x => x.ShippingAddressId);
                });

            migrationBuilder.CreateTable(
                name: "ShippingAddressOrder",
                columns: table => new
                {
                    ShippingAddressOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingAddressOrder", x => x.ShippingAddressOrderId);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IndentityId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasicInfo_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasicInfo_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasicInfo_EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasicInfo_Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasicInfo_ShippingAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.UserProfileId);
                    table.ForeignKey(
                        name: "FK_UserProfiles_ShippingAddress_BasicInfo_ShippingAddressId",
                        column: x => x.BasicInfo_ShippingAddressId,
                        principalTable: "ShippingAddress",
                        principalColumn: "ShippingAddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ShipmentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShippingAddressOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserInstructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_ShipmentTypes_ShipmentTypeId",
                        column: x => x.ShipmentTypeId,
                        principalTable: "ShipmentTypes",
                        principalColumn: "ShipmentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_ShippingAddressOrder_ShippingAddressOrderId",
                        column: x => x.ShippingAddressOrderId,
                        principalTable: "ShippingAddressOrder",
                        principalColumn: "ShippingAddressOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "UserProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShipmentTypeId",
                table: "Orders",
                column: "ShipmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingAddressOrderId",
                table: "Orders",
                column: "ShippingAddressOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserProfileId",
                table: "Orders",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderId",
                table: "Products",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_BasicInfo_ShippingAddressId",
                table: "UserProfiles",
                column: "BasicInfo_ShippingAddressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ShipmentTypes");

            migrationBuilder.DropTable(
                name: "ShippingAddressOrder");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "ShippingAddress");
        }
    }
}
