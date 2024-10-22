using Microsoft.EntityFrameworkCore;
using yyuna.Data;
using yyuna.Models;
using yyuna.ViewModel;

namespace yyuna.Services
{
   public class Cls_Product
   {
      Yazilima15MartETicaretContext context = new();
      public async Task<List<Product>> GetProductsAsync()
      {

         List<Product> products = await context.Products.ToListAsync();//Bütün kategorileri liste olarak döner(asenkron)
         return products;
      }

      public static bool ProductInsert(Product product)
      {
         using (Yazilima15MartETicaretContext context = new())
         {
            try
            {
               //product.ProductName= product.ProductName.ToLower().Trim(); Bu şekilde kullanıcının girdiği veriyi hem küçük harflere çevirmiş hemde boşlukları ladığımız için , veritabanındakiyle kolayla exist leyebiliriz.



               if (product.Notes == null)
                  product.Notes = "";
               product.AddDate = DateTime.Now;

               bool exist = context.Products.Any(x => x.ProductName.ToLower().Trim() == product.ProductName.ToLower().Trim());

               if (!exist)
               {
                  context.Add(product);
                  context.SaveChanges();
                  return true;
               }
               return false;
            }
            catch (Exception)
            {

               return false;
            }
         }
      }

      public async Task<Product?> GetProductDetailsAsync(int? id)
      {
         Product? product = await context.Products.FirstOrDefaultAsync(pr => pr.ProductID == id);
         return product;
      }

      public static bool ProductUpdate(Product product)
      {

         using (Yazilima15MartETicaretContext context = new())
         {
            try
            {
               context.Update(product);
               context.SaveChanges();
               return true;
            }
            catch (Exception)
            {

               return false;
            }
         }

      }

      public static bool ProductDelete(int id)
      {
         using (Yazilima15MartETicaretContext context = new())
         {
            try
            {
               Product? product = context.Products.FirstOrDefault(p => p.ProductID == id);
               product!.Active = false;
               context.SaveChanges();
               return true;
            }
            catch (Exception)
            {

               return false;
            }
         }
      }

      public static List<QuickSearcViewModel> gettingSearchProducts(string id)
      {
         using (Yazilima15MartETicaretContext context = new Yazilima15MartETicaretContext())
         {
            var products = context.sp_Aramas.FromSql($"sp_arama {id}").ToList();
            return products;
         }
      }







   }
}
