using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yyuna.Models
{
   public class Category
   {
      [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      [DisplayName("Kategori ID")]

      public int CategoryID { get; set; }

      [DisplayName("Parent ID")]
      public int ParentID { get; set; }

      [StringLength(50, ErrorMessage = "En fazla 50 karakter giriniz.")]
      [Required(ErrorMessage = "Kategori Adı Zorunlu Alan")]
      [DisplayName("Kategori Adı")]
      public string? CategoryName { get; set; }

      [DisplayName("Aktif/Pasif")]
      public bool Active { get; set; }










   }

}
