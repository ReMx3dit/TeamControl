using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamControlLib.Migrations
{
    public partial class IC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StamNummer = table.Column<int>(nullable: false),
                    Naam = table.Column<string>(nullable: true),
                    Bijnaam = table.Column<string>(nullable: true),
                    Trainer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpelerId = table.Column<int>(nullable: false),
                    OudTeamId = table.Column<int>(nullable: false),
                    NieuwTeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spelers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(nullable: true),
                    Rugnummer = table.Column<int>(nullable: false),
                    Waarde = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false),
                    TeamStamnummer = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spelers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spelers_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Spelers_Rugnummer",
                table: "Spelers",
                column: "Rugnummer",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Spelers_TeamId",
                table: "Spelers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_StamNummer",
                table: "Teams",
                column: "StamNummer",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Spelers");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
