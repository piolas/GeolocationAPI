using Microsoft.EntityFrameworkCore.Migrations;

namespace Geolocation.Infrastructure.Migrations
{
    public partial class AddURLValueFieldToRootObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "URLValue",
                table: "Geolocations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "URLValue",
                table: "Geolocations");
        }
    }
}
