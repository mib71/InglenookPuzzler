using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InglenookPuzzler.Migrations
{
    /// <inheritdoc />
    public partial class AddRollingStockNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RollingStockNumber",
                table: "Wagons",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RollingStockNumber",
                table: "Wagons");
        }
    }
}
