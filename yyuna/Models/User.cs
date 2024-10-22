using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yyuna.Models
{
   public class User
   {
      [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int UserID { get; set; }

      [StringLength(100)]
      public string? NameSurname { get; set; }

      [Required]
      [StringLength(100)]
      [DataType(DataType.EmailAddress)]
      public string? Email { get; set; }

      [Required]
      [StringLength(100)]
      [DataType(DataType.Password)]
      public string? Password { get; set; }


      public string? Telephone { get; set; }

      public string? InvoicesAddress { get; set; }//Fatura adresi

      public bool IsAdmin { get; set; }


      public bool Active { get; set; }


   }
}
