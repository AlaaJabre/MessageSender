using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MessageSender.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //using (MessagingEntities dbContext = new MessagingEntities())
            //{                                
            //    return View(dbContext.Messages.ToList());
            //}
            return View(new List<Message>()
            {
                new Message() { MessageDate = new DateTime(2010, 1, 5), MessageTime = new TimeSpan(10, 45, 10), MessageText = "This a Test bla bla bla 1" },
                new Message() { MessageDate = new DateTime(2010, 2, 10), MessageTime = new TimeSpan(15, 30, 20), MessageText = "This a Test bla bla bla 2" },
                new Message() { MessageDate = new DateTime(2012, 3, 15), MessageTime = new TimeSpan(20, 15, 10), MessageText = "This a Test bla bla bla 3" },
                new Message() { MessageDate = new DateTime(2012, 4, 20), MessageTime = new TimeSpan(1, 10, 10), MessageText = "This a Test bla bla bla 4" },
                new Message() { MessageDate = new DateTime(2013, 5, 25), MessageTime = new TimeSpan(4, 5, 10), MessageText = "This a Test bla bla bla 5" },
                new Message() { MessageDate = new DateTime(2013, 6, 30), MessageTime = new TimeSpan(2, 50, 10), MessageText = "This a Test bla bla bla 6" },
                new Message() { MessageDate = new DateTime(2014, 7, 3), MessageTime = new TimeSpan(16, 55, 10), MessageText = "This a Test bla bla bla 7" },
                new Message() { MessageDate = new DateTime(2014, 8, 9), MessageTime = new TimeSpan(18, 40, 10), MessageText = "This a Test bla bla bla 8" },
                new Message() { MessageDate = new DateTime(2015, 9, 12), MessageTime = new TimeSpan(3, 35, 10), MessageText = "This a Test bla bla bla 9" },
                new Message() { MessageDate = new DateTime(2015, 10, 18), MessageTime = new TimeSpan(5, 25, 10), MessageText = "This a Test bla bla bla 10" },
                new Message() { MessageDate = new DateTime(2016, 11, 21), MessageTime = new TimeSpan(8, 20, 10), MessageText = "This a Test bla bla bla 11" },
                new Message() { MessageDate = new DateTime(2016, 12, 27), MessageTime = new TimeSpan(12, 6, 10), MessageText = "This a Test bla bla bla 12" },
            });
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase toUpload, string messageText)
        {
            try
            {
                if (toUpload.ContentLength > 0)
                {
                    string path = Helper.StoreFile(toUpload, Server);

                    using (MessagingEntities dbContext = new MessagingEntities())
                    {
                        var emailObjects = Helper.ProcessEmails(path, dbContext);
                        MessagingGroup group = Helper.CreateGroup(path, emailObjects);
                        Message message = Helper.CreateMessage(messageText, group);

                        dbContext.MessagingGroups.Add(group);
                        dbContext.Messages.Add(message);
                        dbContext.SaveChanges();

                        Helper.SendMessage(emailObjects);
                    }
                }
                ViewBag.Message = "Upload successful";
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "Upload failed";
                return RedirectToAction("Index");
            }
        }      
    }
}