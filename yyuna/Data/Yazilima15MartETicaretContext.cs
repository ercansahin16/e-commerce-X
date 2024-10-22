using Microsoft.EntityFrameworkCore;
using yyuna.Models;
using yyuna.ViewModel;

namespace yyuna.Data
{
   public class Yazilima15MartETicaretContext : DbContext
   {
      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         var builder = new ConfigurationBuilder()
            .SetBasePath(Directory
            .GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
         var configuration = builder.Build();
         optionsBuilder.UseSqlServer(configuration["ConnectionStrings:EticaretConnection"]);
      }


      public DbSet<Category> Categories { get; set; }
      public DbSet<Comment> Comments { get; set; }

      public DbSet<Message> Messages { get; set; }

      public DbSet<Order> Orders { get; set; }

      public DbSet<Product> Products { get; set; }

      public DbSet<Setting> Settings { get; set; }

      public DbSet<Status> Statuses { get; set; }

      public DbSet<Supplier> Suppliers { get; set; }

      public DbSet<User> Users { get; set; }

      public DbSet<TopluEmail> TopluEmal { get; set; }


      //Aşağıdakiler görüntüleme modelleridir
      public DbSet<MyOrderViewModel> vw_MyOrders { get; set; }
      public DbSet<QuickSearcViewModel> sp_Aramas { get; set; }

   }
}
