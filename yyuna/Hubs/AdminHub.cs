using Microsoft.AspNetCore.SignalR;

namespace yyuna.Hubs
{
   public class AdminHub : Hub
   {
      //Buraya metodlarımız gelecek.

      public async Task AnlıkHareket(string kullanici, string aktivite)
      {
         await Clients.All.SendAsync("Yakala", kullanici, aktivite);
      }




   }
}
