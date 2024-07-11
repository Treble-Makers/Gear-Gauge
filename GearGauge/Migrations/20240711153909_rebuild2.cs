using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GearGauge.Migrations
{
    /// <inheritdoc />
    public partial class rebuild2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GearInventoryTags_GearInventories_GearInventoriesId",
                table: "GearInventoryTags");

            migrationBuilder.DropForeignKey(
                name: "FK_GearInventoryTags_Tags_TagsId",
                table: "GearInventoryTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GearInventoryTags",
                table: "GearInventoryTags");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "GearInventories");

            migrationBuilder.RenameTable(
                name: "GearInventoryTags",
                newName: "GearInventoryTag");

            migrationBuilder.RenameIndex(
                name: "IX_GearInventoryTags_TagsId",
                table: "GearInventoryTag",
                newName: "IX_GearInventoryTag_TagsId");

            migrationBuilder.AddColumn<int>(
                name: "GearInventoryId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "GearInventories",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GearInventoryTag",
                table: "GearInventoryTag",
                columns: new[] { "GearInventoriesId", "TagsId" });

            migrationBuilder.CreateTable(
                name: "GearInventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MarketValue = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearInventory", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_GearInventoryId",
                table: "Tags",
                column: "GearInventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_GearInventoryTag_GearInventory_GearInventoriesId",
                table: "GearInventoryTag",
                column: "GearInventoriesId",
                principalTable: "GearInventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GearInventoryTag_Tags_TagsId",
                table: "GearInventoryTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_GearInventories_GearInventoryId",
                table: "Tags",
                column: "GearInventoryId",
                principalTable: "GearInventories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GearInventoryTag_GearInventory_GearInventoriesId",
                table: "GearInventoryTag");

            migrationBuilder.DropForeignKey(
                name: "FK_GearInventoryTag_Tags_TagsId",
                table: "GearInventoryTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_GearInventories_GearInventoryId",
                table: "Tags");

            migrationBuilder.DropTable(
                name: "GearInventory");

            migrationBuilder.DropIndex(
                name: "IX_Tags_GearInventoryId",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GearInventoryTag",
                table: "GearInventoryTag");

            migrationBuilder.DropColumn(
                name: "GearInventoryId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "GearInventories");

            migrationBuilder.RenameTable(
                name: "GearInventoryTag",
                newName: "GearInventoryTags");

            migrationBuilder.RenameIndex(
                name: "IX_GearInventoryTag_TagsId",
                table: "GearInventoryTags",
                newName: "IX_GearInventoryTags_TagsId");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "GearInventories",
                type: "longblob",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GearInventoryTags",
                table: "GearInventoryTags",
                columns: new[] { "GearInventoriesId", "TagsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GearInventoryTags_GearInventories_GearInventoriesId",
                table: "GearInventoryTags",
                column: "GearInventoriesId",
                principalTable: "GearInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GearInventoryTags_Tags_TagsId",
                table: "GearInventoryTags",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
