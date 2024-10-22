using Microsoft.EntityFrameworkCore;
using yyuna.Data;
using yyuna.Models;

namespace yyuna.Services
{

   public class Cls_Status
   {
      Yazilima15MartETicaretContext context = new();

      public async Task<List<Status>> GetStatusAsync()
      {

         List<Status> status = await context.Statuses.ToListAsync();
         return status;
      }

      public static bool StatusInsert(Status status)
      {
         using (Yazilima15MartETicaretContext context = new())
         {
            try
            {
               context.Add(status);
               context.SaveChanges();
               return true;
            }
            catch (Exception)
            {

               return false;
            }
         }
      }

      public async Task<Status> GetStatusDetailsAsync(int? id)
      {
         Status? status = await context.Statuses.FirstOrDefaultAsync(s => s.StatusID == id);
         return status;

      }


      public static bool StatusUpdate(Status status)
      {
         using (Yazilima15MartETicaretContext context = new())
         {
            try
            {
               context.Update(status);
               context.SaveChanges();
               return true;
            }
            catch (Exception)
            {

               return false;
            }
         }

      }

      public static bool statusdelete(int id)
      {
         // Bu metod statik olduğu için Context direk gelmez, onun yerine using açarız.
         using (Yazilima15MartETicaretContext context = new())
         {
            try
            {//Supplier İd sini yakalar
               Status? status = context
                   .Statuses.FirstOrDefault(x => x.StatusID == id);

               // Durumlarını False çeker
               status!.Active = false;
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
