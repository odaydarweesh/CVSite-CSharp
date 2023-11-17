using Data;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CVSite.Controllers
{
    public class SkillsController : Controller
    {
        private Datacontext _context = new Datacontext();

        // GET: Skills
        public ActionResult Index()
        {
            return View();
        }

        // GET: Skills/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Skills/Create
        public ActionResult Create()
        {
            return View();
        }


        // GET: Skills/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Skills/Edit/5
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

        // GET: Skills/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Skills/Delete/5
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

        // POST: Skills/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SkillID,Title")] Skill skill)
        {
            Skill teSkill = _context.Skills.Where(x => x.Title == skill.Title).FirstOrDefault();

            if (teSkill == null)
            {
                if (ModelState.IsValid)
                {
                    _context.Skills.Add(skill);
                    _context.SaveChanges();
                    return RedirectToAction("Create");
                }
            }
            else if (teSkill.Title.ToLower() == skill.Title.ToLower())
            {
                TempData["alertMessage"] = "Denna färdighet existerar redan i systemet!";
                return RedirectToAction("Create");
            }
            return View(skill);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
