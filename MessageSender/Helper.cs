using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MessageSender
{
    public class Helper
    {
        public static List<Email> ProcessEmails(string filePath, MessagingEntities dbContext)
        {
            //Extract emails
            //check existed 
            //return list
            return new List<Email>();
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

        public static void SendMessage(List<Email> emailObjects)
        {
            throw new NotImplementedException();
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