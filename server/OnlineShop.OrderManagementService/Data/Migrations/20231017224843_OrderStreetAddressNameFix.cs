using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.OrderManagementService.Data.Migrations
{
    public partial class OrderStreetAddressNameFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetAddresss",
                table: "Orders",
                newName: "StreetAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "Orders",
                newName: "StreetAddresss");
        }
    }
}
