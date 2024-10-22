using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using yyuna.Data;
using yyuna.Models;
using yyuna.Services;

namespace yyuna.Controllers
{
   public class AdminController : Controller
   {
      Cls_User u = new();
      Cls_Category c = new();
      Cls_Suplier s = new();
      Cls_Status st = new();
      Cls_Product pr = new();
      Yazilima15MartETicaretContext context = new();

      #region K  A  T  E  G  O  R  I  L  E  R


      public IActionResult Login()
      {
         return View();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Login([Bind("Email,Password,NameSurname")] User user)
      {

         if (ModelState.IsValid)
         {

            User? usr = await u.LoginControl(user);
            if (usr != null)
            {
               var claims = new List<Claim>
               {
                  new Claim(ClaimTypes.Role,"Admin")
               };
               var ClaimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
               var authProperties = new AuthenticationProperties();
               await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ClaimsIdentity), authProperties
                  );




               return RedirectToAction("Index", "Admin");
            }
         }
         else
         {
            ViewBag.error = "Giriş Bilgileri yanlıştır.";
         }
         return View();
      }

      [HttpGet]
      public async Task<IActionResult> Logout()
      {
         await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
         return RedirectToAction("Login", "Admin");
      }
      [Authorize(Roles = "Admin")]
      public IActionResult Index()
      {
         return View();
      }


      [Authorize(Roles = "Admin")]
      public async Task<IActionResult> CategoryIndex()
      {
         List<Category> categories = await c.GetCategoriesAsync();
         return View(categories);
      }

      [Authorize(Roles = "Admin")]
      [HttpGet]
      public IActionResult CategoryCreate()
      {
         CategoryFill();
         return View();
      }

      [Authorize(Roles = "Admin")]
      [HttpPost]
      public IActionResult CategoryCreate(Category category)
      {
         bool answer = Cls_Category.CategoryInsert(category);

         if (answer)
         {
            TempData["Message"] = "Kategori eklendi.";
            // return RedirectToAction(nameof(CategoryCreate));
         }
         else
         {
            TempData["Message"] = "HATA! Kategori Eklenemedi.";
         }


         return RedirectToAction(nameof(CategoryCreate));

      }

      void CategoryFill()//Kategory doldur
      {
         List<Category> categories = c.GetMainCategories();
         ViewData["categoryList"] = categories
             .Select(c => new SelectListItem
             {
                Text = c.CategoryName,
                Value = c.CategoryID.ToString()
             });
      }
      async Task SupplierFill()//Marka doldurma
      {
         List<Supplier> suppliers = await s.GetSuppliersAsync();
         ViewData["suppliersList"] = suppliers
             .Select(s => new SelectListItem
             {
                Text = s.BrandName,
                Value = s.SupplierID.ToString()
             });
      }

      async Task StatusFill()//Durum doldurma
      {
         List<Status> statuses = await st.GetStatusAsync();
         ViewData["statusesList"] = statuses
             .Select(st => new SelectListItem
             {
                Text = st.StatusName,
                Value = st.StatusID.ToString()
             });
      }
      [Authorize(Roles = "Admin")]
      public async Task<IActionResult> CategoryEdit(int? id)
      {
         CategoryFill();

         if (id == null || context.Categories == null)
         {
            return NotFound();
         }
         var category = await c.GetCategoryDetailsAsync(id);

         return View(category);
      }


      [Authorize(Roles = "Admin")]
      [HttpPost]
      public IActionResult CategoryEdit(Category category)
      {

         bool answer = Cls_Category.CategoryUpdate(category);

         if (answer)
         {
            TempData["Message"] = "Kategori Güncellendi.";
            return RedirectToAction(nameof(CategoryIndex));

         }
         else
         {
            TempData["Message"] = "HATA! Kategori Güncellenemedi.";
         }


         return RedirectToAction(nameof(CategoryEdit));

      }
      [Authorize(Roles = "Admin")]
      public async Task<IActionResult> CategoryDetails(int? id)
      {
         var category = await c.GetCategoryDetailsAsync(id);
         ViewBag.ctr = category?.CategoryName;
         return View(category);

      }
      [Authorize(Roles = "Admin")]
      [HttpGet]
      public async Task<IActionResult> CategoryDelete(int? id)
      {
         if (id == null || context.Categories == null)
            return NotFound();
         var category = await context.Categories.FirstOrDefaultAsync(x => x.CategoryID == id);

         if (category == null)
            return NotFound();

         return View(category);
      }
      [HttpPost, ActionName("CategoryDelete")]
      public ActionResult CategoryDeleteConfirmed(int id)
      {
         bool answer = Cls_Category.categorydelete(id);

         if (answer)
         {
            TempData["Message"] = "Kategori Silindi.";
            return RedirectToAction(nameof(CategoryIndex));

         }
         else
         {
            TempData["Message"] = "HATA! Kategori Silinemedi.";
         }


         return RedirectToAction(nameof(CategoryDelete));

      }
      #endregion

      #region M   A   R   K   A   L   A   R
      [Authorize(Roles = "Admin")]
      public async Task<IActionResult> SupplierIndex()
      {
         List<Supplier> suppliers = await s.GetSuppliersAsync();
         return View(suppliers);
      }


      [Authorize(Roles = "Admin")]
      [HttpGet]
      public IActionResult SupplierCreate()
      {
         return View();
      }

      [Authorize(Roles = "Admin")]
      [HttpPost]
      public IActionResult SupplierCreate(Supplier supplier)
      {
         bool answer = Cls_Suplier.SupplierInsert(supplier);

         if (answer)
         {
            TempData["Message"] = "Marka eklendi.";

         }
         else
         {
            TempData["Message"] = "HATA! Marka Eklenemedi.";
         }


         return RedirectToAction(nameof(SupplierCreate));

      }

      [Authorize(Roles = "Admin")]
      public async Task<IActionResult> SupplierEdit(int? id)

      {
         if (id == null || context.Suppliers == null)
         {
            return NotFound();
         }
         var supplier = await s.GetSuppliersDetailsAsync(id);
         return View(supplier);
      }
      [Authorize(Roles = "Admin")]
      [HttpPost]
      public IActionResult SupplierEdit(Supplier supplier)
      {
         //Kullanıcı editleme yaparken foto eski haliyle kalsın diye if lendi.Eski resmi sabit kalacak. değiştirmek istesede değişecek zaten.
         if (supplier.PhotoPath == null)
         {
            string? photoPath = context.Suppliers.FirstOrDefault(s => s.SupplierID == supplier.SupplierID)?.PhotoPath;
            supplier.PhotoPath = photoPath;
         }
         bool answer = Cls_Suplier.SupplierUpdate(supplier);

         if (answer)
            TempData["Message"] = supplier.BrandName + " " + "Markası güncellendi";
         else
            TempData["Message"] = "HATA!!!" + supplier.BrandName + " " + "Markası güncellenemedi";

         return RedirectToAction(nameof(SupplierIndex));
      }


      [Authorize(Roles = "Admin")]
      public async Task<IActionResult> SupplierDetails(int? id)
      {
         var supplier = await s.GetSuppliersDetailsAsync(id);
         ViewBag.spl = supplier?.BrandName;
         return View(supplier);
      }
      [Authorize(Roles = "Admin")]
      [HttpGet]
      public async Task<IActionResult> SupplierDelete(int? id)
      {
         if (id == null || context.Suppliers == null)
            return NotFound();
         var supplier = await context.Suppliers.FirstOrDefaultAsync(x => x.SupplierID == id);

         if (supplier == null)
            return NotFound();

         return View(supplier);
      }
      [Authorize(Roles = "Admin")]
      [HttpPost, ActionName("SupplierDelete")]
      public ActionResult SupplierDeleteConfirmed(int id)
      {
         bool answer = Cls_Suplier.supplierdelete(id);

         if (answer)
         {
            TempData["Message"] = "Marka Silindi.";
            return RedirectToAction(nameof(SupplierIndex));

         }
         else
         {
            TempData["Message"] = "HATA! Marka Silinemedi.";
         }


         return RedirectToAction(nameof(SupplierDelete));

      }






      #endregion

      #region D   U   R   U   M

      [Authorize(Roles = "Admin")]
      public async Task<IActionResult> StatusIndex()
      {
         List<Status> statuses = await st.GetStatusAsync();
         return View(statuses);
      }
      [Authorize(Roles = "Admin")]
      [HttpGet]
      public IActionResult StatusCreate()
      {
         return View();
      }
      [Authorize(Roles = "Admin")]
      [HttpPost]
      public IActionResult StatusCreate(Status status)
      {
         bool answer = Cls_Status.StatusInsert(status);
         if (answer)
         {
            TempData["Message"] = "Durum Eklendi";

         }
         else
            TempData["Message"] = "HATA! Durum Eklenemedi.";

         return RedirectToAction(nameof(StatusCreate));

      }


      [Authorize(Roles = "Admin")]
      public async Task<IActionResult> StatusEdit(int? id)

      {
         if (id == null || context.Statuses == null)
         {
            return NotFound();
         }
         var status = await st.GetStatusDetailsAsync(id);
         return View(status);
      }
      [Authorize(Roles = "Admin")]
      [HttpPost]
      public IActionResult StatusEdit(Status status)
      {

         bool answer = Cls_Status.StatusUpdate(status);

         if (answer)
            TempData["Message"] = status.StatusName + " " + "Markası güncellendi";
         else
            TempData["Message"] = "HATA!!!" + status.StatusName + " " + "Markası güncellenemedi";

         return RedirectToAction(nameof(StatusIndex));
      }

      [Authorize(Roles = "Admin")]
      public async Task<IActionResult> StatusDetails(int? id)
      {
         var status = await st.GetStatusDetailsAsync(id);
         ViewBag.st = status?.StatusName;
         return View(status);
      }
      [Authorize(Roles = "Admin")]
      [HttpGet]
      public async Task<IActionResult> StatusDelete(int? id)
      {
         if (id == null || context.Statuses == null)
            return NotFound();
         var status = await context.Statuses.FirstOrDefaultAsync(x => x.StatusID == id);

         if (status == null)
            return NotFound();

         return View(status);
      }
      [HttpPost, ActionName("StatusDelete")]
      public ActionResult StatusDeleteConfirmed(int id)
      {
         bool answer = Cls_Status.statusdelete(id);

         if (answer)
         {
            TempData["Message"] = "Durum Silindi.";
         }
         else
         {
            TempData["Message"] = "HATA! Durum Silinemedi.";
         }


         return RedirectToAction("StatusIndex");

      }













      #endregion

      #region U  R  U  N  L  E  R
      [Authorize(Roles = "Admin")]
      public async Task<IActionResult> ProductIndex()
      {
         List<Product> products = await pr.GetProductsAsync();
         return View(products);

      }
      [Authorize(Roles = "Admin")]
      [HttpGet]
      public async Task<IActionResult> ProductCreate()
      {
         CategoryFill();
         await SupplierFill();
         await StatusFill();
         return View();
      }
      [Authorize(Roles = "Admin")]
      [HttpPost]
      public IActionResult ProductCreate(Product product)
      {
         if (ModelState.IsValid)
         {
            bool answer = Cls_Product.ProductInsert(product);
            if (answer)
            {
               TempData["Message"] = product.ProductName + " Ürünü Eklendi";

            }
            else
               TempData["Message"] = "HATA!" + product.ProductName + "Ürünü Eklenemedi.";
         }
         else
            TempData["Message"] = "Zorunlu alanları doldurunuz.";
         return RedirectToAction(nameof(ProductCreate));//HTTPGet
      }
      [Authorize(Roles = "Admin")]
      public async Task<IActionResult> ProductEdit(int? id)
      {
         CategoryFill();
         await SupplierFill();
         await StatusFill();
         if (id == null || context.Products == null)
         {
            return NotFound();
         }
         var product = await pr.GetProductDetailsAsync(id);
         return View(product);
      }
      [Authorize(Roles = "Admin")]
      [HttpPost]
      public async Task<IActionResult> ProductEdit(Product product)
      {
         var ExistProduct = await pr.GetProductDetailsAsync(product.ProductID);//Bu satırda Product ürünlerinin bütün kolonları çağrıldı ve ExistProduct değişkenine atandı.
         if (product.PhotoPath == null)
         {
            //Güncelleme esnasında null olabilecek kolonlara eski değerleri sabit tuttuk.
            product.PhotoPath = ExistProduct?.PhotoPath;
            //Null goingto: HighLated,Topseller,AddDate
            product.HighLighted = ExistProduct!.HighLighted;
            product.AddDate = ExistProduct!.AddDate;
            product.TopSeller = ExistProduct!.TopSeller;
         }
         bool answer = Cls_Product.ProductUpdate(product);
         if (answer)
         {
            TempData["Message"] = product.ProductName + " Ürünü Güncellendi";
            return RedirectToAction(nameof(ProductIndex));
         }
         else
            TempData["Message"] = "HATA!" + product.ProductName + "Ürünü Güncellenemedi.";
         return RedirectToAction(nameof(ProductEdit));//HTTPGet
      }
      [Authorize(Roles = "Admin")]

      [HttpGet]
      public async Task<IActionResult> ProductDelete(int? id)
      {
         if (id == null || context.Products == null)
            return NotFound();
         var product = await context.Products.FirstOrDefaultAsync(pr => pr.ProductID == id);

         if (product == null)
            return NotFound();

         return View(product);
      }
      [Authorize(Roles = "Admin")]

      [HttpPost, ActionName("ProductDelete")]
      public async Task<IActionResult> ProductDeleteConfirmed(int id)
      {
         bool answer = Cls_Product.ProductDelete(id);

         if (answer)
         {
            TempData["Message"] = " Ürün Güncellendi";
            return RedirectToAction(nameof(ProductIndex));
         }
         TempData["Message"] = "HATA! Ürün Güncellenemedi.";

         return RedirectToAction(nameof(ProductDelete));//HTTPGet
      }


      [Authorize(Roles = "Admin")]
      public async Task<IActionResult> ProductDetails(int? id)
      {
         var product = await pr.GetProductDetailsAsync(id);
         ViewBag.pr = product?.ProductName;
         return View(product);
      }








      #endregion

      public IActionResult siparis()
      {
         var orders = context.Orders
             .Include(o => o.User)
             .ToList();
         Console.Write(orders);
         return View(orders);
         
      }
      public IActionResult message()
      {
         var messag=context.Messages.ToList();
         return View(messag);
      }
     

   }
}
