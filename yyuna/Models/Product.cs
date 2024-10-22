
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yyuna.Models
{
   public class Product
   {
      [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      [DisplayName("ID")]
      public int ProductID { get; set; }

      [Required]
      [StringLength(100)]
      [DisplayName("Ürün Adı")]
      public string? ProductName { get; set; }


      [DisplayName("Fiyat")]
      //[Column(TypeName = "decimal(18,2)")]
      public decimal UnitPrice { get; set; }

      [DisplayName("Kategori")]
      public int CateoryID { get; set; }


      [DisplayName("Marka")]
      public int SupplierID { get; set; }

      [DisplayName("Stok")]
      public int Stock { get; set; }

      [DisplayName("İndirim")]
      public int Discount { get; set; }

      [DisplayName("Durum")]
      public int StatusID { get; set; }

      [DisplayName("Eklenme Tarihi")]
      public DateTime AddDate { get; set; }

      [DisplayName("Anahtar Kelimeler")]
      public string? Keywords { get; set; }
      private int _Kdv { get; set; }

      [DisplayName("KDV")]
      public int Kdv
      {
         get { return _Kdv; }
         set { _Kdv = Math.Abs(value); }
      }
      [DisplayName("Öne Çıkanlar")]

      public int HighLighted { get; set; }

      [DisplayName("Çok Satanlar")]
      public int TopSeller { get; set; }


      [DisplayName("İlişkili")]
      public int Related { get; set; }//Buna bakanlar buna da baktı

      [DisplayName("Notlar")]
      public string? Notes { get; set; }//Notlar

      [DisplayName("Fotoğraf")]
      public string? PhotoPath { get; set; }

      [DisplayName("Aktif/Pasif")]
      public bool Active { get; set; }

      public int ToplamRate { get; set; }
      public int Oysayisi { get; set; }
      public decimal Ortalama { get; set; }

   }
}
