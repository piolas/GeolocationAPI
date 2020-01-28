using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Geolocation.Infrastructure.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Connection",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    asn = table.Column<int>(nullable: false),
                    isp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    code = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    plural = table.Column<string>(nullable: true),
                    symbol = table.Column<string>(nullable: true),
                    symbol_native = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    geoname_id = table.Column<int>(nullable: false),
                    capital = table.Column<string>(nullable: true),
                    country_flag = table.Column<string>(nullable: true),
                    country_flag_emoji = table.Column<string>(nullable: true),
                    country_flag_emoji_unicode = table.Column<string>(nullable: true),
                    calling_code = table.Column<string>(nullable: true),
                    is_eu = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeZone",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    current_time = table.Column<DateTime>(nullable: false),
                    gmt_offset = table.Column<int>(nullable: false),
                    code = table.Column<string>(nullable: true),
                    is_daylight_saving = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeZone", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    code = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    native = table.Column<string>(nullable: true),
                    LocationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Language_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Geolocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ip = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: true),
                    continent_code = table.Column<string>(nullable: true),
                    continent_name = table.Column<string>(nullable: true),
                    country_code = table.Column<string>(nullable: true),
                    country_name = table.Column<string>(nullable: true),
                    region_code = table.Column<string>(nullable: true),
                    region_name = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true),
                    zip = table.Column<string>(nullable: true),
                    latitude = table.Column<double>(nullable: false),
                    longitude = table.Column<double>(nullable: false),
                    locationId = table.Column<Guid>(nullable: true),
                    time_zoneid = table.Column<string>(nullable: true),
                    currencyId = table.Column<Guid>(nullable: true),
                    connectionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geolocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Geolocations_Connection_connectionId",
                        column: x => x.connectionId,
                        principalTable: "Connection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Geolocations_Currency_currencyId",
                        column: x => x.currencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Geolocations_Location_locationId",
                        column: x => x.locationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Geolocations_TimeZone_time_zoneid",
                        column: x => x.time_zoneid,
                        principalTable: "TimeZone",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Geolocations_connectionId",
                table: "Geolocations",
                column: "connectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Geolocations_currencyId",
                table: "Geolocations",
                column: "currencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Geolocations_locationId",
                table: "Geolocations",
                column: "locationId");

            migrationBuilder.CreateIndex(
                name: "IX_Geolocations_time_zoneid",
                table: "Geolocations",
                column: "time_zoneid");

            migrationBuilder.CreateIndex(
                name: "IX_Language_LocationId",
                table: "Language",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Geolocations");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "Connection");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "TimeZone");

            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}
