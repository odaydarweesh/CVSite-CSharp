using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Data.Repository
{
    public class RepResume
    {
        public Datacontext _context { get; set; }
        public RepResume()
        {
            _context = new Datacontext();
        }


       



        public Resume HämtaResumGenomIdUser(int iduser)
        {

            return _context.Resumes.Where(x => x.UserID == iduser).FirstOrDefault();
        }
      
        
        
      



        public List<Experience> HämtaExperiencGenomIdResume (int id)

        {


            return _context.Experiences.Where(x => x.Resumes.Any(z => z.ResumneID == id)).ToList();
        }
        

        public List<Education> HämtaEducationgenomIdResume(int Id)
        {
            return _context.Educations.Where(x => x.Resumes.Any(z => z.ResumneID == Id)).ToList();
        }

        

        public List<Skill> HämtaSkillGenomIdResume(int Id)
        {
            return _context.Skills.Where(x => x.Resumes.Any(z => z.ResumneID == Id)).ToList();
        }


        public List<Skill> HämtaAllaSkill()
        {
            return _context.Skills.ToList();
        }


        public List<Experience> HämtaAllaExperience()
        {
            return _context.Experiences.ToList();
        }

        public void AdderaEducationTillResume(Resume Resume, int id)
        {
            var education = _context.Educations.Where(x => x.EduID == id).FirstOrDefault();

            Resume.Educations.Add(education);

            _context.SaveChanges();
        }

        public void TabortEducationAvResume(Resume Resume, string titel)
        {
            var Education = _context.Educations.Where(x => x.Title.Equals(titel)).FirstOrDefault();

            Resume.Educations.Remove(Education);

            _context.SaveChanges();
        }

        public void TabortskillAvResume(Resume Resume, string erfarenhet)
        {
            var skil = _context.Skills.Where(x => x.Title.Equals(erfarenhet)).FirstOrDefault();

            Resume.Skills.Remove(skil);

            _context.SaveChanges();
        }


        public int HämtaResumGenomMail(string mail)
        {
            string UserEMail = mail;

            RepUser userRespository = new RepUser();

            int userid = userRespository.HämtaIdUserGenomMail(UserEMail);

            Resume Resume = _context.Resumes.Where(u => u.UserID == userid).FirstOrDefault();

            int idR = Resume.ResumneID;

            return idR;
        }

        public List<Education> HämtaAllaEducation()
        {
            return _context.Educations.ToList();
        }

        public void AdderaSkillTillResume(Resume Resume, int id)
        {
            var adderaSkills = _context.Skills.Where(x => x.SkillID == id).FirstOrDefault();

            Resume.Skills.Add(adderaSkills);

            _context.SaveChanges();
        }

        public void AdderaExperienceTillResume(Resume Resume, int ExperId)
        {
            var jobb = _context.Experiences.Where(x => x.ExperienceID == ExperId).FirstOrDefault();

            Resume.Experiences.Add(jobb);


            _context.SaveChanges();
        }

       
        public void TabortExperienceAvResume(Resume Resume, string jobb)
        {

            var ex = _context.Experiences.Where(x => x.Titel.Equals(jobb)).FirstOrDefault();

            Resume.Experiences.Remove(ex);

            _context.SaveChanges();
        }
        public void SkapaResume(int id)
        {
            _context.Resumes.Add(new Data.Models.Resume
            {
                UserID = id,
                ImageID = 1

            });
            _context.SaveChanges();
        }

        public List<Resume> HämtaAllaResume()
        {

            return _context.Resumes.Include(x => x.User).ToList();

        }

        public Resume HämtaResumeGenomId(int? id)
        {

            return _context.Resumes.Find(id);

        }

    }
}
