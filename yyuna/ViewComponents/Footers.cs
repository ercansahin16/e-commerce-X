using Microsoft.AspNetCore.Mvc;
using yyuna.Data;
using yyuna.Models;

namespace yyuna.ViewComponents
{
   public class Footers : ViewComponent
   {

      Yazilima15MartETicaretContext context = new();

      public IViewComponentResult Invoke()
      {

         List<Supplier> suppliers = context.Suppliers.Where(x => x.Active).ToList();
         return View(suppliers);
      }


   }
}
