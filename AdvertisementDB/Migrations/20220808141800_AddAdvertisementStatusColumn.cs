using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvertisementDB.Migrations
{
    public partial class AddAdvertisementStatusColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Users SET Name = SUBSTRING(Name, 0, 10)");
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Advertisements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Advertisements");
        }
    }
}
