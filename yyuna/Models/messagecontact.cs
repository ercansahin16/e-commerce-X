using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace yyuna.Models
{
   public class messagecontact
   {
      
         [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
         public int MessageID { get; set; }

         public string? namesurname { get; set; }

         public string? email { get; set; }
         public string? Head { get; set; }
         public string? Content { get; set; }




      
   }
}
