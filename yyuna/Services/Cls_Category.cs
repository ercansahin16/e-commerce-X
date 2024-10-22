using Microsoft.EntityFrameworkCore;
using yyuna.Data;
using yyuna.Models;

namespace yyuna.Services
{
   public class Cls_Category
   {
      Yazilima15MartETicaretContext context = new();
      public async Task<List<Category>> GetCategoriesAsync()
      {

         List<Category> categories = await context.Categories.ToListAsync();//Bütün kategorileri liste olarak döner(asenkron)
         return categories;
      }

      public List<Category> GetMainCategories()//Ana Kategorileri getir, yani parenID leri 0 olanları getir
      {

         List<Category> categories = context.Categories.Where(x => x.ParentID == 0).ToList();
         return categories;
      }


      public static bool CategoryInsert(Category category)
      {

         // Bu metod statik olduğu için Context direk gelmez, onun yerine using açarız.
         using (Yazilima15MartETicaretContext context = new())
         {
            try
            {
               context.Add(category);
               context.SaveChanges();
               return true;
            }
            catch (Exception)
            {
               return false;
            }
         }
      }

      public async Task<Category?> GetCategoryDetailsAsync(int? id)
      {
         Category? category = await context.Categories.FirstOrDefaultAsync(c => c.CategoryID == id);
         return category;
      }

      public static bool CategoryUpdate(Category category)
      {

         // Bu metod statik olduğu için Context direk gelmez, onun yerine using açarız.
         using (Yazilima15MartETicaretContext context = new())
         {
            try
            {
               context.Update(category);
               context.SaveChanges();
               return true;
            }
            catch (Exception)
            {
               return false;
            }
         }
      }


      public static bool categorydelete(int id)
      {
         // Bu metod statik olduğu için Context direk gelmez, onun yerine using açarız.
         using (Yazilima15MartETicaretContext context = new())
         {
            try
            {//Kategori İd sini yakalar
               Category? category = context
                   .Categories.FirstOrDefault(c => c.CategoryID == id);

               //Ana kategoriye ait Alt kategorileri listeler.
               List<Category> categories = context
                   .Categories.Where(c => c.ParentID == id).ToList();

               //Döngüyle ne kadar alt kategori var , false yapacak. yoksa zaten yapmaz.
               foreach (var item in categories)
               {
                  item.Active = false;
               }
               // Durumlarını False çeker
               category!.Active = false;
               context.SaveChanges();
               return true;
            }
            catch (Exception)
            {
               return false;
            }
         }
      }





















   }
}
