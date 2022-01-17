using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Friendbook.DataAccess.PostgreSql.Migrations
{
    public partial class UserPasswords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FollowerPairs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "UserProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "UserProfiles");

            migrationBuilder.InsertData(
                table: "FollowerPairs",
                columns: new[] { "Id", "FollowerId", "FollowingId", "IsRetroactive" },
                values: new object[] { 1, 1, 2, false });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "Nickname" },
                values: new object[,]
                {
                    { 1, new DateOnly(2004, 1, 24), "Alexander", "Konovalov", "shuryak" },
                    { 2, new DateOnly(2003, 2, 16), "Ivan", "Vdovin", "ivan03vdovin" },
                    { 3, new DateOnly(1998, 6, 10), "Artur", "Ivanov", "archii" }
                });
        }
    }
}
