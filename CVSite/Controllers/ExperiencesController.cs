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
    public class ExperiencesController : Controller
    {

        private Datacontext _context = new Datacontext();
        private RepProject RepProject = new RepProject();
        private RepResume RepR = new RepResume();
        private RepUser repUser = new RepUser();
        private RepMessage repMassage = new RepMessage();
        // GET: Experiences
        public ActionResult Index()
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
        // GET: Experiences/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Experiences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Experiences/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Experiences/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Experiences/Edit/5
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExperinceID,Titel")] Experience experience)
        {
            Experience teExp = _context.Experiences.Where(x => x.Titel == experience.Titel).FirstOrDefault();

            if (teExp == null)
            {
                if (ModelState.IsValid)
                {

                    _context.Experiences.Add(experience);
                    _context.SaveChanges();
                    return RedirectToAction("Create");
                }
            }

            else if (teExp.Titel.ToLower() == experience.Titel.ToLower())

            {
                TempData["alertMessage"] = "This experience already exists!";
                return RedirectToAction("Create");
            }

            return View(experience);
        }


        // GET: Experiences/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Experiences/Delete/5
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
