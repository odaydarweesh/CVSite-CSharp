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
    public class HomeController : Controller
    {

        private Datacontext _context = new Datacontext();
        private RepProject RepProject = new RepProject();
        private RepResume RepR = new RepResume();
        private RepUser repUser = new RepUser();
      
        public ActionResult Search(string searchString)
        {
            string loggedInMail = User.Identity.Name.ToString();
            User user = repUser.HämtaUserGenomMail(loggedInMail);

            if (user != null)
            {
                int userId = user.UserID;
                ViewBag.Id = userId;
            }
            return View(repUser.UsersGenomString(searchString)); 
        }


        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Index()
        {
            string loggedInMail = User.Identity.Name.ToString();
            User loggedInUser = repUser.HämtaUserGenomMail(loggedInMail);
            List<Project> projects = RepProject.HämtaAllaProjekt();
            ViewBag.ProjectCount = projects.Count();
            ViewBag.LoggedInId = null;
            if (loggedInUser != null)
            {
                ViewBag.LoggedInId = loggedInUser.UserID;
                var userIdIProjects = _context.ProjectsUsers.Where(pu => pu.UserID == loggedInUser.UserID).ToList();
                List<string> projIds = new List<string>();
                foreach (var item in userIdIProjects)
                {
                    projIds.Add(item.ProjectID.ToString());
                }
                ViewBag.Projects = projIds;
            }

            //Laddar in projekt om det finns.
            if (projects.Count() > 0)
            {
                var project = projects.Last();
                ViewBag.Projectnamn = "Title: " + project.Title;
                var creator = repUser.HämtaUserGenomIdUser(project.Creator);
                if (loggedInUser == null && creator.PrivateUser == true)
                {
                    ViewBag.Creator = "Anonymous User";
                }
                else
                {
                    ViewBag.Creator = creator.Firstname;
                    ViewBag.CreatorId = creator.UserID;
                    ViewBag.ProjId = project.ProjectID;
                }
            }

            List<User> users = null;
            if (loggedInUser == null)
            {
                users = repUser.AllUserSomIntePrivata();
            }
            else
            {
                users = repUser.HämtaAllaUser();
            }
            if (users.Count() > 0)
            {
                int control = users.Count();
                ViewBag.Count = users.Count();

                List<int> InID = new List<int>();
                List<string> name = new List<string>();
                for (int i = 0; i < 3 && i < control; i++)
                {
                    var user = users.Last();
                    InID.Add(user.UserID);
                    name.Add(user.Firstname + " " + user.Lastname);
                    users.Remove(user);
                }
                ViewBag.OID = InID;
                ViewBag.Name = name;
            }
            return View();
        }

        public ActionResult JoinProject(int id)
        {
            string loggedInMail = User.Identity.Name.ToString();
            User user = repUser.HämtaUserGenomMail(loggedInMail);

            RepProject.AttAdderaNyttProjektUser(id, user.UserID);

            return RedirectToAction("Index");
        }

        public ActionResult LeaveProject(int id)
        {
            string loggedInUserMail = User.Identity.Name.ToString();
            User user = repUser.HämtaUserGenomMail(loggedInUserMail);
            var teProjekt = RepProject.HämtaprojektUsersGenomIdProjektIdUser(id, user.UserID);

            RepProject.TabortProjektUser(teProjekt);

            return RedirectToAction("Index");
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}