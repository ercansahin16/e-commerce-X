using System.Linq;
using yyuna.Data;
using yyuna.Models;

namespace yyuna.MainModel
{
   public class Cls_ProductsMain
   {
      Yazilima15MartETicaretContext context = new();

      public List<Product> GetProducts(string mainPageName, string subPageName, int pagenumber = 0)
      {


         List<Product> products;


         int? MainPageCount = context.Settings.FirstOrDefault(x => x.SettingID == 1)?.MainPageCount;
         int? MainPageSubCount = context.Settings.FirstOrDefault(x => x.SettingID == 1)?.SubpageCount;
         

         if (mainPageName == "SliderProducts")
         {
            //Slider ürünleri
            products = context.Products.Where(x => x.StatusID == 1 && x.Active).Take(Convert.ToInt32(MainPageCount)).ToList();
         }
         else if (mainPageName == "NewProducts")
         { //New Ürünler
            if (subPageName == "Index")
            {
               //Home/Index - Yani 8li değer gelicek
               products = context.Products.Where(x => x.Active).OrderByDescending(x => x.AddDate).Take(Convert.ToInt32(MainPageCount)).ToList();
               
            }
            else
            {
               if (subPageName == "topmenü")
               {//Home/NewProduct - Yani Convert.ToInt32(MainPageSubCount)lü değer gelecek- İkinci parametre "New" için çalışır
                  products = context.Products.Where(x => x.Active).OrderByDescending(x => x.AddDate).Take(Convert.ToInt32(MainPageCount)).ToList();
               }
               else
               {//ajax
                  products = context.Products.Where(x => x.Active).OrderByDescending(x => x.AddDate).Skip(pagenumber * Convert.ToInt32(MainPageSubCount)).Take(Convert.ToInt32(MainPageSubCount)).ToList();
               }
            }

         }
         else if (mainPageName == "SpecialProducts")
         { //Special Ürünler
            if (subPageName == "Index")
            {
               //Home/Index - Yani 8li değer gelicek
               products = context.Products.Where(x => x.StatusID == 2 && x.Active).OrderByDescending(o => o.AddDate).Take(Convert.ToInt32(MainPageCount)).ToList();
            }
            else
            {
               if (subPageName == "topmenü")
               {//Home/SpecialProducts - Yani Convert.ToInt32(MainPageSubCount)lü değer gelecek- İkinci parametre "Special" için çalışır
                  products = context.Products.Where(x => x.StatusID == 2 && x.Active).OrderByDescending(o => o.AddDate).Take(Convert.ToInt32(MainPageSubCount)).ToList();
               }
               else
               {//ajax

                  products = context.Products.Where(x => x.StatusID == 2 && x.Active).OrderByDescending(o => o.AddDate).Skip(pagenumber * Convert.ToInt32(MainPageSubCount)).Take(Convert.ToInt32(MainPageSubCount)).ToList();
               }
            }

         }
         else if (mainPageName == "DiscountedProducts")
         { //Special Ürünler
            if (subPageName == "Index")
            {
               //Home/Index - Yani 8li değer gelicek
               products = context.Products.Where(x => x.Active).OrderByDescending(x => x.Discount).Take(Convert.ToInt32(MainPageCount)).ToList();
            }
            else
            {
               if (subPageName == "topmenü")
               {//Home/DiscountedProducts - Yani Convert.ToInt32(MainPageSubCount)lü değer gelecek- İkinci parametre "İndirimli" için çalışır
                  products = context.Products.Where(x => x.Active).OrderByDescending(x => x.Discount).Take(Convert.ToInt32(MainPageSubCount)).ToList();
               }
               else
               {//ajax

                  products = context.Products.Where(x => x.Active).OrderByDescending(x => x.Discount).Skip(pagenumber * Convert.ToInt32(MainPageSubCount)).Take(Convert.ToInt32(MainPageSubCount)).ToList();
               }
            }

         }
         else if (mainPageName == "HighlightedProducts")
         { //Öne çıkanlar
            if (subPageName == "Index")
            {
               //Home/Index - Yani 8li değer gelicek
               products = context.Products.Where(x => x.Active).OrderByDescending(x => x.HighLighted).Take(Convert.ToInt32(MainPageCount)).ToList();
            }
            else
            {//Home/HighlightedProducts
               if (subPageName == "topmenü")
               {// - Yani Convert.ToInt32(MainPageSubCount)lü değer gelecek- İkinci parametre "Öne çıkanlar" için çalışır


                  products = context.Products.Where(x => x.Active).OrderByDescending(x => x.HighLighted).Take(Convert.ToInt32(MainPageSubCount)).ToList();
               }
               else
               {//ajax

                  products = context.Products.Where(x => x.Active).OrderByDescending(x => x.HighLighted).Skip(pagenumber * Convert.ToInt32(MainPageSubCount)).Take(Convert.ToInt32(MainPageSubCount)).ToList();

               }
            }

         }
         else if (mainPageName == "StarProducts")
         {
            //Star Ürünler
            products = context.Products.Where(x => x.StatusID == 3 && x.Active).OrderByDescending(o => o.AddDate).Take(Convert.ToInt32(MainPageCount)).ToList();
         }
         else if (mainPageName == "FeatureProducts")
         {
            //Fırsat Ürünler
            products = context.Products.Where(x => x.StatusID == 4 && x.Active).OrderByDescending(o => o.AddDate).Take(Convert.ToInt32(MainPageCount)).ToList();
         }
         else if (mainPageName == "TopsellerProducts")
         {
            //Çok satanlar Ürünler
            products = context.Products.Where(x => x.Active).OrderByDescending(x => x.TopSeller).Take(Convert.ToInt32(MainPageCount)).ToList();
         }
         else if (mainPageName == "NotableProducts")
         {
            //Dikkat çeken Ürünler
            products = context.Products.Where(x => x.StatusID == 5 && x.Active).OrderByDescending(x => x.AddDate).Take(Convert.ToInt32(MainPageCount)).ToList();
         }
         else
         {
            return null;
         }
         return products;
      }
      public Product? GetProductOfDay()//Günün Ürünün getiren
      {
         Product? product = context.Products.FirstOrDefault(x => x.StatusID == 6);
         return product;
      }
      public List<Product> GetProductsByCategoryId(int id)
      {
         List<Product> products = context.Products.Where(x => x.CateoryID == id && x.Active).OrderByDescending(x => x.AddDate).ToList();
         return products;
      }
      public List<Product> GetProductsBySupplierId(int id)
      {
         List<Product> products = context.Products.Where(x => x.SupplierID == id && x.Active).OrderByDescending(x => x.AddDate).ToList();
         return products;
      }
      public void HihlightedPlus(int id)
      {
         //1. Productstsn ürünü bul
         //2.Highlighted 1 artır
         using (Yazilima15MartETicaretContext context = new())
         {
            Product? product = context.Products.FirstOrDefault(p => p.ProductID == id);
            product.HighLighted = product.HighLighted + 1;
            context.Update(product);
            context.SaveChanges();
         }
      }








   }
}
