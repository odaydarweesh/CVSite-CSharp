using System;
using System.Collections.Generic;

namespace CVSite.Models
{
    public class ProjectModels
    {
        public string Creator { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreatID { get; set; }
        public int ProjektId { get; set; }
        public List<string> Users { get; set; }
        public List<string> PublicUser { get; set; }
        public ProjectModels(int creatorId, string titel, string beskrivning, int projektId)
        {
            this.Creator = "Anonymous";
           this.Title = titel;
           this.CreatID = creatorId;
            this.ProjektId = projektId;
            this.Description = beskrivning;
        }
        public ProjectModels(string name, int creatorId, string titel, string beskrivning, int projektID)
        {
           this.CreatID = creatorId;
            this.Creator = name;
            this.Title = titel;
            this.ProjektId = projektID;
            this.Description = beskrivning;
        }
    }
}