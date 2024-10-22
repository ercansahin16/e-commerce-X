using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yyuna.Models
{
   public class Status
   {
      [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      [DisplayName("Durum ID")]

      public int StatusID { get; set; }
      [StringLength(100)]
      [Required]
      [DisplayName("Durum Adı")]
      public string? StatusName { get; set; }

      [DisplayName("Aktif/Pasif")]
      public bool Active { get; set; }









   }
}
