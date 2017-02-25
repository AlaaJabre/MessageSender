using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;

namespace MessageSender
{
    public class Helper
    {
        public static List<Email> ProcessEmails(string filePath, MessagingEntities dbContext)
        {
            var emailsText = File.ReadAllLines(filePath);
            var existing = dbContext.Emails.Where(c => emailsText.Contains(c.Email1)).ToList();
            var nonexisting = emailsText.Except(existing.ConvertAll(c => c.Email1)).ToList().ConvertAll(c => new Email() { Email1 = c });
            dbContext.Emails.AddRange(nonexisting);
            dbContext.SaveChanges();

            nonexisting.AddRange(existing);
            return nonexisting;
        }

        public static string StoreFile(HttpPostedFileBase toUpload, HttpServerUtilityBase server)
        {
            var fileName = Path.GetFileName(toUpload.FileName);
            var path = Path.Combine(server.MapPath("~/App_Data/Uplaods"), fileName);
            toUpload.SaveAs(path);
            return path;
        }

        public static MessagingGroup CreateGroup(string path, List<Email> emailObjects)
        {
            MessagingGroup group = new MessagingGroup() { FilePath = path };
            foreach (var item in emailObjects)
            {
                group.Emails.Add(item);
            }

            return group;
        }

        public static Message CreateMessage(string messageText, MessagingGroup group)
        {
            return new Message()
            {
                GroupId = group.GroupId,
                MessageDate = DateTime.Now.Date,
                MessageTime = DateTime.Now.TimeOfDay,
                MessageText = messageText
            };
        }

        public static void SendMessage(string recipient, string subject, string body)
        {
            MailMessage emailMessage = new MailMessage("mail@host.com", recipient);
            emailMessage.Subject = subject;
            emailMessage.Body = body;
            emailMessage.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.host.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential();
            NetworkCred.UserName = "mail@host.com";
            NetworkCred.Password = "password";
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            //smtp.Send(emailMessage);            
        }

        public static bool IsValid(string messsage)
        {
            bool condition1 = messsage.Length >= 20;
            bool condition2 = messsage.Length <= 160;
            bool condition3 = !Regex.IsMatch(messsage, "(?<SYMBOL>[$€#@&%]){1}\\s*");

            return condition1 & condition2 & condition3;
        }
    }
}