using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManagementService.Data.Migrations
{
    public partial class ChangedManufacturerColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Company",
                table: "Manufacturers",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Manufacturers",
                newName: "Company");
        }
    }
}
