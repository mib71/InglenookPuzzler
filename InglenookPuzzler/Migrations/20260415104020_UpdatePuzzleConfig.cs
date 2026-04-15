using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InglenookPuzzler.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePuzzleConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Config_WagonCount",
                table: "PuzzleSessions",
                newName: "Config_TotalWagons");

            migrationBuilder.AddColumn<int>(
                name: "Config_GoalWagons",
                table: "PuzzleSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Config_GoalWagons",
                table: "PuzzleSessions");

            migrationBuilder.RenameColumn(
                name: "Config_TotalWagons",
                table: "PuzzleSessions",
                newName: "Config_WagonCount");
        }
    }
}
