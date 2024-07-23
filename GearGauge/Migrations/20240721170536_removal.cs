using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GearGauge.Migrations
{
    /// <inheritdoc />
    public partial class removal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactUs_AspNetUsers_UserId",
                table: "ContactUs");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactUs_ContactUs_ContactUsId",
                table: "ContactUs");

            migrationBuilder.DropIndex(
                name: "IX_ContactUs_ContactUsId",
                table: "ContactUs");

            migrationBuilder.DropIndex(
                name: "IX_ContactUs_UserId",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "ContactUsId",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ContactUs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContactUsId",
                table: "ContactUs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ContactUs",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_ContactUsId",
                table: "ContactUs",
                column: "ContactUsId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_UserId",
                table: "ContactUs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactUs_AspNetUsers_UserId",
                table: "ContactUs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactUs_ContactUs_ContactUsId",
                table: "ContactUs",
                column: "ContactUsId",
                principalTable: "ContactUs",
                principalColumn: "Id");
        }
    }
}
