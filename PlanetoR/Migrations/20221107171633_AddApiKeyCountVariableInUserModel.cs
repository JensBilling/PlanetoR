using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanetoR.Migrations
{
    public partial class AddApiKeyCountVariableInUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApiKeyCount",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiKeyCount",
                table: "users");
        }
    }
}
