using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InglenookPuzzler.Migrations
{
    /// <inheritdoc />
    public partial class AddHeadshunt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Config_HeadshuntCapacity",
                table: "PuzzleSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Config_HeadshuntCapacity",
                table: "PuzzleSessions");
        }
    }
}
