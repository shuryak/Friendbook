using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Friendbook.DataAccess.PostgreSql.Migrations
{
    public partial class UniqueRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserSessions_RefreshToken",
                table: "UserSessions",
                column: "RefreshToken");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserSessions_RefreshToken",
                table: "UserSessions");
        }
    }
}
