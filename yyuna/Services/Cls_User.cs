using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Text;
using XSystem.Security.Cryptography;
using yyuna.Data;
using yyuna.Models;

namespace yyuna.Services
{
   public class Cls_User
   {

      Yazilima15MartETicaretContext context = new();


      public async Task<User> LoginControl(User user)

      {
         var value = MD5Sifrele(user.Password);

         User? usr = await context
            .Users
            .FirstOrDefaultAsync(u =>
            u.Email == user.Email &&
            u.Password == value &&
            u.IsAdmin == true &&
            u.Active == true);
         return usr;
      }

      // User? user = Cls_User.GetUserInfo(HttpContext.Session.GetString("Email"));
      public static User? GetUserInfo(string email)
      {
         using (Yazilima15MartETicaretContext context = new())
         {
            User? user = context.Users.FirstOrDefault(x => x.Email == email);
            return user;
         }

      }
      //   string answer = Cls_User.AddUser(user);
      public static string AddUser(User user)
      {
         using (Yazilima15MartETicaretContext context = new())
         {
            try
            {
               User? usr = context.Users.FirstOrDefault(u => u.Email == user.Email);
               if (usr != null)//Usr Eğer null ise demekki bu mail daha önceden kayıtlıdır.
               {
                  return "Email Zaten kayıtlıdır";
               }
               else
               {

               }
               {
                  user.Active = true;
                  user.IsAdmin = false;
                  user.Password = MD5Sifrele(user.Password);
                  context.Users.Add(user);
                  context.SaveChanges();
                  return "Başarılı Kayıt oluşturuldu";
               }
            }
            catch (Exception)
            {

               return "Başarısız Kayıt";
            }
         }
      }


      //MD5Sifrele Şifrleme motodu(Şuan bunu kullandık)
      public static string MD5Sifrele(string value)
      {
         MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
         byte[] btr = Encoding.UTF8.GetBytes(value);
         btr = md5.ComputeHash(btr);

         //Birleştrime yapılma şaması
         StringBuilder sb = new StringBuilder();
         foreach (byte item in btr)
         {
            sb.Append(item.ToString("x2").ToLower());
         }
         return sb.ToString();
      }


      //ShaHash Şifreleme Yöntemi
      public static string SHA256Sifrele(string password)
      {
         using (SHA256 sha256Hash = SHA256.Create())
         {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
               builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
         }
      }

      //string answer = Cls_User.UserControl(user);
      //Giriş yapcak olan user mi admin mi işi yaptırılacak
      public static string UserControl(User user)
      {
         using (Yazilima15MartETicaretContext context = new())
         {
            string answer = "";
            try
            {
               string md5sifrele = MD5Sifrele(user.Password);
               User? usr = context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == md5sifrele && u.Active);

               if (usr == null)
               {
                  //Buraya girerse demekki ya şifre ya kullanıcı adı ya da active değildir. yani bunlardan en az birisi false oldupu durumlarda buraya girecek.
                  answer = "yanlış";

               }
               else
               {
                  //Email ,şifre doğru girilmiş ve aynı zamanda aktifir.
                  //O zaman mı admin yoksa user mi, onu sorgulayacagız.
                  if (usr.IsAdmin)//Admin mi Yani True mi.
                  {
                     answer = "admin";
                  }
                  else
                  {
                     answer = usr.Email;
                  }
               }
            }
            catch (Exception)
            {

               answer = "Hata";
            }

            return answer;
         }
      }
      public static string Send_Sms(string OrderGroupGUID)
      {
         Order? order;
         User? user;
         string content;
         string? telefon;

         using (Yazilima15MartETicaretContext context = new())
         {
            order = context.Orders.FirstOrDefault(o => o.OrderGroupGUID == OrderGroupGUID);

            user = context.Users.FirstOrDefault(u => u.UserID == order.UserID);

            //Sayın Recep Şamil Çiftçi, 29.08.2024 tarihinde 29082024215730 nolu siparişiniz alınmıstır

            content = $"Sayın {user?.NameSurname}, {order?.OrderDate} tarihinde {OrderGroupGUID} nolu siparişiniz alınmıstır";

            telefon = user?.Telephone;
         }

         string ss = "";
         ss += "<?xml version='1.0' encoding='UTF-8'?>";
         ss += "<mainbody>";
         ss += "<header>";
         ss += "<company dil='TR'>Netgsm</company>";
         ss += "<usercode>8503096835</usercode>";
         ss += "<password>738FC_B</password>";
         ss += "<type>1:n</type>";
         ss += "<msgheader>8503096835</msgheader>";
         ss += "</header>";
         ss += "<body>";
         ss += $"<msg><![CDATA[{content}]]></msg><no>0{telefon}</no>";
         ss += "</body>";
         ss += "</mainbody>";

         return XMLPOST("https://api.netgsm.com.tr/sms/send/xml", ss);
      }

      static string XMLPOST(string PostAddress, string xmlData)
      {
         try
         {
            WebClient wUpload = new WebClient();
            HttpWebRequest request = WebRequest.Create(PostAddress) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] bPostArray = Encoding.UTF8.GetBytes(xmlData);
            byte[] bResponse = wUpload.UploadData(PostAddress, "POST", bPostArray);
            char[] sReturnChars = Encoding.UTF8.GetChars(bResponse);
            string sWebPage = new string(sReturnChars);
            return sWebPage;
         }
         catch
         {

            return "-1";
         }
      }

      public static void Send_Email(string OrderGroupGUID)
      {
         using (Yazilima15MartETicaretContext contex = new())
         {
            Order? order = contex.Orders.FirstOrDefault(o => o.OrderGroupGUID == OrderGroupGUID);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("no-reply@iamcodingaround.com", "Eticaret Bilgi");

            string subject = "Siparişiniz Hk.";


            User? user = contex.Users.FirstOrDefault(x => x.UserID == order!.UserID);

            mailMessage.To.Add(user.Email);

            string content = $"Sayın {user.NameSurname}, {order.OrderDate} tarihinde {OrderGroupGUID} nolu siparişiniz alınmıştır.RAW";

            mailMessage.Body = content;

            mailMessage.Subject = subject;

            SmtpClient smtp = new SmtpClient();

            smtp.Credentials = new NetworkCredential("no-reply@iamcodingaround.com", "8rh.I897t=B@fTZ.");

            smtp.Port = 587;
            smtp.Host = "mail.kurumsaleposta.com";

            try
            {
               smtp.Send(mailMessage);
            }
            catch (Exception)
            {

               //Email gonderilemediği zaman burada bilgilendirme felan benzer iş yapılabilir.
            }

         }
      }









   }

}
