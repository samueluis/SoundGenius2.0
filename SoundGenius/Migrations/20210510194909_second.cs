using Microsoft.EntityFrameworkCore.Migrations;

namespace SoundGenius.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Albuns",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Albuns",
                keyColumn: "ID",
                keyValue: 1,
                column: "Data",
                value: "11");

            migrationBuilder.UpdateData(
                table: "Albuns",
                keyColumn: "ID",
                keyValue: 2,
                column: "Data",
                value: "11");

            migrationBuilder.UpdateData(
                table: "Albuns",
                keyColumn: "ID",
                keyValue: 3,
                column: "Data",
                value: "11");

            migrationBuilder.UpdateData(
                table: "Albuns",
                keyColumn: "ID",
                keyValue: 4,
                column: "Data",
                value: "11");

            migrationBuilder.UpdateData(
                table: "Albuns",
                keyColumn: "ID",
                keyValue: 5,
                column: "Data",
                value: "11");

            migrationBuilder.UpdateData(
                table: "Albuns",
                keyColumn: "ID",
                keyValue: 6,
                column: "Data",
                value: "11");

            migrationBuilder.UpdateData(
                table: "Albuns",
                keyColumn: "ID",
                keyValue: 7,
                column: "Data",
                value: "11");

            migrationBuilder.UpdateData(
                table: "Albuns",
                keyColumn: "ID",
                keyValue: 8,
                column: "Data",
                value: "11");

            migrationBuilder.UpdateData(
                table: "Albuns",
                keyColumn: "ID",
                keyValue: 9,
                column: "Data",
                value: "11");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Albuns");
        }
    }
}
