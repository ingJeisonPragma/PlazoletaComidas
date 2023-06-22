using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Food.DataBase.Migrations
{
    public partial class InitialDBFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "food");

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "food",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantEmployees",
                schema: "food",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersona = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantEmployees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                schema: "food",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nit = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdPropietario = table.Column<int>(type: "int", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    urlLogo = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dishes",
                schema: "food",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdCategoria = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    IdRestaurant = table.Column<int>(type: "int", nullable: false),
                    urlImagen = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dishes_Categories_IdCategoria",
                        column: x => x.IdCategoria,
                        principalSchema: "food",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dishes_Restaurants_IdRestaurant",
                        column: x => x.IdRestaurant,
                        principalSchema: "food",
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "food",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IdChef = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    IdRestaurante = table.Column<int>(type: "int", nullable: false),
                    DishEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Dishes_DishEntityId",
                        column: x => x.DishEntityId,
                        principalSchema: "food",
                        principalTable: "Dishes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_RestaurantEmployees_IdChef",
                        column: x => x.IdChef,
                        principalSchema: "food",
                        principalTable: "RestaurantEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Restaurants_IdRestaurante",
                        column: x => x.IdRestaurante,
                        principalSchema: "food",
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDishes",
                schema: "food",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPedido = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    IdPlato = table.Column<int>(type: "int", maxLength: 100, nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDishes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDishes_Dishes_IdPlato",
                        column: x => x.IdPlato,
                        principalSchema: "food",
                        principalTable: "Dishes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderDishes_Orders_IdPedido",
                        column: x => x.IdPedido,
                        principalSchema: "food",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_IdCategoria",
                schema: "food",
                table: "Dishes",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_IdRestaurant",
                schema: "food",
                table: "Dishes",
                column: "IdRestaurant");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDishes_IdPedido",
                schema: "food",
                table: "OrderDishes",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDishes_IdPlato",
                schema: "food",
                table: "OrderDishes",
                column: "IdPlato");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DishEntityId",
                schema: "food",
                table: "Orders",
                column: "DishEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdChef",
                schema: "food",
                table: "Orders",
                column: "IdChef");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdRestaurante",
                schema: "food",
                table: "Orders",
                column: "IdRestaurante",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDishes",
                schema: "food");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "food");

            migrationBuilder.DropTable(
                name: "Dishes",
                schema: "food");

            migrationBuilder.DropTable(
                name: "RestaurantEmployees",
                schema: "food");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "food");

            migrationBuilder.DropTable(
                name: "Restaurants",
                schema: "food");
        }
    }
}
