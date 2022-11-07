using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanetoR.Migrations
{
    public partial class AddGravitationalForcePropery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GravitationalForce",
                table: "CelestialBodies",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GravitationalForce",
                table: "CelestialBodies");
        }
    }
}
