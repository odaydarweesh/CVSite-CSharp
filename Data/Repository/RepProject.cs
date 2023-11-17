using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace Data.Repository
{
   public class RepProject
    {
        public Datacontext _context { get; set;  }
        public RepProject()
        {

            _context = new Datacontext();
        }


        

        public void TabortProjektUser(ProjectsUsers projectUser)
        {
            _context.ProjectsUsers.Remove(projectUser);

            _context.SaveChanges();
        }

        public void AdderaNyttProjekt(Project project)
        {
            _context.Projects.Add(project);

            _context.ProjectsUsers.Add(new ProjectsUsers { ProjectID = project.ProjectID, UserID = project.Creator });
            _context.SaveChanges();
        }

        public List<ProjectsUsers> HämtaProjektUserGenomUserId(int id)
        {
            return _context.ProjectsUsers.Where(x => x.UserID == id).ToList(); ;
        }

        

        public ProjectsUsers HämtaprojektUsersGenomIdProjektIdUser(int projectID, int userID)
        {
            return _context.ProjectsUsers.Where(x => x.ProjectID == projectID && x.UserID == userID).FirstOrDefault();
        }

        
        public List<ProjectsUsers> HämtaProjektUserGenomIdProjekt(int id)
        {
            return _context.ProjectsUsers.Where(x => x.ProjectID == id).ToList();
        }
        
       

      

        public void TabortProjektGenomId(int id)
        {
            Project project = _context.Projects.Find(id);


            var projectUsers = _context.ProjectsUsers.Where(x => x.ProjectID == id); 


            foreach (var item in projectUsers)
            {
                _context.ProjectsUsers.Remove(item);
            }

            _context.Projects.Remove(project);


            _context.SaveChanges();

        }
        public List<Project> HämtaAllaProjekt()
        {
            return _context.Projects.ToList();
        }

        public List<Project> HämtaAllaProjektMedUser()
        {
            return _context.Projects.Include(x => x.Users).ToList();
        }
        public void TabortUserProjektGenomIdProjektIdUser(int projectID, int userID)
        {
            var Projek = this.HämtaprojektUsersGenomIdProjektIdUser(projectID, userID);

            _context.ProjectsUsers.Remove(Projek);


            _context.SaveChanges();
        }

        public void AttAdderaNyttProjektUser(int projID, int userID)
        {
            _context.ProjectsUsers.Add(new ProjectsUsers { ProjectID = projID, UserID = userID });


            _context.SaveChanges();
        }

    }
}
