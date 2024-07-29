using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GearGauge.Migrations
{
    /// <inheritdoc />
    public partial class anotherMigr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_AspNetUsers_UserId1",
                table: "Favorites");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "GearInventories",
                type: "longblob",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "Favorites",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "GearInventoryId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GearInventoryId",
                table: "AspNetUsers",
                column: "GearInventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_GearInventories_GearInventoryId",
                table: "AspNetUsers",
                column: "GearInventoryId",
                principalTable: "GearInventories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_AspNetUsers_UserId1",
                table: "Favorites",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_GearInventories_GearInventoryId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_AspNetUsers_UserId1",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GearInventoryId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "GearInventories");

            migrationBuilder.DropColumn(
                name: "GearInventoryId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Favorites",
                keyColumn: "UserId1",
                keyValue: null,
                column: "UserId1",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "Favorites",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_AspNetUsers_UserId1",
                table: "Favorites",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
