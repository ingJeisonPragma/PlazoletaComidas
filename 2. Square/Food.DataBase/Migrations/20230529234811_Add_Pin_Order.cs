using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Food.DataBase.Migrations
{
    public partial class Add_Pin_Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pin",
                schema: "food",
                table: "Orders",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pin",
                schema: "food",
                table: "Orders");
        }
    }
}
