using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanetoR.Migrations
{
    public partial class ChangedPropsOfCelestialBody : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageTempCelsius",
                table: "CelestialBodies");

            migrationBuilder.DropColumn(
                name: "GravitationalForce",
                table: "CelestialBodies");

            migrationBuilder.DropColumn(
                name: "Lat",
                table: "CelestialBodies");

            migrationBuilder.DropColumn(
                name: "Long",
                table: "CelestialBodies");

            migrationBuilder.AlterColumn<string>(
                name: "country",
                table: "Satellites",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "AverageTemperature",
                table: "CelestialBodies",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DayInEarthHours",
                table: "CelestialBodies",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Density",
                table: "CelestialBodies",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Diameter",
                table: "CelestialBodies",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Gravity",
                table: "CelestialBodies",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Mass",
                table: "CelestialBodies",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NumberOfMoons",
                table: "CelestialBodies",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "YearInEarthDays",
                table: "CelestialBodies",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageTemperature",
                table: "CelestialBodies");

            migrationBuilder.DropColumn(
                name: "DayInEarthHours",
                table: "CelestialBodies");

            migrationBuilder.DropColumn(
                name: "Density",
                table: "CelestialBodies");

            migrationBuilder.DropColumn(
                name: "Diameter",
                table: "CelestialBodies");

            migrationBuilder.DropColumn(
                name: "Gravity",
                table: "CelestialBodies");

            migrationBuilder.DropColumn(
                name: "Mass",
                table: "CelestialBodies");

            migrationBuilder.DropColumn(
                name: "NumberOfMoons",
                table: "CelestialBodies");

            migrationBuilder.DropColumn(
                name: "YearInEarthDays",
                table: "CelestialBodies");

            migrationBuilder.UpdateData(
                table: "Satellites",
                keyColumn: "country",
                keyValue: null,
                column: "country",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "country",
                table: "Satellites",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "AverageTempCelsius",
                table: "CelestialBodies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GravitationalForce",
                table: "CelestialBodies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Lat",
                table: "CelestialBodies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Long",
                table: "CelestialBodies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
