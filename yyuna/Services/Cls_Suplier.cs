using Microsoft.EntityFrameworkCore;
using yyuna.Data;
using yyuna.Models;

namespace yyuna.Services
{
   public class Cls_Suplier

   {
      Yazilima15MartETicaretContext context = new();
      public async Task<List<Supplier>> GetSuppliersAsync()
      {

         List<Supplier> suppliers = await context.Suppliers.ToListAsync();//Bütün kategorileri liste olarak döner(asenkron)
         return suppliers;
      }


      public static bool SupplierInsert(Supplier supplier)
      {

         //Metot statik olduğu için metod direk gelmez
         using (Yazilima15MartETicaretContext contex = new())
         {
            try
            {
               contex.Add(supplier);
               contex.SaveChanges();
               return true;
            }
            catch (Exception)
            {

               return false;
            }

         }
      }

      public async Task<Supplier> GetSuppliersDetailsAsync(int? id)
      {
         Supplier? supplier = await context.Suppliers.FirstOrDefaultAsync(s => s.SupplierID == id);
         return supplier;

      }

      public static bool SupplierUpdate(Supplier supplier)
      {
         using (Yazilima15MartETicaretContext context = new())
         {
            try
            {
               context.Update(supplier);
               context.SaveChanges();
               return true;
            }
            catch (Exception)
            {

               return false;
            }
         }

      }


      public static bool supplierdelete(int id)
      {
         // Bu metod statik olduğu için Context direk gelmez, onun yerine using açarız.
         using (Yazilima15MartETicaretContext context = new())
         {
            try
            {//Supplier İd sini yakalar
               Supplier? supplier = context
                   .Suppliers.FirstOrDefault(x => x.SupplierID == id);

               // Durumlarını False çeker
               supplier!.Active = false;
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
