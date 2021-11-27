using Microsoft.EntityFrameworkCore.Migrations;

namespace KnowBetter_WebApp.Migrations
{
    public partial class ProductIngredientSpelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IngeridentId",
                table: "ProductIngredient");

            migrationBuilder.AddColumn<int>(
                name: "IngredientId",
                table: "ProductIngredient",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IngredientId",
                table: "ProductIngredient");

            migrationBuilder.AddColumn<int>(
                name: "IngeridentId",
                table: "ProductIngredient",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
