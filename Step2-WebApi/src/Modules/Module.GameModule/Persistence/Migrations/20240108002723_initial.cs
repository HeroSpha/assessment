using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Module.GameModule.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "RouletteWheel");

            migrationBuilder.CreateTable(
                name: "RouletteWheelSessions",
                schema: "RouletteWheel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    SelectedNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    PocketColor = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouletteWheelSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SessionBets",
                schema: "RouletteWheel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    WinAmount = table.Column<decimal>(type: "TEXT", nullable: true),
                    BetType = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    SelectedBet = table.Column<string>(type: "TEXT", nullable: false),
                    IsSelected = table.Column<bool>(type: "INTEGER", nullable: false),
                    RouletteSessionId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionBets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionBets_RouletteWheelSessions_RouletteSessionId",
                        column: x => x.RouletteSessionId,
                        principalSchema: "RouletteWheel",
                        principalTable: "RouletteWheelSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionBets_RouletteSessionId",
                schema: "RouletteWheel",
                table: "SessionBets",
                column: "RouletteSessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessionBets",
                schema: "RouletteWheel");

            migrationBuilder.DropTable(
                name: "RouletteWheelSessions",
                schema: "RouletteWheel");
        }
    }
}
