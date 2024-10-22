using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace yyuna.Migrations
{
   /// <inheritdoc />
   public partial class toplumail : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.CreateTable(
             name: "TopluEmal",
             columns: table => new
             {
                TopluMailID = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_TopluEmal", x => x.TopluMailID);
             });
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropTable(
             name: "TopluEmal");
      }
   }
}
