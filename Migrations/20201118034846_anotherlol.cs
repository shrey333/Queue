using Microsoft.EntityFrameworkCore.Migrations;

namespace Queue.Migrations
{
    public partial class anotherlol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserPhoneNumber",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MerchantPhoneNumber",
                table: "merchants",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserPhoneNumber",
                table: "users");

            migrationBuilder.DropColumn(
                name: "MerchantPhoneNumber",
                table: "merchants");
        }
    }
}
