using Data;
using Data.Models;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CVSite.Controllers
{
    public class EducationController : Controller
    {


        private Datacontext _context = new Datacontext();
        private RepProject RepProject = new RepProject();
        private RepResume RepR = new RepResume();
        private RepUser repUser = new RepUser();
        private RepMessage repMassage = new RepMessage();

        // GET: Education
        public ActionResult Index()
        {
            return View(_context.Educations.ToList());
        }

        // GET: Education/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Education/Create
        public ActionResult Create()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Education/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Education/Edit/5
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

        // GET: Education/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }



        // POST: Education/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EduID,Title")] Education education)
        {
            Education teEdu = _context.Educations.Where(x => x.Title == education.Title).FirstOrDefault();

            if (teEdu == null)
            {
                if (ModelState.IsValid)
                {
                    _context.Educations.Add(education);
                    _context.SaveChanges();
                    return RedirectToAction("Create");
                }
            }

            else if (teEdu.Title.ToLower() == education.Title.ToLower())
            {
                TempData["alertMessage"] = "This education already exists!";
                return RedirectToAction("Create");
            }
            return View(education);
        }

        // POST: Education/Delete/5
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
    }
}
