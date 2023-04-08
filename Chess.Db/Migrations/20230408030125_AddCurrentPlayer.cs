using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chess.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrentPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentPlayer",
                table: "Lobbies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CurrentPlayerId",
                table: "Lobbies",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPlayer",
                table: "Lobbies");

            migrationBuilder.DropColumn(
                name: "CurrentPlayerId",
                table: "Lobbies");
        }
    }
}
