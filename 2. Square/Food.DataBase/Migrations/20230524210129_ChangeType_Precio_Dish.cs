using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Food.DataBase.Migrations
{
    public partial class ChangeType_Precio_Dish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_RestaurantEmployees_IdChef",
                schema: "food",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "IdChef",
                schema: "food",
                table: "Orders",
                type: "int",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "Precio",
                schema: "food",
                table: "Dishes",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_RestaurantEmployees_IdChef",
                schema: "food",
                table: "Orders",
                column: "IdChef",
                principalSchema: "food",
                principalTable: "RestaurantEmployees",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_RestaurantEmployees_IdChef",
                schema: "food",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "IdChef",
                schema: "food",
                table: "Orders",
                type: "int",
                maxLength: 20,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                schema: "food",
                table: "Dishes",
                type: "decimal(8,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_RestaurantEmployees_IdChef",
                schema: "food",
                table: "Orders",
                column: "IdChef",
                principalSchema: "food",
                principalTable: "RestaurantEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
