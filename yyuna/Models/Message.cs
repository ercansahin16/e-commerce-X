using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yyuna.Models
{
   public class Message
   {
      [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int MessageID { get; set; }

      public string? namesurname { get; set; }

      public string? email { get; set; }
      public string? Head { get; set; }
      public string? Content { get; set; }




   }
}
