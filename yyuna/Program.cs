
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text.Unicode;
using yyuna.Hubs;//Alert T�rk�e karakter destekleme i�in.

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region SignalR for
builder.Services.AddSignalR();
#endregion
#region Cokie ayarlar�
//S�re olarak 1 Dakika belirlendi.
builder.Services.AddSession(option =>
{
   option.IdleTimeout = TimeSpan.FromMinutes(10);
});



#endregion
#region Alert T�rk�e karakter destekleme i�in.

builder.Services.AddWebEncoders(o =>
{
   o.TextEncoderSettings = new System.Text.Encodings.Web.TextEncoderSettings(UnicodeRanges.All);
});
#endregion
#region login i�in
//Layoutta session login g�r�n�m� i�in
builder.Services.AddHttpContextAccessor();
#endregion
#region Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
   options.Cookie.Name = "15MartEticaret.Auth";
   options.LogoutPath = "/Admin/Login";
   options.LogoutPath = "/Admin/Login";
   options.AccessDeniedPath = "/Admin/Login";
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
   app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseSession();//Biz ekledik.

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

#region SignalR for
app.MapHub<AdminHub>("/adminHub");
#endregion

app.Run();
