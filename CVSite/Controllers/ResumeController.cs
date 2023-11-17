using CVSite.Models;
using Data;
using Data.Models;
using Data.Repository;
using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;


namespace CVSite.Controllers
{
    public class ResumeController : Controller
    {

        private Datacontext _context = new Datacontext();
        private RepProject RepProject = new RepProject();
        private RepResume RepR = new RepResume();
        private RepUser repUser = new RepUser();



        public ActionResult Upload()
        {
            foreach (string file in Request.Files)
            {
                var postedFile = Request.Files[file];
                try
                {
                    postedFile.SaveAs(Server.MapPath("~/UploadedFiles/") + Path.GetFileName(postedFile.FileName));
                    _context.Photos.Add(new Photo { Name = postedFile.FileName, Url = Server.MapPath("~/UploadedFiles/") + Path.GetFileName(postedFile.FileName) });
                    _context.SaveChanges();
                }
                catch (Exception e) { ErrorMessage(" Select file!"); }

                var photoId = _context.Photos.OrderByDescending(i => i.PhotoID).FirstOrDefault();
                int resumeId = RepR.HämtaResumGenomMail(User.Identity.Name.ToString());

                var tempCv = from c in _context.Resumes
                             where c.ResumneID == resumeId
                             select c;
                foreach (Resume c in tempCv)
                {
                    c.ImageID = photoId.PhotoID;
                }
                _context.SaveChanges();
            }
            return RedirectToAction("CreateResume");
        }




        public ActionResult Remove(int id)
        {
            string loggedInUserMail = User.Identity.Name.ToString();
            int userID = repUser.HämtaIdUserGenomMail(loggedInUserMail);
            RepProject.TabortUserProjektGenomIdProjektIdUser(id, userID);

            return RedirectToAction("CreateResume");
        }

        public ActionResult UpdateResume(string actionType)
        {
            int resumeId = RepR.HämtaResumGenomMail(User.Identity.Name.ToString());
            Resume resume = RepR.HämtaResumeGenomId(resumeId);

            if (actionType == "Add skill")
            {
                try
                {
                    int skill = Int32.Parse(Request.Form["Skills"]);
                    RepR.AdderaSkillTillResume(resume, skill);
                }
                catch (Exception) { ErrorMessage("Add a skill to the database!"); }
                        return RedirectToAction("CreateResume");

            }
            else if (actionType == "Add Experience")
            {
                try
                {
                    int experience = Int32.Parse(Request.Form["Experineces"]);
                    RepR.AdderaExperienceTillResume(resume, experience);
                }
                catch (Exception) { ErrorMessage("Add experience to the database!");}
                        return RedirectToAction("CreateResume");
            }
            else if (actionType == "Add Education")
            {
                try
                {
                    int education = Int32.Parse(Request.Form["Education"]);
                    RepR.AdderaEducationTillResume(resume, education);
                }
                catch (Exception) { ErrorMessage("Add education to the database!");}
                        return RedirectToAction("CreateResume");
            }

            else if (actionType.Contains("Delete education"))
            {
                string education = actionType.Substring(17);
                RepR.TabortEducationAvResume(resume, education);
                        return RedirectToAction("CreateResume");
            }

            else if (actionType.Contains("Delete skills"))
            {
                string skill = actionType.Substring(14);
                RepR.TabortskillAvResume(resume, skill);
                        return RedirectToAction("CreateResume");
            }

            else if (actionType.Contains("Delete Experience"))
            {
                string experience = actionType.Substring(18);
                RepR.TabortExperienceAvResume(resume, experience);
                        return RedirectToAction("CreateResume");
            }
            else
            {
                         return RedirectToAction("CreateResume");
            }
        }


        public ActionResult CreateResume(HttpPostedFileBase file)
        {
            var files = Directory.GetFiles(Server.MapPath("~/UploadedFiles"));
            int resumeId = RepR.HämtaResumGenomMail(User.Identity.Name.ToString());
            var tempCv = RepR.HämtaResumeGenomId(resumeId);
            var image = _context.Photos.Where(i => i.PhotoID == tempCv.ImageID).FirstOrDefault();
            string path = image.Url;
            ViewBag.Path = ("/UploadedFiles/") + Path.GetFileName(image.Name);
            var exp = RepR.HämtaExperiencGenomIdResume(resumeId);
            var education = RepR.HämtaEducationgenomIdResume(resumeId);
            var skill = RepR.HämtaSkillGenomIdResume(resumeId);
            string loggedInUserMail = User.Identity.Name.ToString();
            int userID = repUser.HämtaIdUserGenomMail(loggedInUserMail);
            ViewBag.Id = userID;
            var projects = RepProject.HämtaAllaProjekt();
            var projectsUsers = RepProject.HämtaProjektUserGenomUserId(userID);
            List<Project> teList = new List<Project>();
            foreach (var item in projects)
            {
                foreach (var item2 in projectsUsers)
                {
                    if (item.ProjectID == item2.ProjectID)
                    {
                        teList.Add(item);
                    }
                }
            }

            var CreateResumeModel = new ResumeModels(education, skill, exp, teList);

            var skillsList = new SelectList(RepR.HämtaAllaSkill(), "SkillID", "Title");
            ViewData["SkillsDB"] = skillsList;

            var experience = new SelectList(RepR.HämtaAllaExperience(), "ExperienceID", "Titel");
            ViewData["ExperienceDB"] = experience;

            var educations = new SelectList(RepR.HämtaAllaEducation(), "EduID", "Title");
            ViewData["EducationsDB"] = educations;

            return View(CreateResumeModel);
        }

        [Route("Resume/{userid:int}/ShowUserResume", Name = "ShowUserResume")]
        public ActionResult ShowUserResume(int userid)
        {
            User user = repUser.HämtaUserGenomIdUser(userid);
            Resume resume = RepR.HämtaResumGenomIdUser(user.UserID);
            int resumeId = resume.ResumneID;
            var exp = RepR.HämtaExperiencGenomIdResume(resumeId);
            var education = RepR.HämtaEducationgenomIdResume(resumeId);
            var skills = RepR.HämtaSkillGenomIdResume(resumeId);
            var img = _context.Photos.Where(i => i.PhotoID == resume.ImageID).FirstOrDefault();
            var projects = RepProject.HämtaAllaProjekt();
            var projectUsers = RepProject.HämtaProjektUserGenomUserId(userid);
            List<Project> tempList = new List<Project>();

            foreach (var item in projects)
            {
                foreach (var item2 in projectUsers)
                {
                    if (item.ProjectID == item2.ProjectID)
                    {
                        tempList.Add(item);
                    }
                }
            }
            string url = ("/UploadedFiles/") + Path.GetFileName(img.Name);
            string name = user.Firstname + " " + user.Lastname;
            ResumeModels model = new ResumeModels(user.UserID, name, url, user.Email, user.Adress, education, skills, exp, tempList);
            if (!User.Identity.IsAuthenticated) 
            {
                foreach (var item in model.Project)
                {
                    if (item.User.PrivateUser == true)
                    {
                        item.User.Firstname = "Anonymous user";
                    }
                }

            }
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ErrorMessage(string message)
        {
            TempData["alertMessage"] = message;
            return RedirectToAction("CreateResume");
        }

        // GET: Resume
   

        // GET: Resume/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Resume/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Resume/Create
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

        // GET: Resume/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Resume/Edit/5
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

        // GET: Resume/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Resume/Delete/5
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
