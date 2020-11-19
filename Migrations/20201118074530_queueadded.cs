using Microsoft.EntityFrameworkCore.Migrations;

namespace Queue.Migrations
{
    public partial class queueadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "queueModels",
                columns: table => new
                {
                    UserEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MerchantEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserEmail1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MerchantEmail1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_queueModels", x => new { x.UserEmail, x.MerchantEmail });
                    table.ForeignKey(
                        name: "FK_queueModels_merchants_MerchantEmail1",
                        column: x => x.MerchantEmail1,
                        principalTable: "merchants",
                        principalColumn: "MerchantEmail",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_queueModels_users_UserEmail1",
                        column: x => x.UserEmail1,
                        principalTable: "users",
                        principalColumn: "UserEmail",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_queueModels_MerchantEmail1",
                table: "queueModels",
                column: "MerchantEmail1");

            migrationBuilder.CreateIndex(
                name: "IX_queueModels_UserEmail1",
                table: "queueModels",
                column: "UserEmail1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "queueModels");
        }
    }
}
