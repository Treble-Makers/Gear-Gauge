using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GearGauge.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "commentId",
                table: "MusicItems",
                newName: "CommentId");

            migrationBuilder.RenameColumn(
                name: "categoryId",
                table: "MusicItems",
                newName: "CategoryId");

            migrationBuilder.AddColumn<bool>(
                name: "HaveOne",
                table: "MusicItems",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MarketValue",
                table: "MusicItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "WantOne",
                table: "MusicItems",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HaveOne",
                table: "MusicItems");

            migrationBuilder.DropColumn(
                name: "MarketValue",
                table: "MusicItems");

            migrationBuilder.DropColumn(
                name: "WantOne",
                table: "MusicItems");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "MusicItems",
                newName: "commentId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "MusicItems",
                newName: "categoryId");
        }
    }
}
