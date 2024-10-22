using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace yyuna.ViewModel
{
   public class MyOrderViewModel
   {
      [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int OrderID { get; set; }

      public int ProductID { get; set; }

      public DateTime OrderDate { get; set; }

      public string? OrderGroupGUID { get; set; }
      public int Quantity { get; set; }

      public int UserID { get; set; }
      public string? ProductName { get; set; }

      public decimal UnitPrice { get; set; }
      public string? PhotoPath { get; set; }




   }
}
