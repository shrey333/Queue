using Microsoft.EntityFrameworkCore.Migrations;

namespace Queue.Migrations
{
    public partial class mygod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_queueModels_merchants_MerchantEmail1",
                table: "queueModels");

            migrationBuilder.DropForeignKey(
                name: "FK_queueModels_users_UserEmail1",
                table: "queueModels");

            migrationBuilder.DropIndex(
                name: "IX_queueModels_MerchantEmail1",
                table: "queueModels");

            migrationBuilder.DropIndex(
                name: "IX_queueModels_UserEmail1",
                table: "queueModels");

            migrationBuilder.DropColumn(
                name: "MerchantEmail1",
                table: "queueModels");

            migrationBuilder.DropColumn(
                name: "UserEmail1",
                table: "queueModels");

            migrationBuilder.RenameColumn(
                name: "MerchantEmail",
                table: "queueModels",
                newName: "MerchantId");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "queueModels",
                newName: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_queueModels_MerchantId",
                table: "queueModels",
                column: "MerchantId");

            migrationBuilder.AddForeignKey(
                name: "FK_queueModels_merchants_MerchantId",
                table: "queueModels",
                column: "MerchantId",
                principalTable: "merchants",
                principalColumn: "MerchantEmail",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_queueModels_users_UserId",
                table: "queueModels",
                column: "UserId",
                principalTable: "users",
                principalColumn: "UserEmail",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_queueModels_merchants_MerchantId",
                table: "queueModels");

            migrationBuilder.DropForeignKey(
                name: "FK_queueModels_users_UserId",
                table: "queueModels");

            migrationBuilder.DropIndex(
                name: "IX_queueModels_MerchantId",
                table: "queueModels");

            migrationBuilder.RenameColumn(
                name: "MerchantId",
                table: "queueModels",
                newName: "MerchantEmail");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "queueModels",
                newName: "UserEmail");

            migrationBuilder.AddColumn<string>(
                name: "MerchantEmail1",
                table: "queueModels",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail1",
                table: "queueModels",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_queueModels_MerchantEmail1",
                table: "queueModels",
                column: "MerchantEmail1");

            migrationBuilder.CreateIndex(
                name: "IX_queueModels_UserEmail1",
                table: "queueModels",
                column: "UserEmail1");

            migrationBuilder.AddForeignKey(
                name: "FK_queueModels_merchants_MerchantEmail1",
                table: "queueModels",
                column: "MerchantEmail1",
                principalTable: "merchants",
                principalColumn: "MerchantEmail",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_queueModels_users_UserEmail1",
                table: "queueModels",
                column: "UserEmail1",
                principalTable: "users",
                principalColumn: "UserEmail",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
