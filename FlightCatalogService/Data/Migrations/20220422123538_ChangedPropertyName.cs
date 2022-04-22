using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightCatalogService.Data.Migrations
{
    public partial class ChangedPropertyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightRoutes_Airports_FromId",
                table: "FlightRoutes");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightRoutes_Airports_ToId",
                table: "FlightRoutes");

            migrationBuilder.DropIndex(
                name: "IX_FlightRoutes_FromId",
                table: "FlightRoutes");

            migrationBuilder.DropIndex(
                name: "IX_FlightRoutes_ToId",
                table: "FlightRoutes");

            migrationBuilder.DropColumn(
                name: "FromId",
                table: "FlightRoutes");

            migrationBuilder.DropColumn(
                name: "ToId",
                table: "FlightRoutes");

            migrationBuilder.CreateIndex(
                name: "IX_FlightRoutes_FromAirportId",
                table: "FlightRoutes",
                column: "FromAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightRoutes_ToAirportId",
                table: "FlightRoutes",
                column: "ToAirportId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightRoutes_Airports_FromAirportId",
                table: "FlightRoutes",
                column: "FromAirportId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightRoutes_Airports_ToAirportId",
                table: "FlightRoutes",
                column: "ToAirportId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightRoutes_Airports_FromAirportId",
                table: "FlightRoutes");

            migrationBuilder.DropForeignKey(
                name: "FK_FlightRoutes_Airports_ToAirportId",
                table: "FlightRoutes");

            migrationBuilder.DropIndex(
                name: "IX_FlightRoutes_FromAirportId",
                table: "FlightRoutes");

            migrationBuilder.DropIndex(
                name: "IX_FlightRoutes_ToAirportId",
                table: "FlightRoutes");

            migrationBuilder.AddColumn<int>(
                name: "FromId",
                table: "FlightRoutes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToId",
                table: "FlightRoutes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FlightRoutes_FromId",
                table: "FlightRoutes",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightRoutes_ToId",
                table: "FlightRoutes",
                column: "ToId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightRoutes_Airports_FromId",
                table: "FlightRoutes",
                column: "FromId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightRoutes_Airports_ToId",
                table: "FlightRoutes",
                column: "ToId",
                principalTable: "Airports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
