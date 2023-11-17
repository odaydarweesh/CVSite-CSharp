using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Project
    {
        [Key]
        public int ProjectID { get; set; }
        public string Title { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [ForeignKey("User")]
        [Display(Name = "Creator")]
        public int Creator { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ProjectsUsers> Users { get; set; }
    }
}
