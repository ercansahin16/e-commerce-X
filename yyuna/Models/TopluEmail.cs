using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yyuna.Models
{
   public class TopluEmail
   {
      [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int TopluMailID { get; set; }
      public string? Email { get; set; }
   }
}
