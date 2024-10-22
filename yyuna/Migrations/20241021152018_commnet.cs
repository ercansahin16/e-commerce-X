using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace yyuna.Migrations
{
    /// <inheritdoc />
    public partial class commnet : Migration
    {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropColumn(
             name: "ProductID",
             table: "Messages");

         migrationBuilder.DropColumn(
             name: "UserID",
             table: "Messages");

         migrationBuilder.AddColumn<string>(
             name: "email",
             table: "Messages",
             type: "nvarchar(max)",
             nullable: true);

         migrationBuilder.AddColumn<string>(
             name: "namesurname",
             table: "Messages",
             type: "nvarchar(max)",
             nullable: true);

         // title sütununu ekleyin
         migrationBuilder.AddColumn<string>(
             name: "title",
             table: "Messages",
             type: "nvarchar(max)",
             nullable: true);
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropColumn(
             name: "email",
             table: "Messages");

         migrationBuilder.DropColumn(
             name: "namesurname",
             table: "Messages");

         migrationBuilder.DropColumn(
             name: "title",
             table: "Messages");

         migrationBuilder.AddColumn<int>(
             name: "ProductID",
             table: "Messages",
             type: "int",
             nullable: false,
             defaultValue: 0);

         migrationBuilder.AddColumn<int>(
             name: "UserID",
             table: "Messages",
             type: "int",
             nullable: false,
             defaultValue: 0);
      }
   }
}
