using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GearGauge.Migrations
{
    /// <inheritdoc />
    public partial class trying : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_GearInventory_GearInventoryId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_GearInventory_GearInventoryId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Gear_GearInventory_GearInventoryId",
                table: "Gear");

            migrationBuilder.DropForeignKey(
                name: "FK_GearInventories_GearInventories_GearInventoryId",
                table: "GearInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_GearInventoryTag_GearInventory_GearInventoriesId",
                table: "GearInventoryTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_GearInventories_GearInventoryId",
                table: "Tags");

            migrationBuilder.DropTable(
                name: "GearInventory");

            migrationBuilder.DropIndex(
                name: "IX_Tags_GearInventoryId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "GearInventoryId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "GearInventories",
                keyColumn: "Title",
                keyValue: null,
                column: "Title",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "GearInventories",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "GearInventoryId",
                table: "GearInventories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "GearInventories",
                keyColumn: "Description",
                keyValue: null,
                column: "Description",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "GearInventories",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "GearInventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "GearInventories",
                type: "longblob",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_GearInventories_CommentId",
                table: "GearInventories",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_GearInventories_GearInventoryId",
                table: "Comments",
                column: "GearInventoryId",
                principalTable: "GearInventories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_GearInventories_GearInventoryId",
                table: "Favorites",
                column: "GearInventoryId",
                principalTable: "GearInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gear_GearInventories_GearInventoryId",
                table: "Gear",
                column: "GearInventoryId",
                principalTable: "GearInventories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GearInventories_Comments_CommentId",
                table: "GearInventories",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GearInventories_GearInventories_GearInventoryId",
                table: "GearInventories",
                column: "GearInventoryId",
                principalTable: "GearInventories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GearInventoryTag_GearInventories_GearInventoriesId",
                table: "GearInventoryTag",
                column: "GearInventoriesId",
                principalTable: "GearInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_GearInventories_GearInventoryId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_GearInventories_GearInventoryId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Gear_GearInventories_GearInventoryId",
                table: "Gear");

            migrationBuilder.DropForeignKey(
                name: "FK_GearInventories_Comments_CommentId",
                table: "GearInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_GearInventories_GearInventories_GearInventoryId",
                table: "GearInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_GearInventoryTag_GearInventories_GearInventoriesId",
                table: "GearInventoryTag");

            migrationBuilder.DropIndex(
                name: "IX_GearInventories_CommentId",
                table: "GearInventories");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "GearInventories");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "GearInventories");

            migrationBuilder.AddColumn<int>(
                name: "GearInventoryId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "GearInventories",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "GearInventoryId",
                table: "GearInventories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "GearInventories",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GearInventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GearInventoryId = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<byte[]>(type: "longblob", nullable: false),
                    ImagePath = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MarketValue = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearInventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GearInventory_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GearInventory_GearInventory_GearInventoryId",
                        column: x => x.GearInventoryId,
                        principalTable: "GearInventory",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_GearInventoryId",
                table: "Tags",
                column: "GearInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GearInventory_CommentId",
                table: "GearInventory",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_GearInventory_GearInventoryId",
                table: "GearInventory",
                column: "GearInventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_GearInventory_GearInventoryId",
                table: "Comments",
                column: "GearInventoryId",
                principalTable: "GearInventory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_GearInventory_GearInventoryId",
                table: "Favorites",
                column: "GearInventoryId",
                principalTable: "GearInventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gear_GearInventory_GearInventoryId",
                table: "Gear",
                column: "GearInventoryId",
                principalTable: "GearInventory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GearInventories_GearInventories_GearInventoryId",
                table: "GearInventories",
                column: "GearInventoryId",
                principalTable: "GearInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GearInventoryTag_GearInventory_GearInventoriesId",
                table: "GearInventoryTag",
                column: "GearInventoriesId",
                principalTable: "GearInventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_GearInventories_GearInventoryId",
                table: "Tags",
                column: "GearInventoryId",
                principalTable: "GearInventories",
                principalColumn: "Id");
        }
    }
}
