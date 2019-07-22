using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CardsAPI.Migrations
{
    public partial class intialcommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    game_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.game_id);
                });

            migrationBuilder.CreateTable(
                name: "Decks",
                columns: table => new
                {
                    deck_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    game_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decks", x => x.deck_id);
                    table.ForeignKey(
                        name: "FK_Decks_Games_game_id",
                        column: x => x.game_id,
                        principalTable: "Games",
                        principalColumn: "game_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    player_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    value = table.Column<int>(nullable: false),
                    game_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.player_id);
                    table.ForeignKey(
                        name: "FK_Players_Games_game_id",
                        column: x => x.game_id,
                        principalTable: "Games",
                        principalColumn: "game_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    card_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    value = table.Column<int>(nullable: false),
                    position = table.Column<int>(nullable: false),
                    suit = table.Column<int>(nullable: false),
                    face = table.Column<int>(nullable: false),
                    player_id = table.Column<int>(nullable: true),
                    deck_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.card_id);
                    table.ForeignKey(
                        name: "FK_Cards_Decks_deck_id",
                        column: x => x.deck_id,
                        principalTable: "Decks",
                        principalColumn: "deck_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cards_Players_player_id",
                        column: x => x.player_id,
                        principalTable: "Players",
                        principalColumn: "player_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_deck_id",
                table: "Cards",
                column: "deck_id");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_player_id",
                table: "Cards",
                column: "player_id");

            migrationBuilder.CreateIndex(
                name: "IX_Decks_game_id",
                table: "Decks",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "IX_Players_game_id",
                table: "Players",
                column: "game_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Decks");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
