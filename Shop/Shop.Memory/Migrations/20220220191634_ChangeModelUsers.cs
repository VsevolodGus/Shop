using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Memory.Migrations
{
    public partial class ChangeModelUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductIdAmount",
                table: "SalesDatas",
                newName: "ProductPrice");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductPrice",
                table: "SalesDatas",
                newName: "ProductIdAmount");
        }
    }
}
