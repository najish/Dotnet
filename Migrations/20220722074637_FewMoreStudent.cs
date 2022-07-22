using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotnet.Migrations
{
    public partial class FewMoreStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[,]
                {
                    { 121, "Hyderabad", "Zafer Ravish" },
                    { 123, "Address 1", "User 1" },
                    { 124, "Address 2", "User 2" },
                    { 125, "Address 3", "User 3" },
                    { 126, "Address 4", "User 4" },
                    { 127, "Address 5", "User 5" },
                    { 128, "Address 6", "User 6" },
                    { 129, "Address 7", "User 7" },
                    { 130, "Address 8", "User 8" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 130);
        }
    }
}
