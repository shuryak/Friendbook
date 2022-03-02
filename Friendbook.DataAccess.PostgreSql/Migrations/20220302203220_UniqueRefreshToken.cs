using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Friendbook.DataAccess.PostgreSql.Migrations
{
    public partial class UniqueRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_RefreshToken",
                table: "UserSessions",
                column: "RefreshToken",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserSessions_RefreshToken",
                table: "UserSessions");
        }
    }
}
