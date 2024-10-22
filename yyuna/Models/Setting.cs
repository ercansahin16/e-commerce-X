using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yyuna.Models
{
   public class Setting
   {
      [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int SettingID { get; set; }

      public string? Telephone { get; set; }

      [EmailAddress]
      public string? Email { get; set; }

      public string? Adress { get; set; }

      public int MainPageCount { get; set; }//Ürünler kaçar kaçar gözterilsin


      public int SubpageCount { get; set; }//Anasayfada ürünleri kaçar kaçar gösterilsin









   }
}
