using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Memory.Migrations
{
    public partial class AddSalePoint_With_ConnectionUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "SalePoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalePoints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProvidedProducts",
                columns: table => new
                {
                    PKID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalePointId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvidedProducts", x => x.PKID);
                    table.ForeignKey(
                        name: "FK_ProvidedProducts_SalePoints_SalePointId",
                        column: x => x.SalePointId,
                        principalTable: "SalePoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProvidedProducts_SalePointId",
                table: "ProvidedProducts",
                column: "SalePointId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProvidedProducts");

            migrationBuilder.DropTable(
                name: "SalePoints");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
