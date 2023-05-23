using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Food.DataBase.Migrations
{
    public partial class Add_RestaurantEmp_IdRestaurante : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdRestaurante",
                schema: "food",
                table: "RestaurantEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdRestaurante",
                schema: "food",
                table: "RestaurantEmployees");
        }
    }
}
