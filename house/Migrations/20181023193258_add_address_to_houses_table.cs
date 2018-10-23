using Microsoft.EntityFrameworkCore.Migrations;

namespace house.Migrations
{
    public partial class add_address_to_houses_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "House",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "House");
        }
    }
}
