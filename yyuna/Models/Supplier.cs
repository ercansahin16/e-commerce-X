using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yyuna.Models
{
   public class Supplier//Marka
   {
      [DisplayName("Marka ID")]
      [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int SupplierID { get; set; }

      [Required]
      [StringLength(100)]
      [DisplayName("Marka Adı")]
      public string? BrandName { get; set; }//MarkaAdı

      [DisplayName("Resim")]
      public string? PhotoPath { get; set; }//Marka Resmi

      [DisplayName("Aktif/Pasif")]
      public bool Active { get; set; }





   }
}
