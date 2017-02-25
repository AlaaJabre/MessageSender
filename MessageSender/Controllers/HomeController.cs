using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MessageSender.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (MessagingEntities dbContext = new MessagingEntities())
            {
                return View(dbContext.Messages.ToList());
            }
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
                        if (dbContext.Messages.Any(c => c.MessageText == messageText))
                            return RedirectToAction("Index");

                        var emailObjects = Helper.ProcessEmails(path, dbContext);
                        MessagingGroup group = Helper.CreateGroup(path, emailObjects);
                        Message message = Helper.CreateMessage(messageText, group);

                        dbContext.MessagingGroups.Add(group);
                        dbContext.Messages.Add(message);
                        dbContext.SaveChanges();

                        Parallel.ForEach(emailObjects, c =>
                        {
                            Helper.SendMessage(c.Email1, "subject", messageText);
                        });                        
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