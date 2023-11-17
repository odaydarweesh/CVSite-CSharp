using CVSite.Models;
using Data;
using Data.Models;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CVSite.Controllers
{
    public class ProjectsController : Controller
    {
        private Datacontext _context = new Datacontext();
        private RepProject RepProject = new RepProject();
        private RepResume RepR = new RepResume();
        private RepUser repUser = new RepUser();

        public ActionResult Insert(int id)
        {
            string loggedInEmail = User.Identity.Name.ToString();
            User user = repUser.HämtaUserGenomMail(loggedInEmail);
            RepProject.AttAdderaNyttProjektUser(id, user.UserID);
            return RedirectToAction("ViewProject");
        }

        public ActionResult Remove(int id)
        {
            string loggedInEmail = User.Identity.Name.ToString();
            User user = repUser.HämtaUserGenomMail(loggedInEmail);
            var teProjekt = RepProject.HämtaprojektUsersGenomIdProjektIdUser(id, user.UserID);
            RepProject.TabortProjektUser(teProjekt);
            return RedirectToAction("ViewProject");
        }

        public ActionResult Create()
        {
            string loggedInEmail = User.Identity.Name.ToString();
            ViewBag.Creator = new SelectList(_context.Users.Where(u => u.Email == loggedInEmail).ToList(), "UserID", "Firstname");
                  return View();
        }

        // GET: Projects
        public ActionResult Index()
        {
            return View();
        }

        // GET: Projects/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectID,Title,Description,Creator")] Project project)
        {
            if (ModelState.IsValid)
            {
                foreach (var proj in _context.Projects)
                {
                    if (proj.Title.ToLower() == project.Title.ToLower())
                    {
                        return RedirectToAction("DuplicateErrorProj");
                    }
                }
                RepProject.AdderaNyttProjekt(project);

                return RedirectToAction("ViewProject");
            }
            ViewBag.Creator = new SelectList(repUser.HämtaAllaUser(), "UserID", "Firstname", project.Creator);
            return View(project);
        }



        // POST: Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectID, Title, Description, Creator")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(project).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("ViewProject");
            }
            ViewBag.Creator = new SelectList(repUser.HämtaAllaUser(), "UserID", "Firstname", project.Creator);
            return View(project);
        }


        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = _context.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RepProject.TabortProjektGenomId(id);
            return RedirectToAction("ViewProject");
        }
        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = _context.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            string loggedInEmail = User.Identity.Name.ToString();
            ViewBag.Creator = new SelectList(_context.Users.Where(u => u.Email == loggedInEmail).ToList(), "UserID", "Firstname");
            return View(project);
        }



        public ActionResult ViewProject()
        {
            string loggedInEmail = User.Identity.Name.ToString();
            User user = repUser.HämtaUserGenomMail(loggedInEmail);
            if (user != null)
            {
                int userId = user.UserID;
                ViewBag.Id = userId;
            }
            List<Project> projects = new List<Project>();
            projects = RepProject.HämtaAllaProjektMedUser();
            List<ProjectModels> ProjectViews = new List<ProjectModels>();
            using (var context = new Datacontext())
            {
                foreach (var item in projects)
                {
                    ProjectModels ProjectsModel = null;
                    if (user == null && item.User.PrivateUser == true)
                    {
                        ProjectsModel = new ProjectModels(item.Creator, item.Title, item.Description, item.ProjectID);
                    }
                    else
                    {
                        string creatorname = item.User.Firstname + " " + item.User.Lastname;
                        ProjectsModel = new ProjectModels(creatorname, item.Creator, item.Title, item.Description, item.ProjectID);
                    }
                    var projusers = RepProject.HämtaProjektUserGenomIdProjekt(item.ProjectID);
                    List<string> namn = new List<string>();
                    List<string> namnNonHidden = new List<string>();
                    foreach (var projectsUsers in projusers)
                    {
                        var anv = context.Users.Where(u => u.UserID == projectsUsers.UserID).ToList();

                        foreach (var anvItem in anv)
                        {
                            namn.Add(anvItem.Firstname);
                            if (anvItem.PrivateUser == false)
                            {
                                namnNonHidden.Add(anvItem.Firstname);
                            }
                        }
                    }
                    ProjectsModel.Users = namn;
                    ProjectsModel.PublicUser = namnNonHidden;
                    ProjectViews.Add(ProjectsModel);
                }
                if (user != null)
                {
                    var userIdCommon = RepProject.HämtaProjektUserGenomUserId(user.UserID);
                    List<string> projIds = new List<string>();
                    foreach (var item in userIdCommon)
                    {
                        projIds.Add(item.ProjectID.ToString());
                    }
                    ViewBag.Projects = projIds;
                }
                return View(ProjectViews);
            }
        }
    }
}
