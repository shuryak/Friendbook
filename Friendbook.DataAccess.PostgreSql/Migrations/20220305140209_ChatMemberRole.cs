using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Friendbook.DataAccess.PostgreSql.Migrations
{
    public partial class ChatMemberRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "ChatMembers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "ChatMembers");
        }
    }
}
