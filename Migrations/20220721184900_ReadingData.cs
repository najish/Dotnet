using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotnet.Migrations
{
    public partial class ReadingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[,]
                {
                    { 101, "Delhi", "Ramesh Kumar" },
                    { 102, "Aurangabad", "Raja Kumar" },
                    { 103, "Dehri", "Ramesh Kumar" },
                    { 104, "Gaya", "Lalita Kumari" },
                    { 105, "Patna", "Ramesh Kumar" },
                    { 106, "Hyderabad", "Ramesh Kumar" },
                    { 107, "Chennai", "Rahim Khan" },
                    { 108, "Orisha", "Rajesh Khanna" },
                    { 109, "Bihar", "Sanjay Kumar" },
                    { 110, "Varanashi", "Ramesh Kumar" },
                    { 111, "Pune", "Shivam Kumar" },
                    { 112, "Jaipur", "Ramesh Kumar" },
                    { 113, "Jodhpur", "Sweety Kumari" },
                    { 114, "Jhansi", "Sanjay Kumar" },
                    { 115, "Mumbai", "Mahesh Kumar" },
                    { 116, "Goa", "Ganesh Kumar" },
                    { 117, "Ranchi", "Rani Kumari" },
                    { 118, "Deoghar", "Shivam Kumar" },
                    { 119, "Delhi", "Suresh Kumar" },
                    { 120, "Delhi", "Sujeet Kumar" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 120);
        }
    }
}
