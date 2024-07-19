using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GearGauge.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserIdTypeInFavorites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Gear_GearId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactUs_AspNetUsers_UserId",
                table: "ContactUs");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_AspNetUsers_Id",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Gear_GearInventories_GearInventoryId",
                table: "Gear");

            migrationBuilder.DropForeignKey(
                name: "FK_Watchlists_Gear_GearId",
                table: "Watchlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gear",
                table: "Gear");

            migrationBuilder.RenameTable(
                name: "Gear",
                newName: "Gears");

            migrationBuilder.RenameIndex(
                name: "IX_Gear_GearInventoryId",
                table: "Gears",
                newName: "IX_Gears_GearInventoryId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Favorites",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Favorites",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ContactUs",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gears",
                table: "Gears",
                column: "GearId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_GearId",
                table: "Favorites",
                column: "GearId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Gears_GearId",
                table: "Comments",
                column: "GearId",
                principalTable: "Gears",
                principalColumn: "GearId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactUs_AspNetUsers_UserId",
                table: "ContactUs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_AspNetUsers_UserId",
                table: "Favorites",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Gears_GearId",
                table: "Favorites",
                column: "GearId",
                principalTable: "Gears",
                principalColumn: "GearId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gears_GearInventories_GearInventoryId",
                table: "Gears",
                column: "GearInventoryId",
                principalTable: "GearInventories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Watchlists_Gears_GearId",
                table: "Watchlists",
                column: "GearId",
                principalTable: "Gears",
                principalColumn: "GearId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Gears_GearId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactUs_AspNetUsers_UserId",
                table: "ContactUs");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_AspNetUsers_UserId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Gears_GearId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Gears_GearInventories_GearInventoryId",
                table: "Gears");

            migrationBuilder.DropForeignKey(
                name: "FK_Watchlists_Gears_GearId",
                table: "Watchlists");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_GearId",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gears",
                table: "Gears");

            migrationBuilder.RenameTable(
                name: "Gears",
                newName: "Gear");

            migrationBuilder.RenameIndex(
                name: "IX_Gears_GearInventoryId",
                table: "Gear",
                newName: "IX_Gear_GearInventoryId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Favorites",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Favorites",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.UpdateData(
                table: "ContactUs",
                keyColumn: "UserId",
                keyValue: null,
                column: "UserId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ContactUs",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gear",
                table: "Gear",
                column: "GearId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Gear_GearId",
                table: "Comments",
                column: "GearId",
                principalTable: "Gear",
                principalColumn: "GearId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactUs_AspNetUsers_UserId",
                table: "ContactUs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_AspNetUsers_Id",
                table: "Favorites",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gear_GearInventories_GearInventoryId",
                table: "Gear",
                column: "GearInventoryId",
                principalTable: "GearInventories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Watchlists_Gear_GearId",
                table: "Watchlists",
                column: "GearId",
                principalTable: "Gear",
                principalColumn: "GearId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
