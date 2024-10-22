
using Microsoft.Data.SqlClient;
using yyuna.Data;
using yyuna.Models;
using yyuna.ViewModel;

namespace yyuna.MainModel
{
   public class Cls_Order
   {

      public int ProductID { get; set; }
      public string? ProductName { get; set; }
      public int Quantity { get; set; }
      public string? Sepet { get; set; } //Çerez valuesi -- 5=1&10=1&7=1

      public decimal UnitPrice { get; set; }
      public int Kdv { get; set; }
      public String? PhotoPath { get; set; }

      Yazilima15MartETicaretContext context = new();
      public bool AddToCArt(string id)
      {
         bool isvalid = false;
         if (Sepet == "")
         {
            //Sepete ilk defa ürün ekleniyor.
            Sepet = id + "=1";
         }
         else
         {
            //Daha önceden sepete bir şeyler eklenmiş  am aşu an eklemek istediği şey sepetinde zaten var mı bilemiyruz, onu kontrrol etmeliyz.
            //10=1&20=1
            string[] SepetArray = Sepet.Split('&');
            for (int i = 0; i < SepetArray.Length; i++)
            {
               //10=1 -> SepetArray[0]
               //20=1 -> SepetArray[1]
               string[] SepetArray2 = SepetArray[i].Split('=');
               if (SepetArray2[0] == id)
               {
                  //DEmekki bu ürün sepette zaten var.
                  isvalid = true;
               }
            }
            if (isvalid == false)
            {
               //Ürün sepete daha önce eklenmemiş
               Sepet = Sepet + "&" + id + "=1";
            }
         }
         return isvalid;
      }
      //Sepete ekle işi burada yapılacak

      //Projede  sağ üst koşedeki sepet sayfası ve sil butonu tıklanınca yuklenecek olan sayfa bu metodu çağıracak
      //List<cls_order>=propertyleri dönecek
      //Siparişi onaylama metodu da çağırıyor

      public List<Cls_Order> GetMyCart()
      {
         List<Cls_Order> list = new List<Cls_Order>();
         string[] sepetdizi = Sepet.Split('&');//Sepetteki ürünleri önce ayırdık(5=1&10=1&7=1) Yani & olan bölümleri ayırıyoruz
         if (sepetdizi[0] != "")
         {
            for (int i = 0; i < sepetdizi.Length; i++)
            {
               string[] sepetdizi2 = sepetdizi[i].Split('=');//Bu sefer = olan yerden boleceğiz.
               int sepetid = Convert.ToInt32(sepetdizi2[0]);
               int adet = Convert.ToInt32(sepetdizi2[1]);
               Product? product = context.Products.FirstOrDefault(p => p.ProductID == sepetid);

               Cls_Order p = new Cls_Order();
               p.ProductID = product.ProductID;
               p.ProductName = product.ProductName;
               p.Quantity = adet;
               p.UnitPrice = product.UnitPrice;
               p.Kdv = product.Kdv;
               p.PhotoPath = product.PhotoPath;
               list.Add(p);
            }
         }


         return list;


      }

      public void DeleteFromMyCart(string id)
      {
         string[] sepetdizi = Sepet.Split('&');//Ürünleri ayırıyoruz
         string yenisepet = "";
         for (int i = 0; i < sepetdizi.Length; i++)
         {
            string[] sepetdizi2 = sepetdizi[i].Split('=');
            int adet = Convert.ToInt32(sepetdizi2[1]);
            if (sepetdizi2[0] != id)
            {
               //Silinmeyecek ürünleri burada yakalıyoruz ve yeni sepet oluşturuyoruz
               if (yenisepet == "")
               {
                  yenisepet = sepetdizi2[0] + "=" + adet.ToString();
               }
               else
               {
                  yenisepet = yenisepet + "&" + sepetdizi2[0] + "=" + adet.ToString();
               }
            }
         }
         Sepet = yenisepet;
      }



      public string WriteFromCookieToTable(string Email)
      {//Ürünleri ayrı ayrı dönerken (siparişler tablosuna işlerken) o ürünlerin stok değerlerini Quantity kadar azalt.
         //Ve Yine o ürünlerin top seller kolonunu Quantity kadar arttır.
         string OrderGroupGUID = DateTime.Now.ToString().Replace(".", "").Replace(":", "").Replace(" ", ""); ;
         DateTime OrderDate = DateTime.Now;
         List<Cls_Order> orders = GetMyCart();
         foreach (var item in orders)
         {
            Order order = new Order();
            order.OrderDate = OrderDate;
            order.OrderGroupGUID = OrderGroupGUID;
            order.UserID = context.Users.FirstOrDefault(u => u.Email == Email).UserID;
            order.ProductID = item.ProductID;
            order.Quantity = item.Quantity;
            context.Orders.Add(order);//Siparişler tablosuna o sipariş ekleniyor

            Product? product = context.Products.FirstOrDefault(p => p.ProductID == order.ProductID);

            product.Stock -= order.Quantity;

            if (product.Stock == 0)//Eğer stok 0 olursa o ürünü pasife çek.
            {
               product.Active = false;
            }

            product.TopSeller += order.Quantity;




            context.SaveChanges();

         }
         return OrderGroupGUID;
      }


      public List<MyOrderViewModel> SelectMyOrder(string Email)
      {
         int userID = context.Users.FirstOrDefault(x => x.Email == Email)!.UserID;
         List<MyOrderViewModel> orders = context.vw_MyOrders.Where(o => o.UserID == userID).ToList();
         return orders;
      }

      public List<Cls_Order> Select_Products_DetailsSearch(string query)
      {
         List<Cls_Order> products = new();

         SqlConnection sqlcon = AdoNetConnection.baglanti;
         SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
         sqlcon.Open();
         SqlDataReader sqlDataReader = sqlcmd.ExecuteReader();
         while (sqlDataReader.Read())
         {
            Cls_Order p = new Cls_Order();
            p.ProductID = Convert.ToInt32(sqlDataReader["ProductID"]);
            p.ProductName = sqlDataReader["ProductName"].ToString();
            p.UnitPrice = Convert.ToDecimal(sqlDataReader["UnitPrice"]);
            p.PhotoPath = sqlDataReader["PhotoPath"].ToString();
            products.Add(p);
         }
         sqlcon.Close();
         return products;
      }







   }
}
