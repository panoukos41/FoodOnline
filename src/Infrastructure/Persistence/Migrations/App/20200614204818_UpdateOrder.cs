using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodOnline.Infrastructure.Persistence.Migrations.App
{
    public partial class UpdateOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPriceEur",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalPriceEur",
                table: "Orders",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
