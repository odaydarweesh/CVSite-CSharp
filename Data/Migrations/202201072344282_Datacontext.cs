namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Datacontext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Educations",
                c => new
                    {
                        EduID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.EduID);
            
            CreateTable(
                "dbo.Resumes",
                c => new
                    {
                        ResumneID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        ImageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ResumneID)
                .ForeignKey("dbo.Photos", t => t.ImageID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.ImageID);
            
            CreateTable(
                "dbo.Experiences",
                c => new
                    {
                        ExperienceID = c.Int(nullable: false, identity: true),
                        Titel = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ExperienceID);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        PhotoID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.PhotoID);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        SkillID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.SkillID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Adress = c.String(),
                        Email = c.String(),
                        PrivateUser = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.ProjectsUsers",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        ProjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.ProjectID })
                .ForeignKey("dbo.Projects", t => t.ProjectID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.ProjectID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Creator = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectID)
                .ForeignKey("dbo.Users", t => t.Creator, cascadeDelete: true)
                .Index(t => t.Creator);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.MessageID);
            
            CreateTable(
                "dbo.UserMessages",
                c => new
                    {
                        RecievingUser = c.Int(nullable: false),
                        MessageID = c.Int(nullable: false),
                        Read = c.Boolean(nullable: false),
                        Sender = c.String(),
                    })
                .PrimaryKey(t => new { t.RecievingUser, t.MessageID })
                .ForeignKey("dbo.Messages", t => t.MessageID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.RecievingUser, cascadeDelete: true)
                .Index(t => t.RecievingUser)
                .Index(t => t.MessageID);
            
            CreateTable(
                "dbo.ResumeEducations",
                c => new
                    {
                        Resume_ResumneID = c.Int(nullable: false),
                        Education_EduID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Resume_ResumneID, t.Education_EduID })
                .ForeignKey("dbo.Resumes", t => t.Resume_ResumneID, cascadeDelete: true)
                .ForeignKey("dbo.Educations", t => t.Education_EduID, cascadeDelete: true)
                .Index(t => t.Resume_ResumneID)
                .Index(t => t.Education_EduID);
            
            CreateTable(
                "dbo.ExperienceResumes",
                c => new
                    {
                        Experience_ExperienceID = c.Int(nullable: false),
                        Resume_ResumneID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Experience_ExperienceID, t.Resume_ResumneID })
                .ForeignKey("dbo.Experiences", t => t.Experience_ExperienceID, cascadeDelete: true)
                .ForeignKey("dbo.Resumes", t => t.Resume_ResumneID, cascadeDelete: true)
                .Index(t => t.Experience_ExperienceID)
                .Index(t => t.Resume_ResumneID);
            
            CreateTable(
                "dbo.SkillResumes",
                c => new
                    {
                        Skill_SkillID = c.Int(nullable: false),
                        Resume_ResumneID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_SkillID, t.Resume_ResumneID })
                .ForeignKey("dbo.Skills", t => t.Skill_SkillID, cascadeDelete: true)
                .ForeignKey("dbo.Resumes", t => t.Resume_ResumneID, cascadeDelete: true)
                .Index(t => t.Skill_SkillID)
                .Index(t => t.Resume_ResumneID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserMessages", "RecievingUser", "dbo.Users");
            DropForeignKey("dbo.UserMessages", "MessageID", "dbo.Messages");
            DropForeignKey("dbo.Resumes", "UserID", "dbo.Users");
            DropForeignKey("dbo.ProjectsUsers", "UserID", "dbo.Users");
            DropForeignKey("dbo.ProjectsUsers", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.Projects", "Creator", "dbo.Users");
            DropForeignKey("dbo.SkillResumes", "Resume_ResumneID", "dbo.Resumes");
            DropForeignKey("dbo.SkillResumes", "Skill_SkillID", "dbo.Skills");
            DropForeignKey("dbo.Resumes", "ImageID", "dbo.Photos");
            DropForeignKey("dbo.ExperienceResumes", "Resume_ResumneID", "dbo.Resumes");
            DropForeignKey("dbo.ExperienceResumes", "Experience_ExperienceID", "dbo.Experiences");
            DropForeignKey("dbo.ResumeEducations", "Education_EduID", "dbo.Educations");
            DropForeignKey("dbo.ResumeEducations", "Resume_ResumneID", "dbo.Resumes");
            DropIndex("dbo.SkillResumes", new[] { "Resume_ResumneID" });
            DropIndex("dbo.SkillResumes", new[] { "Skill_SkillID" });
            DropIndex("dbo.ExperienceResumes", new[] { "Resume_ResumneID" });
            DropIndex("dbo.ExperienceResumes", new[] { "Experience_ExperienceID" });
            DropIndex("dbo.ResumeEducations", new[] { "Education_EduID" });
            DropIndex("dbo.ResumeEducations", new[] { "Resume_ResumneID" });
            DropIndex("dbo.UserMessages", new[] { "MessageID" });
            DropIndex("dbo.UserMessages", new[] { "RecievingUser" });
            DropIndex("dbo.Projects", new[] { "Creator" });
            DropIndex("dbo.ProjectsUsers", new[] { "ProjectID" });
            DropIndex("dbo.ProjectsUsers", new[] { "UserID" });
            DropIndex("dbo.Resumes", new[] { "ImageID" });
            DropIndex("dbo.Resumes", new[] { "UserID" });
            DropTable("dbo.SkillResumes");
            DropTable("dbo.ExperienceResumes");
            DropTable("dbo.ResumeEducations");
            DropTable("dbo.UserMessages");
            DropTable("dbo.Messages");
            DropTable("dbo.Projects");
            DropTable("dbo.ProjectsUsers");
            DropTable("dbo.Users");
            DropTable("dbo.Skills");
            DropTable("dbo.Photos");
            DropTable("dbo.Experiences");
            DropTable("dbo.Resumes");
            DropTable("dbo.Educations");
        }
    }
}
