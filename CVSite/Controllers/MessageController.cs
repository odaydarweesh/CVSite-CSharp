using CVSite.Models;
using Data;
using Data.Models;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CVSite.Controllers
{
    public class MessageController : Controller
    {

        private Datacontext _context = new Datacontext();
        private RepProject RepProject = new RepProject();
        private RepResume RepR = new RepResume();
        private RepUser repUser = new RepUser();
        private RepMessage repMassage = new RepMessage();


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
        // GET: Message/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Message/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }




        public ActionResult MarkAsRead(int id)
        {
            repMassage.MarkeraLäst(id);
            return RedirectToAction("Index");
        }

        public ActionResult MarkAllAsRead(int userID)
        {
            repMassage.MarkAllAsRead(userID);
            return RedirectToAction("Index");
        }

        // GET: Message/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Message/Create
        public ActionResult Create(int id)
        {
            string loggedInMail = User.Identity.Name.ToString();
            User userSend = repUser.HämtaUserGenomMail(loggedInMail);
            string name = null;
            if (userSend != null)
            {
                name = userSend.Firstname + " " + userSend.Lastname;
            }
            var userM = repUser.HämtaUserGenomIdUser(id);
            var model = new MessageModels
            {
                SkickasAv = name,
                Mottagare = id,
                MottagareNamn = userM.Firstname + " " + userM.Lastname
            };

            return View(model);
        }
        // POST: Message/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMeddelande,Innehåll")] Message message) 
        {
            if (ModelState.IsValid)
            {
                _context.Messages.Add(message);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(message);
        }



        // GET: Message
        public ActionResult Index()
        {
            string loggedInMail = User.Identity.Name.ToString();
            User user = repUser.HämtaUserGenomMail(loggedInMail);
            ViewBag.userID = user.UserID;

            var UserMessages = repMassage.HämtaMeddelandeUserGenomIdUser(user.UserID);
            List<MessageModels> listMsgViewModels = new List<MessageModels>();
            foreach (var item in UserMessages)
            {
                var MsgModel = new MessageModels
                {
                    Mottagare = user.UserID,
                    Läst = item.Read,
                    SkickasAv = item.Sender,
                    IdMeddelande = item.MessageID
                };
                var msg = repMassage.HämtaMeddelandeGenomMeddelandeId(item.MessageID);
                MsgModel.Innehåll = msg.Content;
                listMsgViewModels.Add(MsgModel);

            }

            return View(listMsgViewModels);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = _context.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = _context.Messages.Find(id);
            _context.Messages.Remove(message);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

      
       

        //// POST: Message/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
