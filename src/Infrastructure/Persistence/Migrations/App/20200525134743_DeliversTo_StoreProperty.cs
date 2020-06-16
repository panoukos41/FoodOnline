using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodOnline.Infrastructure.Persistence.Migrations.App
{
    public partial class DeliversTo_StoreProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliversTo",
                table: "Stores",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliversTo",
                table: "Stores");
        }
    }
}
