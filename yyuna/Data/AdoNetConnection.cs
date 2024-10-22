using Microsoft.Data.SqlClient;

namespace yyuna.Data
{
   public class AdoNetConnection
   {
      public static SqlConnection baglanti
      {
         get
         {
            SqlConnection sqlcon = new SqlConnection("Server=LENOVA20VE\\SQLEXPRESS;Database=Dbyyuna; Trusted_Connection=True; TrustServerCertificate=True;");
            return sqlcon;
         }
      }
   }
}
