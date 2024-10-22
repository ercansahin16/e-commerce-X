using Microsoft.AspNetCore.Mvc;
using yyuna.Data;
using yyuna.Models;

namespace yyuna.ViewComponents
{
   public class Menus : ViewComponent
   {

      Yazilima15MartETicaretContext context = new();

      public IViewComponentResult Invoke()
      {

         List<Category> categories = context.Categories.Where(x => x.Active).ToList();
         return View(categories);
      }
   }
}
