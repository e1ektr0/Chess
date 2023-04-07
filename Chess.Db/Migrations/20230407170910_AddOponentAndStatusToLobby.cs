using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chess.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddOponentAndStatusToLobby : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OpponentUserId",
                table: "Lobbies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Lobbies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Lobbies_OpponentUserId",
                table: "Lobbies",
                column: "OpponentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lobbies_AspNetUsers_OpponentUserId",
                table: "Lobbies",
                column: "OpponentUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lobbies_AspNetUsers_OpponentUserId",
                table: "Lobbies");

            migrationBuilder.DropIndex(
                name: "IX_Lobbies_OpponentUserId",
                table: "Lobbies");

            migrationBuilder.DropColumn(
                name: "OpponentUserId",
                table: "Lobbies");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Lobbies");
        }
    }
}
