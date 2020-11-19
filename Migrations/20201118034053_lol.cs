using Microsoft.EntityFrameworkCore.Migrations;

namespace Queue.Migrations
{
    public partial class lol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MerchantAddress1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MerchantAddress2",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MerchantFirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MerchantLastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MerchantLatitude",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MerchantLongitude",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MerchantPinCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MerchantShopName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserFirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserLastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserLatitude",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserLongitude",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "merchants",
                columns: table => new
                {
                    MerchantEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MerchantFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantLatitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantLongitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantShopName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantAddress1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantAddress2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantPinCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_merchants", x => x.MerchantEmail);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserLatitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserLongitude = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserEmail);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "merchants");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MerchantAddress1",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MerchantAddress2",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MerchantFirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MerchantLastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MerchantLatitude",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MerchantLongitude",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MerchantPinCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MerchantShopName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserFirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserLastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserLatitude",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserLongitude",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
