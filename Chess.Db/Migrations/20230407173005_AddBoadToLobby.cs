using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chess.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddBoadToLobby : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Board",
                table: "Lobbies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OneUserId",
                table: "Lobbies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Lobbies",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZeroUserId",
                table: "Lobbies",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Board",
                table: "Lobbies");

            migrationBuilder.DropColumn(
                name: "OneUserId",
                table: "Lobbies");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Lobbies");

            migrationBuilder.DropColumn(
                name: "ZeroUserId",
                table: "Lobbies");
        }
    }
}
