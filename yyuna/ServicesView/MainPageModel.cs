using yyuna.Models;

namespace yyuna.MainModel
{
   public class MainPageModel
   {
      public List<Product>? SliderProduct { get; set; }//Slider ürün prop
      public List<Product>? NewProduct { get; set; }//Yeni ürün prop
      public Product? ProductOfDay { get; set; }//Günün ürünü prop
      public List<Product>? SpecialProducts { get; set; }//Özelş ürüünü prop
      public List<Product>? StarProducts { get; set; }//Yıldızlı ürün prop
      public List<Product>? FeatureProducts { get; set; }//Fırsat ürünü prop
      public List<Product>? DiscountedProducts { get; set; }//İndirmli ürünü prop
      public List<Product>? HighlightedProducts { get; set; }//Öne çıkanar ürünü prop
      public List<Product>? TopsellerProducts { get; set; }//Çok satanalr ürünü prop
      public List<Product>? NotableProducts { get; set; }//Dikkat çeken ürünü prop
      public List<Product>? ProductsByCategory { get; set; }//CategoryPage
      public List<Product>? ProductsBySupplier { get; set; }//SupplierPage
      public Product? ProductDetails { get; set; }
      public string? CategoryName { get; set; }
      public string? BrandName { get; set; }
      public List<Product>? RelatedProduct { get; set; }

      public List<Comment>? comments { get; set; }


   }
}
