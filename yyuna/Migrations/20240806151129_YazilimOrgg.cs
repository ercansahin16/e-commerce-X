using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace yyuna.Migrations
{
   /// <inheritdoc />
   public partial class YazilimOrgg : Migration
   {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.CreateTable(
             name: "Categories",
             columns: table => new
             {
                CategoryID = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                ParentID = table.Column<int>(type: "int", nullable: false),
                CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Active = table.Column<bool>(type: "bit", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_Categories", x => x.CategoryID);
             });

         migrationBuilder.CreateTable(
             name: "Comments",
             columns: table => new
             {
                CommentID = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                UserID = table.Column<int>(type: "int", nullable: false),
                ProductID = table.Column<int>(type: "int", nullable: false),
                Review = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                AddDate = table.Column<DateTime>(type: "datetime2", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_Comments", x => x.CommentID);
             });

         migrationBuilder.CreateTable(
             name: "Messages",
             columns: table => new
             {
                MessageID = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                UserID = table.Column<int>(type: "int", nullable: false),
                ProductID = table.Column<int>(type: "int", nullable: false),
                Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_Messages", x => x.MessageID);
             });

         migrationBuilder.CreateTable(
             name: "Orders",
             columns: table => new
             {
                OrderID = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                OrderGroupGUID = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                UserID = table.Column<int>(type: "int", nullable: false),
                ProductID = table.Column<int>(type: "int", nullable: false),
                Quantity = table.Column<int>(type: "int", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_Orders", x => x.OrderID);
             });

         migrationBuilder.CreateTable(
             name: "Products",
             columns: table => new
             {
                ProductID = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                CateoryID = table.Column<int>(type: "int", nullable: false),
                SupplierID = table.Column<int>(type: "int", nullable: false),
                Stock = table.Column<int>(type: "int", nullable: false),
                Discount = table.Column<int>(type: "int", nullable: false),
                StatusID = table.Column<int>(type: "int", nullable: false),
                AddDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                Keywords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Kdv = table.Column<int>(type: "int", nullable: false),
                HighLighted = table.Column<int>(type: "int", nullable: false),
                TopSeller = table.Column<int>(type: "int", nullable: false),
                Related = table.Column<int>(type: "int", nullable: false),
                Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Active = table.Column<bool>(type: "bit", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_Products", x => x.ProductID);
             });

         migrationBuilder.CreateTable(
             name: "Settings",
             columns: table => new
             {
                SettingID = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                MainPageCount = table.Column<int>(type: "int", nullable: false),
                SubpageCount = table.Column<int>(type: "int", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_Settings", x => x.SettingID);
             });

         migrationBuilder.CreateTable(
             name: "Statuses",
             columns: table => new
             {
                StatusID = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                StatusName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Active = table.Column<bool>(type: "bit", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_Statuses", x => x.StatusID);
             });

         migrationBuilder.CreateTable(
             name: "Suppliers",
             columns: table => new
             {
                SupplierID = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                BrandName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Active = table.Column<bool>(type: "bit", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_Suppliers", x => x.SupplierID);
             });

         migrationBuilder.CreateTable(
             name: "Users",
             columns: table => new
             {
                UserID = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                NameSurname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                InvoicesAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                Active = table.Column<bool>(type: "bit", nullable: false)
             },
             constraints: table =>
             {
                table.PrimaryKey("PK_Users", x => x.UserID);
             });
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropTable(
             name: "Categories");

         migrationBuilder.DropTable(
             name: "Comments");

         migrationBuilder.DropTable(
             name: "Messages");

         migrationBuilder.DropTable(
             name: "Orders");

         migrationBuilder.DropTable(
             name: "Products");

         migrationBuilder.DropTable(
             name: "Settings");

         migrationBuilder.DropTable(
             name: "Statuses");

         migrationBuilder.DropTable(
             name: "Suppliers");

         migrationBuilder.DropTable(
             name: "Users");
      }
   }
}
