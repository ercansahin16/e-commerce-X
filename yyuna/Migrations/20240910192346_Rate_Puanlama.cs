using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace yyuna.Migrations
{
    /// <inheritdoc />
    public partial class Rate_Puanlama : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Ortalama",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Oysayisi",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToplamRate",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ortalama",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Oysayisi",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ToplamRate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Comments");
        }
    }
}
