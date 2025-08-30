using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballScoreApp.Migrations
{
    /// <inheritdoc />
    public partial class followteam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTeam",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    TeamId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTeam", x => new { x.UserId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_UserTeam_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTeam");
        }
    }
}
