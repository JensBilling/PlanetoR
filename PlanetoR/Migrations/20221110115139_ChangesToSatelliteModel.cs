using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanetoR.Migrations
{
    public partial class ChangesToSatelliteModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Satellites",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "Satellites",
                newName: "longitude");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "Satellites",
                newName: "latitude");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Satellites",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Satellites",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Satellites",
                newName: "id");

            migrationBuilder.AlterColumn<double>(
                name: "longitude",
                table: "Satellites",
                type: "double",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<double>(
                name: "latitude",
                table: "Satellites",
                type: "double",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Satellites",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "longitude",
                table: "Satellites",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "latitude",
                table: "Satellites",
                newName: "Latitude");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Satellites",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "Satellites",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Satellites",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Longitude",
                table: "Satellites",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Latitude",
                table: "Satellites",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
