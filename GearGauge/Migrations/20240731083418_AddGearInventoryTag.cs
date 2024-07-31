using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GearGauge.Migrations
{
    /// <inheritdoc />
    public partial class AddGearInventoryTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GearInventoryTag");

            migrationBuilder.AddColumn<int>(
                name: "GearInventoryId",
                table: "Watchlists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GearInventoryTags",
                columns: table => new
                {
                    GearInventoryId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearInventoryTags", x => new { x.GearInventoryId, x.TagId });
                    table.ForeignKey(
                        name: "FK_GearInventoryTags_GearInventories_GearInventoryId",
                        column: x => x.GearInventoryId,
                        principalTable: "GearInventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GearInventoryTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Watchlists_GearInventoryId",
                table: "Watchlists",
                column: "GearInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GearInventoryTags_TagId",
                table: "GearInventoryTags",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Watchlists_GearInventories_GearInventoryId",
                table: "Watchlists",
                column: "GearInventoryId",
                principalTable: "GearInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Watchlists_GearInventories_GearInventoryId",
                table: "Watchlists");

            migrationBuilder.DropTable(
                name: "GearInventoryTags");

            migrationBuilder.DropIndex(
                name: "IX_Watchlists_GearInventoryId",
                table: "Watchlists");

            migrationBuilder.DropColumn(
                name: "GearInventoryId",
                table: "Watchlists");

            migrationBuilder.CreateTable(
                name: "GearInventoryTag",
                columns: table => new
                {
                    GearInventoriesId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearInventoryTag", x => new { x.GearInventoriesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_GearInventoryTag_GearInventories_GearInventoriesId",
                        column: x => x.GearInventoriesId,
                        principalTable: "GearInventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GearInventoryTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GearInventoryTag_TagsId",
                table: "GearInventoryTag",
                column: "TagsId");
        }
    }
}
