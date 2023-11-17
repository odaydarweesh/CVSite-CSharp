using Data;
using Data.Models;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace CVSite.Controllers
{
    public class UserController : Controller
    {
        private Datacontext _context = new Datacontext();
        private RepUser repUser = new RepUser(); 

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
       
      
        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: User/Edit/5

      

        public ActionResult Edit()
        {
            string loggedInUserMail = User.Identity.Name.ToString();
            User user = repUser.HämtaUserGenomMail(loggedInUserMail);
            int? id = user.UserID;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Firstname,Lastname,Adress,Email,PrivateUser")] User user)
        {
            var regexpress = @"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$";
            if (user.Firstname != null && user.Lastname != null && user.Adress != null)
            {
                var firstmatching = Regex.Match(user.Firstname, regexpress, RegexOptions.IgnoreCase);
                var lastmatching = Regex.Match(user.Lastname, regexpress, RegexOptions.IgnoreCase);

                if (!firstmatching.Success || !lastmatching.Success)
                {
                    TempData["alertMessage"] = "One of your name-fields contains invalid characters!";
                }
                else if (ModelState.IsValid)
                {
                    _context.Entry(user).State = EntityState.Modified;
                    _context.SaveChanges();
                    TempData["alertMessage"] = "Uppdaterat!";
                    return RedirectToAction("Edit", new { id = user.UserID });
                }
            }
            else TempData["alertMessage"] = "One of your fields is empty!";
            return View(user);
        }
    }
}
