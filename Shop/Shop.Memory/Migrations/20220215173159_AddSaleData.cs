using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Memory.Migrations
{
    public partial class AddSaleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    PKID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalePointId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.PKID);
                    table.ForeignKey(
                        name: "FK_Sales_SalePoints_SalePointId",
                        column: x => x.SalePointId,
                        principalTable: "SalePoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesDatas",
                columns: table => new
                {
                    PKID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductQuantity = table.Column<long>(type: "bigint", nullable: false),
                    ProductIdAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesDatas", x => x.PKID);
                    table.ForeignKey(
                        name: "FK_SalesDatas_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesDatas_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "PKID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProvidedProducts_ProductId",
                table: "ProvidedProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_SalePointId",
                table: "Sales",
                column: "SalePointId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_UserId",
                table: "Sales",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDatas_ProductId",
                table: "SalesDatas",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDatas_SaleId",
                table: "SalesDatas",
                column: "SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProvidedProducts_Products_ProductId",
                table: "ProvidedProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvidedProducts_Products_ProductId",
                table: "ProvidedProducts");

            migrationBuilder.DropTable(
                name: "SalesDatas");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_ProvidedProducts_ProductId",
                table: "ProvidedProducts");
        }
    }
}
