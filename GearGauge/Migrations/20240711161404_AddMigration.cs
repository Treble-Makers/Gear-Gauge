using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GearGauge.Migrations
{
    /// <inheritdoc />
    public partial class AddMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GearInventoryId",
                table: "GearInventory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GearInventoryId",
                table: "GearInventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GearInventory_GearInventoryId",
                table: "GearInventory",
                column: "GearInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GearInventories_GearInventoryId",
                table: "GearInventories",
                column: "GearInventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_GearInventories_GearInventories_GearInventoryId",
                table: "GearInventories",
                column: "GearInventoryId",
                principalTable: "GearInventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GearInventory_GearInventory_GearInventoryId",
                table: "GearInventory",
                column: "GearInventoryId",
                principalTable: "GearInventory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GearInventories_GearInventories_GearInventoryId",
                table: "GearInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_GearInventory_GearInventory_GearInventoryId",
                table: "GearInventory");

            migrationBuilder.DropIndex(
                name: "IX_GearInventory_GearInventoryId",
                table: "GearInventory");

            migrationBuilder.DropIndex(
                name: "IX_GearInventories_GearInventoryId",
                table: "GearInventories");

            migrationBuilder.DropColumn(
                name: "GearInventoryId",
                table: "GearInventory");

            migrationBuilder.DropColumn(
                name: "GearInventoryId",
                table: "GearInventories");
        }
    }
}
