using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PagedList.Core;
using System.Collections.Specialized;
using System.Text;
using yyuna.Data;
using yyuna.Hubs;
using yyuna.MainModel;
using yyuna.Models;
using yyuna.Services;
using yyuna.ViewModel;

namespace yyuna.Controllers
{
   public class HomeController : Controller
   {

      /*1-Slider---
		 * 2-Özel----
		 * 3-Yıldızlı-----
		 * 4-Fırsat---
		 * 5-Dikkat----
		 * 6-Günün---
		 * 
		 * AddDate =Yeni Ürünler---
		 * Discount=İndirimli ------
		 * Highlighted= öne çıkanlar--
		 * Topseller= En çok satanlar-----
		 */

      Yazilima15MartETicaretContext context = new();
      MainPageModel mpm = new();
      Cls_ProductsMain mainProduct = new();
      Cls_Order order = new();
      Cls_User users = new();
      Cls_Category c = new();
      Cls_Suplier s = new();
      IHubContext<AdminHub> _hubContext;

      public HomeController(IHubContext<AdminHub> hubContext)
      {
         _hubContext = hubContext;
      }

    


      public IActionResult ContactUs()
      {
         return View();
      }
      [HttpPost]
      public IActionResult ContactUs(Message message)
      {
         if(ModelState.IsValid)
         {
            context.Add(message);
            context.SaveChanges();
            ViewBag.Message = "Mesaj Gönderildi.";
            return View();
         }
         else
         {
            ViewBag.Message = "Mesaj Gönderilemedi.";
         }
         return View(message);
      }
      public IActionResult AboutUs()
      {
         return View();
      }
      public async Task<IActionResult> Index()//İlgili statu ürünlerini döndürür.
      {
         await _hubContext.Clients.All.SendAsync("Yakala", "Ziyaretçi", "Siteye Giriş Yaptı.");
         mpm.SliderProduct = mainProduct.GetProducts("SliderProducts", "Index");
         mpm.NewProduct = mainProduct.GetProducts("NewProducts", "Index");
         mpm.ProductOfDay = mainProduct.GetProductOfDay();
         mpm.SpecialProducts = mainProduct.GetProducts("SpecialProducts", "Index");
         mpm.StarProducts = mainProduct.GetProducts("StarProducts", "Index");
         mpm.FeatureProducts = mainProduct.GetProducts("FeatureProducts", "Index");
         mpm.DiscountedProducts = mainProduct.GetProducts("DiscountedProducts", "Index");
         mpm.HighlightedProducts = mainProduct.GetProducts("HighlightedProducts", "Index");
         mpm.TopsellerProducts = mainProduct.GetProducts("TopsellerProducts", "Index");
         mpm.NotableProducts = mainProduct.GetProducts("NotableProducts", "Index");

         return View(mpm);

      }
      public IActionResult NewProducts()
      {
         mpm.NewProduct = mainProduct.GetProducts("NewProducts", "topmenü"); //Alt Sayfa,menüden yeni ürünler butonu

         return View(mpm);
      }
      //_PartialNewProducts

      public PartialViewResult _PartialNewProducts(string nextpagenumber)
      {
         //nextpagenumber * 4 =kaçıncı üründen başlayacak -Skip ile.
         int pagenumber = Convert.ToInt32(nextpagenumber);
         mpm.NewProduct = mainProduct.GetProducts("NewProducts", "topmenüajax", pagenumber); //Yeni Ürünlerde Daha Fazla butonu için. Ajax istegi
         return PartialView(mpm);
      }

      public IActionResult SpecialProducts()
      {
         mpm.SpecialProducts = mainProduct.GetProducts("SpecialProducts", "topmenü"); //Alt Sayfa,menüden Özel ürünler butonu
         return View(mpm);
      }
      //_PartialNewProducts

      public PartialViewResult _PartialSpecialProducts(string nextpagenumber)
      {
         //nextpagenumber * 4 =kaçıncı üründen başlayacak -Skip ile.
         int pagenumber = Convert.ToInt32(nextpagenumber);
         mpm.SpecialProducts = mainProduct.GetProducts("SpecialProducts", "topmenüajax", pagenumber); //Özel Ürünlerde Daha Fazla butonu için. Ajax istegi
         return PartialView(mpm);
      }
      public IActionResult DiscountedProducts()
      {
         mpm.DiscountedProducts = mainProduct.GetProducts("DiscountedProducts", "topmenü"); //Alt Sayfa,menüden İndirimli ürünler butonu
         return View(mpm);
      }
      //_PartialNewProducts

      public PartialViewResult _PartialDiscountedProducts(string nextpagenumber)
      {
         //nextpagenumber * 4 =kaçıncı üründen başlayacak -Skip ile.
         int pagenumber = Convert.ToInt32(nextpagenumber);
         mpm.DiscountedProducts = mainProduct.GetProducts("DiscountedProducts", "topmenüajax", pagenumber); //İndirimli Ürünlerde scrooll için. Ajax istegi
         return PartialView(mpm);
      }
      public IActionResult HighlightedProducts()
      {
         mpm.HighlightedProducts = mainProduct.GetProducts("HighlightedProducts", "topmenü"); //Alt Sayfa,menüden ÖneÇıkan ürünler butonu
         return View(mpm);
      }
      //_PartialNewProducts

      public PartialViewResult _PartialHighlightedProducts(string nextpagenumber)
      {
         //nextpagenumber * 4 =kaçıncı üründen başlayacak -Skip ile.
         int pagenumber = Convert.ToInt32(nextpagenumber);
         mpm.HighlightedProducts = mainProduct.GetProducts("HighlightedProducts", "topmenüajax", pagenumber); //ÖneÇıkan Ürünlerde scrool için. Ajax istegi
         return PartialView(mpm);
      }

      public IActionResult TopsellerProducts(int page = 1, int pageSize = 4)
      {
         PagedList<Product> model = new PagedList<Product>(
            context.Products.Where(x => x.Active).OrderByDescending(x => x.TopSeller),
            page,
            pageSize);

         return View("TopsellerProducts", model);
      }


      public IActionResult CategoryPage(int id)
      {
         mpm.ProductsByCategory = mainProduct.GetProductsByCategoryId(id);



         var category = context.Categories.FirstOrDefault(x => x.CategoryID == id);
         var CountID = Enumerable.Range(1, 500);
         if (CountID.Contains(id))
         {
            ViewData["TitlePage"] = category?.CategoryName;
         }

         return View(mpm);
      }

      public IActionResult SupplierPage(int id)
      {
         mpm.ProductsBySupplier = mainProduct.GetProductsBySupplierId(id);

         var supplier = context.Suppliers.FirstOrDefault(x => x.SupplierID == id);

         switch (id)
         {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
            case 15:
            case 16:
            case 17:
            case 18:
               ViewData["TitlePage"] = supplier?.BrandName;
               break;
         }
         return View(mpm);

      }
      //Herhangi bir sepete ekle durumunda çalısır
      public IActionResult CartProcess(int id)// Sepete ekle butonu için çalışacaktır.
      {    //Higligted kolonu 1 artırılacak count++;
         mainProduct.HihlightedPlus(id);

         order.ProductID = id;
         order.Quantity = 1;

         var cookieOptions = new CookieOptions();//Nesne oluşturduk, instance aldık.
                                                 //Çerez poitikası ayarlaması =Cookide(Tarayıcıda tutulur)

         var cookie = Request.Cookies["sepetim"];//Tarayıcıda sepetim isminde Cookie varmı diye istek atılır.
         if (cookie == null)
         {
            //Kullanıcı sisteme ilk defa ürün ekleyecek
            cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(1);//Bir günlük çerez değeri tanımladık, Geriye dönük  gün  için  addday içine - (eksili) değer verleblr
            cookieOptions.Path = "/";
            order.Sepet = "";
            order.AddToCArt(id.ToString());//Sepete ekle metodu olacak
            Response.Cookies.Append("sepetim", order.Sepet, cookieOptions);
            HttpContext.Session.SetString("Message", "Ürün Sepetinize Eklendi");
            TempData["Message"] = "Ürün Sepetinize Eklendi";

         }
         else
         {
            //Kullanıcı daha önce sepetine ürün eklemiş.Yani tarayıcıdaki sepetim içeriğini propertye gönderiyoruz.
            order.Sepet = cookie;
            //Aynı Ürün daha önceden sepete eklenmiş mi,kontol ediyoruz.
            if (order.AddToCArt(id.ToString()) == false)
            {
               //Eğer false ise; demekki eklenmemiş
               Response.Cookies.Append("sepetim", order.Sepet, cookieOptions);
               cookieOptions.Expires = DateTime.Now.AddDays(1);
               //HttpContext.Session.SetString("Message", "Ürün Sepetinize Eklendi");
               TempData["Message"] = "Ürün Sepetinize Eklendi";

            }
            else
            {
               // HttpContext.Session.SetString("Message", "Ürün sepetinizde zaten mevcut");
               TempData["Message"] = "Ürün sepetinizde zaten mevcut";
            }
         }

         //Ürünü sepete ekledikten sonra hangi sayfadaysa o sayfaya tekrar gönderyruz
         string url = Request.Headers["Referer"].ToString();
         return Redirect(url);
      }

      public async Task<IActionResult> Details(int id)
      {
         //EFCore
         //mpm.ProductDetails=context.Products.FirstOrDefault(p => p.ProductID == id);
         //select*from Products where ProductID=id __>Ado.net veya dapper
         //id'si 4 olan ürünün bütün kolon (sutun) bilgilerini getir
         mpm.ProductDetails = (from p in context.Products
                               where p.ProductID == id
                               select p)
                             .FirstOrDefault();

         //LINQU
         mpm.CategoryName = (from p in context.Products
                             join c in context.Categories
                             on p.CateoryID equals c.CategoryID
                             where p.ProductID == id
                             select c.CategoryName).FirstOrDefault();
         //LINQU
         mpm.BrandName = (from p in context.Products
                          join s in context.Suppliers
                          on p.SupplierID equals s.SupplierID
                          where p.ProductID == id
                          select s.BrandName).FirstOrDefault();

         //select*from products where Related=x and ProductID != id
         mpm.RelatedProduct = context.Products.Where(p => p.Related == mpm.ProductDetails!.Related &&
         p.ProductID
         != id).ToList();

         mpm.comments = context.Comments.Where(p => p.ProductID == id).OrderByDescending(c=>c.AddDate).ToList();

         mainProduct.HihlightedPlus(id);
         //Higligted kolonu 1 artırılacak count++;
         ViewBag.Categories = await c.GetCategoriesAsync();
         ViewBag.Supliers = await s.GetSuppliersAsync();
         return View(mpm);
      }
      //PROJENİN SAĞUST KOŞESİNDEN SEPET SAYFAMA GIT TIKLANINCA SEPTİM ÜRÜN SİLERKEN SİL BUTONUNA TIKLAYINCA


      public IActionResult Cart()
      {


         if (HttpContext.Request.Query["scid"].ToString() != "")
         {
            //SAYFADA ÜRÜN SİLERKEN SİL BUTONUNA TIKLANMINCA scid GÖNDERECEĞİZ
            //sepetim cookie(Çerez) sinden ürün silerek Cart.cshtml YÜKLENECEK
            int scid = Convert.ToInt32(HttpContext.Request.Query["scid"].ToString());
            order.Sepet = Request.Cookies["sepetim"];//tarayıcıdan aldık propert ye koyduk
            order.DeleteFromMyCart(scid.ToString());

            var cookieOptions = new CookieOptions();
            cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("sepetim", order.Sepet, cookieOptions);
            //TempData["Message"] = "Ürün Sepetinizden silindi...";
            //cart.cshtml ürünleri foreach ile dönüp gösterecek...
            //Şimdi o bilgileri hazırlıyoruz.
            List<Cls_Order> sepet = order.GetMyCart();
            ViewBag.Sepet = sepet;
            ViewBag.sepet_tablo_detay = sepet;
         }
         else
         {
            //PROJENİN SAĞUST KOŞESİNDEN SEPET SAYFAMA GIT TIKLANINCA
            //sepetim cookie(Çerez) sinden hiç bir şey değiştirmeden  Cart.cshtml YÜKLENECEK
            var cookie = Request.Cookies["sepetim"];
            List<Cls_Order> sepet;
            var cookieOptions = new CookieOptions();
            if (cookie == null)
            {
               order.Sepet = "";
               sepet = order.GetMyCart();
               ViewBag.Sepet = sepet;
               ViewBag.sepet_tablo_detay = sepet;
            }
            else
            {
               order.Sepet = Request.Cookies["sepetim"];
               sepet = order.GetMyCart();
               ViewBag.Sepet = sepet;
               ViewBag.sepet_tablo_detay = sepet;
            }
         }
         return View();
      }

      [HttpGet]
      public IActionResult Order()
      {

         //Kullanıcı Giriş Yapmış mı
         if (HttpContext.Session.GetString("Email") != null)
         {
            //Bu kullanıcı login girişi yapmış ve benden Email isminde bir oturum almış.
            User? user = Cls_User.GetUserInfo(HttpContext.Session.GetString("Email"));
            return View(user);
         }
         else
         {

            return RedirectToAction("Login");
         }



      }


      [HttpPost]
      public IActionResult Order(Order order, IFormCollection frm) // Burada değişiklik olabilir
      {
         if (frm["txt_tckimlikno"] != "")
         {
            string? tckimlikno = frm["txt_tckimlikno"];
         }
         if (Request.Form["txt_vergino"] != "")
         {
            string? vergino = Request.Form["txt_vergino"];
         }

         string? kredikartno = Request.Form["txt_vergino"];
         string? kredikartay = Request.Form["kredikartay"];
         string? kredikartyil = Request.Form["kredikartyil"];
         string? kredikartcvv = Request.Form["kredikartcvv"];
         /*
         //payu, paytr, paratika, iyzico

         //Api
         //Eticaret sitesinde olması gerekenler :
         //Gizlilik söz. iade koşulları, telefon numarası//Fraud un önüne geçmek için. Yani Dolandırıcılık.


         NameValueCollection data = new NameValueCollection();
         string payu_url = "https://www.siteadi.com/backref";
         data.Add("BACK_REF",payu_url);
         data.Add("CC_CVV", kredikartcvv);
         data.Add("CC_NUMBER", kredikartno);
         data.Add("CC_MOUNT", kredikartay);
         data.Add("CC_YEAR", kredikartyil);

         var deger = "";


         foreach (var item in data)
         {
            var value=item as string;
            var byteCount=Encoding.UTF8.GetByteCount(data.Get(value));
            deger += byteCount + data.Get(value);

         }
         var signatureKey = "Bize Verilen SECRET_KEY burada yazılacak";
         var hash = HashWithSignature(deger,signatureKey);
         data.Add("ORDER_HASH",  hash);
         var x = POSTFormPayu("https://secure.payu.com.tr/order/......",data);
         if (x.Contains("<STATUS>SUCCESS</STATUS>") && x.Contains("<RETURN_CODE>3DS_ENROLLED</RETURN_CODE>"))
         {
            //Sanal kart ile alışveriş yapıldı.
         }else 
         {
           
            if (x.Contains("<STATUS>SUCCESS</STATUS>") && x.Contains("<RETURN_CODE>AUTHORIZED</RETURN_CODE>"))
            {
               //Gerçek kartla alışveriş 
            }
            
         }
  

         */

         return RedirectToAction("backref");
      }

      public string POSTFormPayu(string url, NameValueCollection data)
      {
         return "";
      }
      public string HashWithSignature(string deger1, string deger2)
      {
         return "";
      }


      public IActionResult backref()
      {
         OrderConfirm();
         return RedirectToAction("Confirm");
      }

      public static string OrderGroupGUID = "";

      public static string cevap = ""; //netgsm'den dönen cevap için değişken

      //
      public IActionResult OrderConfirm()
      {
         //cookie sepetteki siparişi alıcam, orders tablosuna işlenicek, ve aynı zamanda sepeti boşaltmamız gerekecek.
         var cookieOptions = new CookieOptions();

         var cookie = Request.Cookies["sepetim"];//Tarayıcıda sepetim isminde Cookie varmı diye istek atılır.
         if (cookie != null)
         {
            order.Sepet = cookie;
            OrderGroupGUID = order.WriteFromCookieToTable(HttpContext.Session.GetString("Email").ToString());
            cookieOptions.Expires = DateTime.Now.AddDays(1);//Bir günlük çerez değeri tanımladık, Geriye dönük  gün  için  addday içine - (eksili) değer verleblr
            Response.Cookies.Delete("sepetim");
            cevap = Cls_User.Send_Sms(OrderGroupGUID);
            Cls_User.Send_Email(OrderGroupGUID);



            //Kullanıcıya sms ve mail gönderme
            //Cls_User.Send_Sms(OrderGroupGUID); 
            //Cls_User.Send_Email(OrderGroupGUID); 

         }
         return RedirectToAction("Confirm");
      }
      public IActionResult Confirm()
      {
         ViewBag.sepetorder = OrderGroupGUID;
         ViewBag.cevap = cevap; //netgsm'den dönen cevap
         return View();
      }



      public IActionResult Register()
      {
         return View();
      }

      [HttpPost]
      public IActionResult Register(User user)
      {
         string answer = Cls_User.AddUser(user);
         if (answer == "Başarılı Kayıt oluşturuldu")
         {
            TempData["Message"] = "Bilgileriniz başarıyla kaydedildi";
            return RedirectToAction("Login");
         }
         else if (answer == "Email Zaten kayıtlıdır")
         {
            TempData["Message"] = "Email Zaten kayıtlıdır,Tekrar Deneyin...";
            return View();
         }
         else
         {
            TempData["Message"] = "Hata! ";
            return View();
         }

      }

      public IActionResult Login()
      {
         string url = Request.Headers["Referer"].ToString();
         HttpContext.Session.SetString("url", url);
         return View();
      }

      [HttpPost]
      public IActionResult Login(User user)
      {
         string answer = Cls_User.UserControl(user);
         if (answer == "yanlış")
         {
            //EmailVeşifre kayıtlı değilse buraya girecek
            TempData["Message"] = "Hata!.. Bilgileriniz Yanlıştır, Tekrar deneyin.";
            return View();
         }
         else if (answer == "admin")
         {
            HttpContext.Session.SetString("Admin", answer);
            return RedirectToAction("Login", "Admin");
         }
         else if (answer == "Hata")
         {
            TempData["Message"] = "Bir Hata oluştu,Tekrar deneyiniz...";
            return View();
         }
         else
         {
            HttpContext.Session.SetString("Email", answer);

            if (HttpContext.Session.GetString("url") != null)
            {
               string? url = HttpContext.Session.GetString("url");
               HttpContext.Session.Remove("url");
               return Redirect(url);
            }

            return RedirectToAction("Index", "Home");
         }


      }

      public IActionResult Logout()
      {
         HttpContext.Session.Remove("Email");
         HttpContext.Session.Remove("Admin");
         HttpContext.Session.Remove("url");
         return RedirectToAction("Login");
      }

      //AddTocart
      [HttpPost]
      public IActionResult AddTocart(int id)// Sepete ekle butonu için çalışacaktır.
      {    //Higligted kolonu 1 artırılacak count++;
         string message = "";
         mainProduct.HihlightedPlus(id);

         order.ProductID = id;
         order.Quantity = 1;

         var cookieOptions = new CookieOptions();//Nesne oluşturduk, instance aldık.
                                                 //Çerez poitikası ayarlaması =Cookide(Tarayıcıda tutulur)

         var cookie = Request.Cookies["sepetim"];//Tarayıcıda sepetim isminde Cookie varmı diye istek atılır.
         if (cookie == null)
         {
            //Kullanıcı sisteme ilk defa ürün ekleyecek
            cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(1);//Bir günlük çerez değeri tanımladık, Geriye dönük  gün  için  addday içine - (eksili) değer verleblr
            cookieOptions.Path = "/";
            order.Sepet = "";
            order.AddToCArt(id.ToString());//Sepete ekle metodu olacak
            Response.Cookies.Append("sepetim", order.Sepet, cookieOptions);
            //HttpContext.Session.SetString("Message", "Ürün Sepetinize Eklendi");
            //TempData["Message"] = "Ürün Sepetinize Eklendi";

         }
         else
         {
            //Kullanıcı daha önce sepetine ürün eklemiş.Yani tarayıcıdaki sepetim içeriğini propertye gönderiyoruz.
            order.Sepet = cookie;
            //Aynı Ürün daha önceden sepete eklenmiş mi,kontol ediyoruz.
            if (order.AddToCArt(id.ToString()) == false)
            {
               //Eğer false ise; demekki eklenmemiş
               Response.Cookies.Append("sepetim", order.Sepet, cookieOptions);
               cookieOptions.Expires = DateTime.Now.AddDays(1);
               //HttpContext.Session.SetString("Message", "Ürün Sepetinize Eklendi");
               //TempData["Message"] = "Ürün Sepetinize Eklendi";
               message = "Ürün sepete eklendi";

            }
            else
            {
               // HttpContext.Session.SetString("Message", "Ürün sepetinizde zaten mevcut");
               message = "Ürün sepette zaten mevcut";
            }
         }

         // cookie = Request.Cookies["sepetim"];
         //int itemCount = (cookie != null) ? cookie.Split('&').Length : 0;

         return Json(new { success = true, message = message });
      }


      public IActionResult CartSummary()
      {
         return ViewComponent("CartSummary");
      }


      public ActionResult MyOrders(int id)
      {
         if (HttpContext.Session.GetString("Email") != null)
         {
            List<MyOrderViewModel> orders = order.SelectMyOrder(HttpContext.Session.GetString("Email").ToString());
            return View(orders);
         }
         return RedirectToAction("Login");
      }


      public async Task<IActionResult> DetailedSearch()
      {
         ViewBag.Categories = await c.GetCategoriesAsync();
         ViewBag.Supliers = await s.GetSuppliersAsync();
         return View();
      }



      public IActionResult DpProduct(int CategoryID, string[] SupplierID, string price, string isInStock)
      {

         price = price.Replace(" ", "").Replace("TL", "");//200 - 2000 arasındaki boşluk ve PArabirimi siliindi.

         string[] priceArray = price.Split("-");
         string startmoney = priceArray[0];
         string endmoney = priceArray[1];

         //string sign = ">";
         //if (isInStock == "0")
         //{
         //   sign = ">=";
         //}

         string SupplierValue = "";

         for (int i = 0; i < SupplierID.Length; i++)
         {
            if (i != 0)

               //select * from Products where (SupplierID=2 or SupplierID=3 or SupplierID=4)
               SupplierValue += " or ";

            SupplierValue += "SupplierID = " + SupplierID[i];
            //if (i==0)
            //   //select * from Products where (SupplierID = 2)
            //   SupplierValue = "SupplierID = " + SupplierID[i];
            //else
            //   //select * from Products where (SupplierID=2 or SupplierID=3 or SupplierID=4)
            //   SupplierValue += " or SupplierID= " + SupplierID[i];
         }
         if (SupplierValue != "")
         {
            SupplierValue = "(" + SupplierValue + ") and ";
         }



         string query = $"select * from Products where CateoryID= {CategoryID} and {SupplierValue} (UnitPrice >= {startmoney} and UnitPrice <= {endmoney}) and Stock >= {isInStock} order by AddDate Desc";
         ViewBag.Products = order.Select_Products_DetailsSearch(query);

         return View();
      }


      public PartialViewResult gettingProducts(string id)
      {
         id = id.ToUpper(new System.Globalization.CultureInfo("tr-TR"));
         List<QuickSearcViewModel> ulist = Cls_Product.gettingSearchProducts(id);
         //string json=JsonConvert.SerializeObject(ulist);
         //var response=JsonConvert.DeserializeObject<List<Search>>(json);
         return PartialView(ulist);
      }






   }

}
