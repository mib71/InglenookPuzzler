using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InglenookPuzzler.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Eras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PuzzleSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StartState = table.Column<string>(type: "TEXT", nullable: false),
                    GoalState = table.Column<string>(type: "TEXT", nullable: false),
                    MoveCount = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Config_TrackACapacity = table.Column<int>(type: "INTEGER", nullable: false),
                    Config_TrackBCapacity = table.Column<int>(type: "INTEGER", nullable: false),
                    Config_TrackCCapacity = table.Column<int>(type: "INTEGER", nullable: false),
                    Config_HeadshuntCapacity = table.Column<int>(type: "INTEGER", nullable: false),
                    Config_TotalWagons = table.Column<int>(type: "INTEGER", nullable: false),
                    Config_GoalWagons = table.Column<int>(type: "INTEGER", nullable: false),
                    Config_TimedMode = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuzzleSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WagonTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WagonTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wagons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    WagonTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    EraId = table.Column<int>(type: "INTEGER", nullable: true),
                    RollingStockNumber = table.Column<string>(type: "TEXT", maxLength: 15, nullable: true),
                    Color = table.Column<string>(type: "TEXT", maxLength: 15, nullable: true),
                    ImagePath = table.Column<string>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wagons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wagons_Eras_EraId",
                        column: x => x.EraId,
                        principalTable: "Eras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Wagons_WagonTypes_WagonTypeId",
                        column: x => x.WagonTypeId,
                        principalTable: "WagonTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wagons_EraId",
                table: "Wagons",
                column: "EraId");

            migrationBuilder.CreateIndex(
                name: "IX_Wagons_WagonTypeId",
                table: "Wagons",
                column: "WagonTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PuzzleSessions");

            migrationBuilder.DropTable(
                name: "Wagons");

            migrationBuilder.DropTable(
                name: "Eras");

            migrationBuilder.DropTable(
                name: "WagonTypes");
        }
    }
}
