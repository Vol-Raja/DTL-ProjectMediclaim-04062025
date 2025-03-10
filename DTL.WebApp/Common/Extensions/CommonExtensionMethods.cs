using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace DTL.WebApp.Common.Extensions
{
    public static class CommonExtensionMethods
    {
        public static List<T> GetAllPublicConstantValues<T>(this Type type)
        {
            return type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
                .Select(x => (T)x.GetRawConstantValue())
                .ToList();
        }

        public static string ConvertBytesToPDF(this byte[] data,string fileName)
        {
            //BinaryFormatter bf = new BinaryFormatter();
            //MemoryStream ms = new MemoryStream();
            //bf.Serialize(ms, fileContent);
            //bytes = ms.ToArray();
            var path = "wwwroot/temp";
            if (data != null)
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }                
                File.WriteAllBytes(path + "/" + fileName + ".pdf", data);
                return "/temp/" + fileName + ".pdf";
            }
            else
            {
                return string.Empty;
            }
        }

        public static void DeleteTempFile(this string fileName)
        {
            if (File.Exists("wwwroot/temp/" + fileName))
            {
                File.Delete("wwwroot/temp/" + fileName);
            }
        }

        public static string GeneratePassword(this string password)
        {
            string lowers = "abcdefghijklmnopqrstuvwxyz";
            string uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string number = "0123456789";
            string specialChar = "!@#$&*";

            Random random = new Random();

            string generated = "";
            for (int i = 1; i <= 2; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    lowers[random.Next(lowers.Length - 1)].ToString()
                );

            for (int i = 1; i <= 2; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    uppers[random.Next(uppers.Length - 1)].ToString()
                );

            for (int i = 1; i <= 2; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    number[random.Next(number.Length - 1)].ToString()
                );

            for (int i = 1; i <= 2; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    specialChar[random.Next(specialChar.Length - 1)].ToString()
                );

            return new string(generated.ToCharArray().
                OrderBy(s => (random.Next(2) % 2) == 0).ToArray());

        }

        public static void Email(this string htmlString)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("FromMailAddress");
                message.To.Add(new MailAddress("ToMailAddress"));
                message.Subject = "Test";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("FromMailAddress", "password");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
        }
        public static string GenerateCaptcha(this string password)
        {
            string lowers = "abcdefghjkmnpqrstuvwxyz";
            string uppers = "ABCDEFGHIJKLMNPQRSTUVWXYZ";
            string number = "23456789";
           

            Random random = new Random();

            string generated = "";
            for (int i = 1; i <= 2; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    lowers[random.Next(lowers.Length - 1)].ToString()
                );

            for (int i = 1; i <= 2; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    uppers[random.Next(uppers.Length - 1)].ToString()
                );

            for (int i = 1; i <= 2; i++)
                generated = generated.Insert(
                    random.Next(generated.Length),
                    number[random.Next(number.Length - 1)].ToString()
                );

           

            return new string(generated.ToCharArray().
                OrderBy(s => (random.Next(2) % 2) == 0).ToArray());

        }

    }
}
