using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CVSite.Models
{
    public class ResumeModels
    {

        [Required]
        public string User { get; set; }
        [Required]
        public string PhotoURL { get; set; }

        public string Mail { get; set; }
        public List<Skill> Skill { get; set; }

        public List<Experience> Experiences { get; set; }
        public int IdUser { get; set; }
        public string Adress { get; set; }
        public List<Education> Education { get; set; }

        public List<Project> Project { get; set; }
     





        public ResumeModels(string name , string url , List<Education> educa , List<Skill> skill, List<Experience>Experience, List<Project> project)
        {

            Project = project;

            Education = educa;

            Skill = skill;

            User = name;

            PhotoURL = url;

            Experiences = Experience;

        }

      
        public ResumeModels(List<Education> educa, List<Skill> skill, List<Experience> Experience, List<Project> project)
        {

            Project = project;


            Experiences = Experience ;
            Education = educa;

            Skill = skill;
        
        }

        public ResumeModels(int id, string name, string url, string email, string adres, List<Education> educa, List<Skill> skill, List<Experience> Experience , List<Project> project)
        {


            Project = project;

            Adress = adres ;

            IdUser = id;

            User = name;



            Education = educa;

            Skill = skill;

            PhotoURL = url;

            Mail = email;

            Experiences = Experience;

        }

        public ResumeModels(string url, List<Education> educa, List<Skill> skill, List<Experience> Experience, List<Project> project)
        {

            Project = project;

            Education = educa;

            Skill = skill;

            User = "Anonymous";

            PhotoURL = url;

            Experiences = Experience;


        }



    }
}