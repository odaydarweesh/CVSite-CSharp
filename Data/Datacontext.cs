using Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Data
{
    public class Datacontext :DbContext
    {
        public Datacontext()
        {

        }

        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<ProjectsUsers> ProjectsUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProjectsUsers>().HasRequired(p => p.User).WithMany(p => p.Projects).WillCascadeOnDelete(false);
            modelBuilder.Entity<ProjectsUsers>().HasRequired(p => p.Project).WithMany(p => p.Users).WillCascadeOnDelete(false);
        }
    }

}
