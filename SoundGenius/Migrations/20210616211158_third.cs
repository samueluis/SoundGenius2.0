using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoundGenius.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Data",
                table: "Albuns",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "AlbumFaixas",
                columns: new[] { "ID", "AlbumFK", "FaixaFK" },
                values: new object[] { 11, 1, 9 });

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
            migrationBuilder.DeleteData(
                table: "AlbumFaixas",
                keyColumn: "ID",
                keyValue: 11);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "Albuns",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Albuns",
                keyColumn: "ID",
                keyValue: 1,
                column: "Data",
                value: new DateTime(1998, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Albuns",
                keyColumn: "ID",
                keyValue: 2,
                column: "Data",
                value: new DateTime(1999, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Albuns",
                keyColumn: "ID",
                keyValue: 3,
                column: "Data",
                value: new DateTime(1994, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Albuns",
                keyColumn: "ID",
                keyValue: 4,
                column: "Data",
                value: new DateTime(2010, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Albuns",
                keyColumn: "ID",
                keyValue: 5,
                column: "Data",
                value: new DateTime(2019, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Albuns",
                keyColumn: "ID",
                keyValue: 6,
                column: "Data",
                value: new DateTime(2019, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Albuns",
                keyColumn: "ID",
                keyValue: 7,
                column: "Data",
                value: new DateTime(2019, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Albuns",
                keyColumn: "ID",
                keyValue: 8,
                column: "Data",
                value: new DateTime(2019, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Albuns",
                keyColumn: "ID",
                keyValue: 9,
                column: "Data",
                value: new DateTime(2019, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
