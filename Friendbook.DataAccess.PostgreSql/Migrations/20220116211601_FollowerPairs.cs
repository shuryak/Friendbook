using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Friendbook.DataAccess.PostgreSql.Migrations
{
    public partial class FollowerPairs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NickName",
                table: "UserProfiles",
                newName: "Nickname");

            migrationBuilder.CreateTable(
                name: "FollowerPairs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FollowerId = table.Column<int>(type: "integer", nullable: false),
                    FollowingId = table.Column<int>(type: "integer", nullable: false),
                    IsRetroactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowerPairs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "FollowerPairs",
                columns: new[] { "Id", "FollowerId", "FollowingId", "IsRetroactive" },
                values: new object[] { 1, 1, 2, false });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "Nickname" },
                values: new object[,]
                {
                    { 2, new DateOnly(2003, 2, 16), "Ivan", "Vdovin", "ivan03vdovin" },
                    { 3, new DateOnly(1998, 6, 10), "Artur", "Ivanov", "archii" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FollowerPairs");

            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "Nickname",
                table: "UserProfiles",
                newName: "NickName");
        }
    }
}
