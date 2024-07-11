using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GearGauge.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GearInventories_GearInventories_GearInventoryId",
                table: "GearInventories");

            migrationBuilder.DropIndex(
                name: "IX_GearInventories_GearInventoryId",
                table: "GearInventories");

            migrationBuilder.DropColumn(
                name: "GearInventoryId",
                table: "GearInventories");

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GearInventoryTags",
                columns: table => new
                {
                    GearInventoriesId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearInventoryTags", x => new { x.GearInventoriesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_GearInventoryTags_GearInventories_GearInventoriesId",
                        column: x => x.GearInventoriesId,
                        principalTable: "GearInventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GearInventoryTags_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GearInventoryTags_TagsId",
                table: "GearInventoryTags",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GearInventoryTags");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.AddColumn<int>(
                name: "GearInventoryId",
                table: "GearInventories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GearInventories_GearInventoryId",
                table: "GearInventories",
                column: "GearInventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_GearInventories_GearInventories_GearInventoryId",
                table: "GearInventories",
                column: "GearInventoryId",
                principalTable: "GearInventories",
                principalColumn: "Id");
        }
    }
}
